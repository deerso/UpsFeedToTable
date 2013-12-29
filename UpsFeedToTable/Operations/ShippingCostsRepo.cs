using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deerso.Data.OrmLite.Contracts;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
    public static class ShippingCostsOperations
    {
        public static Tuple<int, int> InsertShippingCosts(this ILiteRepository<ShippingCosts> This, ILiteRepository<InvalidData> InvalidDataRepo, IEnumerable<ShippingCosts> shippingCosts, int processedFileId)
        {
            var goodCount = 0;
            var badCount = 0;
            foreach (var s in shippingCosts)
            {
                if (s.OrderNumber == 0)
                {
                    InvalidDataRepo.InsertInvalidShippingCosts(s, processedFileId, "Couldn't find order number");
                    badCount++;
                    continue;
                }
                try
                {
                    This.Insert(s);
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
                    InvalidDataRepo.InsertInvalidShippingCosts(s, processedFileId, reason);
                    badCount++;
                }
            }

            return new Tuple<int, int>(goodCount, badCount);
        }

        public static int InsertInvalidShippingCosts(this ILiteRepository<InvalidData> This,ShippingCosts s, int processedFileId, string reason)
        {
            return (int) This.Insert(new InvalidData
            {
                Amount = s.Amount,
                ChargeDesc = s.ChargeDesc,
                ChargeDescCode = s.ChargeDescCode,
                OrderNumber = s.OrderNumber,
                Reason = reason,
                DateFailed = DateTime.Now,
                ProcessedFileId = processedFileId
            });
        }
        
         
    }
}