using System.Data;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
    public class SetupDatabase
    {
        public static void Setup(IDbConnection connection)
        {
            var dropCreateTables = ConfigUtils.GetAppSetting("DropCreateEditedTables", false);
            "Setting up Database Tables, Drop-Recreate-Tables={0}".Print(dropCreateTables);
            connection.CreateTable<Models.EDI_Data>(dropCreateTables);
            connection.CreateTable<Models.EDI_Processed_Files>(dropCreateTables);
            connection.CreateTable<Models.ShippingCosts>(dropCreateTables);
            connection.CreateTable<Models.InvalidData>(dropCreateTables);
            connection.CreateTable<Models.Tracking>(false);
        }
       
    }
}