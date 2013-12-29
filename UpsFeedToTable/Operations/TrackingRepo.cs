using Deerso.Data.OrmLite.Contracts;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
    public static class TrackingRepo
    {
        public static int GetOrderNumberFromEDIRecord(this ILiteRepository<Tracking> This, EDI_Data ediRecord)
        {
             int iOrderNumber = 0;

            int.TryParse(ediRecord.ShipmentReferenceNumber1, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;
            int.TryParse(ediRecord.ShipmentReferenceNumber2, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;

            int.TryParse(ediRecord.PackageReferenceNumber1, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;
            int.TryParse(ediRecord.PackageReferenceNumber2, out iOrderNumber);
            if (iOrderNumber > 3921)
                return iOrderNumber;

            var item = This.Single(x => x.TrackingId == ediRecord.TrackingNumber);
            if (item != null)
                return item.OrderNum;

            return 0; 
        }

         
    }
}