using System.Data;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
//    public class SetupDatabase
//    {
//        public static void Setup(IDbConnection connection)
//        {
//            var dropCreateTables = ConfigUtils.GetListFromAppSetting("DropCreateTables");
//            "Setting up Database Tables".Print();
//            "DropCreateTables::".Print();
//            dropCreateTables.PrintDump();
           
//            CreateSchemaIfNotExist(connection, "UPS");
//            CreateSchemaIfNotExist(connection, "Logistics");

//            if (dropCreateTables.Contains("Logistics.InvalidData"))
//            {
//                "Dropping Invalid Data".Print();
//                connection.DropTable<InvalidData>();
//            }
//            if (dropCreateTables.Contains("UPS.EDI_ProcssedFiles"))
//            {
//                "Dropping EDI_Processed_Files".Print();
//                connection.DropTable<EDI_Processed_Files>();
//            }
//            if (dropCreateTables.Contains("UPS.EDI_Data"))
//            {
//                "Dropping EDI_Data".Print();
//                connection.DropTable<EDI_Data>();
//            }
//            if (dropCreateTables.Contains("Logistics.ShippingCosts"))
//            {
//                "Dropping ShippingCoss".Print();
//                connection.DropTable<ShippingCosts>();
//            }
//            connection.CreateTable<Models.EDI_Data>();
//            connection.CreateTable<Models.EDI_Processed_Files>();
//            connection.CreateTable<Models.ShippingCosts>();
//            connection.CreateTableIfNotExists<Models.Tracking>();
//            connection.CreateTable<Models.InvalidData>();
//        }

//        public static void CreateSchemaIfNotExist(IDbConnection connection, string schemaName)
//        {
//             string createSchemaSQL = @"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{0}')
//                                        BEGIN
//                                        EXEC( 'CREATE SCHEMA {0}' );
//                                        END".FormatWith(schemaName);

//            connection.ExecuteSql(createSchemaSQL);
//        }
       
//    }
}