using System.Data;
using ServiceStack;
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
           
            CreateSchemaIfNotExist(connection, "UPS");
            CreateSchemaIfNotExist(connection, "Logistics");

            if (dropCreateTables)
            {
                connection.DropTable<InvalidData>();
                connection.DropTable<EDI_Processed_Files>();
                connection.DropTable<EDI_Data>();
            }
            connection.CreateTable<Models.EDI_Data>(dropCreateTables);
            connection.CreateTable<Models.EDI_Processed_Files>(dropCreateTables);
            connection.CreateTable<Models.ShippingCosts>(dropCreateTables);
            connection.CreateTableIfNotExists<Models.Tracking>();
            connection.CreateTable<Models.InvalidData>(dropCreateTables);
        }

        public static void CreateSchemaIfNotExist(IDbConnection connection, string schemaName)
        {
             string createSchemaSQL = @"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{0}')
                                        BEGIN
                                        EXEC( 'CREATE SCHEMA {0}' );
                                        END".FormatWith(schemaName);

            connection.ExecuteSql(createSchemaSQL);
        }
       
    }
}