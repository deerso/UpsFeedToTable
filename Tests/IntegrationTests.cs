using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deerso.Data.OrmLite;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;
using UpsFeedToTable;
using UpsFeedToTable.Models;
using Xunit;

namespace Tests
{
    [Trait("Integration Tests", "")]
    public class IntegrationTests
    {
        [Fact (DisplayName = "Grab the correct Order Number from tracking using EDI_Data")]
        public void GetOrderNumber()
        {
            var app = new App();
           
            IDbConnectionFactory connectionFactory = new OrmLiteConnectionFactory(ConfigConnectionStrings.ProductionConnectionString, new SqlServerOrmLiteDialectProvider());

            using (var db = connectionFactory.Open())
            {
                app.Repo = new Repos(new LiteRepoProvider(db));

                var sampleData = new List<EDI_Data>
                {
                    new EDI_Data
                    {
                        Id = 65899,
                        TrackingNumber = "1Z3281X00327268479",
                        NetAmount = 51.19m,
                        ChargeDescriptionCode = "003",
                        ChargeDescription = "Ground Residential Third Party",
                        PackageReferenceNumber1 = "3921",
                        ShipmentReferenceNumber1 = "3921"
                    }
                };

                var shippingRecords = app.TransformToShippingCostRecords(sampleData);

                Assert.NotNull(shippingRecords);
                Assert.Equal(435103, shippingRecords.Single().OrderNumber);
            }
        }

        [Fact(DisplayName = "Insert shipping costs to database")]
        public void InsertShippingCosts()
        {
            IDbConnectionFactory connectionFactory = new OrmLiteConnectionFactory(ConfigConnectionStrings.LocalConnectionString, new SqlServerOrmLiteDialectProvider());
            var app = new App();
            using (var db = connectionFactory.Open())
            {
                Setup(db);
                app.Repo = new Repos(new LiteRepoProvider(db));
                var sampleInsert = new ShippingCosts
                {
                    Amount = 51.19m,
                    ChargeDesc = "Ground Residential Thrid Party",
                    ChargeDescCode = "003",
                    OrderNumber = 435103
                };
                app.Repo.ShippingCosts.Insert(sampleInsert);
            }
        }
        public static void Setup(IDbConnection connection)
        {
            var dropCreateTables = "Logistics.ShippingCosts";
            "Setting up Database Tables".Print();
            "DropCreateTables::".Print();
            dropCreateTables.PrintDump();

            CreateSchemaIfNotExist(connection, "UPS");
            CreateSchemaIfNotExist(connection, "Logistics");

            if (dropCreateTables.Contains("Logistics.InvalidData"))
            {
                "Dropping Invalid Data".Print();
                connection.DropTable<InvalidData>();
            }
            if (dropCreateTables.Contains("UPS.EDI_ProcssedFiles"))
            {
                "Dropping EDI_Processed_Files".Print();
                connection.DropTable<EDI_Processed_Files>();
            }
            if (dropCreateTables.Contains("UPS.EDI_Data"))
            {
                "Dropping EDI_Data".Print();
                connection.DropTable<EDI_Data>();
            }
            if (dropCreateTables.Contains("Logistics.ShippingCosts"))
            {
                "Dropping ShippingCoss".Print();
                connection.DropTable<ShippingCosts>();
            }
            connection.CreateTable<UpsFeedToTable.Models.EDI_Data>();
            connection.CreateTable<UpsFeedToTable.Models.EDI_Processed_Files>();
            connection.CreateTable<UpsFeedToTable.Models.ShippingCosts>();
            connection.CreateTableIfNotExists<UpsFeedToTable.Models.Tracking>();
            connection.CreateTable<UpsFeedToTable.Models.InvalidData>();
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
