using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FastMember;
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

        public int GetOrderNumber(EDI_Data data)
        {
            int iOrderNumber = 0;

            int.TryParse(data.ShipmentReferenceNumber1, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;
            int.TryParse(data.ShipmentReferenceNumber2, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;

            int.TryParse(data.PackageReferenceNumber1, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;
            int.TryParse(data.PackageReferenceNumber2, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;

            var item = Db.Single<Tracking>(x => x.TrackingId == data.TrackingNumber);
            if (item != null)
                return item.OrderNum;

            return 0;
        }
        public List<ShippingCosts> GetShippingRecords()
        {
            return EdiData.Select(x => new ShippingCosts
            {
                Amount = x.NetAmount,
                OrderNumber = GetOrderNumber(x),
                ChargeDescCode = x.ChargeDescriptionCode,
                ChargeDesc = x.ChargeDescription
            }).ToList();
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
                    badCount++;
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