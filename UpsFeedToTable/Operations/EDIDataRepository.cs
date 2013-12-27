using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.OrmLite;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
    public class EDIDataRepository
    {
        protected IDbConnection Db { get; set; }
        public List<EDI_Data> EdiData { get; set; }

        public int EdiDataCount
        {
            get { return EdiData.Count; }
        }

        public EDIDataRepository(IDbConnection db)
        {
            Db = db;
        }

        public void InsertMany(IEnumerable<EDI_Data> data)
        {
            Db.InsertAll(data);
        }

        public List<ShippingCosts> GetShippingRecords()
        {
            var tuple = EdiData.Select(x => new Tuple<string, ShippingCosts>(x.TrackingNumber, new ShippingCosts
            {
                Amount = x.NetAmount,
                OrderNumber = FileOperations.ParseInt(x.ShipmentReferenceNumber1),
                ChargeDescCode = x.ChargeDescriptionCode,
                ChargeDesc = x.ChargeDescription
            })).ToList();

            var goodItems = tuple.Where(x => x.Item2.OrderNumber > 0 && x.Item2.OrderNumber != 3921).Select(x => x.Item2).ToList();

            //attempt to get order number from tracking table
            var correctedItems =
                tuple.Where(x => x.Item2.OrderNumber == 0 || x.Item2.OrderNumber == 3921).Select(x => new ShippingCosts
                {
                    Amount = x.Item2.Amount,
                    OrderNumber = GetOrderNumberFromTrackingNumber(x.Item1),
                    ChargeDescCode = x.Item2.ChargeDescCode,
                    ChargeDesc = x.Item2.ChargeDesc
                }).ToList();

            goodItems.AddRange(correctedItems);

            return goodItems;
        }

        //move this
        public int GetOrderNumberFromTrackingNumber(string trackingNumber)
        {
            var item = Db.SingleWhere<Tracking>("TrackingId", trackingNumber);
            return item != null ? item.OrderNum : 0;
        }
        //move this
        public Tuple<int, int> InsertShippingCosts(IEnumerable<ShippingCosts> shippingCosts, long processedFileId)
        {
            Db.CreateTableIfNotExists<InvalidData>();
            var goodCount = 0;
            var badCount = 0;
            foreach (var s in shippingCosts)
            {
                if (s.OrderNumber == 0)
                {
                    InsertInvalidData(s, processedFileId, "Couldn't find order number");
                    continue;
                }
                try
                {
                    Db.Insert(s);
                    goodCount++;
                }
                catch (SqlException x)
                {
                    string reason = x.Message;
                    if (x.Errors.Count > 0)
                    {
                        switch (x.Errors[0].Number)
                        {
                            case 547:
                                reason = "No OrderNumber Found - Foreign Key Violation";
                                break;
                            case 2601:
                                reason = "Item Already Inserted - Primary Key Violation";
                                break;
                        }
                    }
                    InsertInvalidData(s, processedFileId, reason);
                    badCount++;
                }
            }

            return new Tuple<int, int>(goodCount, badCount);
        }

        public void InsertInvalidData(ShippingCosts s, long processedFileId, string reason)
        {
            Db.Insert(new InvalidData
            {
                Amount = s.Amount,
                ChargeDesc = s.ChargeDesc,
                ChargeDescCode = s.ChargeDescCode,
                OrderNumber = s.OrderNumber,
                Reason = reason,
                DateFailed = DateTime.Now,
                ProcessedFileId = (int)processedFileId
            });
        }
    }
}