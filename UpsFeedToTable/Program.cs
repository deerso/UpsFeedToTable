using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;
using UpsFeedToTable.Operations;

namespace UpsFeedToTable
{
    class Program
    {
        static void Main()
        {
            App app = new App();
            app.Run();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }

    public class App
    {
        protected ProcessedFiles ProcessedFilesRepo { get; set; }
        protected EDIDataRepository EdiDataRepo { get; set; }
        public void Run()
        {
            var filesDirectory = ConfigUtils.GetAppSetting("EDIFilesLocation", "");
            var connectionString = ConfigUtils.GetConnectionString("databaseConnection");
            if (connectionString.IsNullOrEmpty() || filesDirectory.IsNullOrEmpty())
            {
                "Invalid settings in App.Config, make sure you have a 'databaseConnection' and 'EDIFilesLocation' specified"
                    .PrintDump();
                return;
            }
            IDbConnectionFactory connectionFactory = new OrmLiteConnectionFactory(connectionString, new SqlServerOrmLiteDialectProvider());
            try
            {

                using (var db = connectionFactory.Open())
                {
                    SetupDatabase.Setup(db);
                    ProcessedFilesRepo = new ProcessedFiles(db);
                    EdiDataRepo = new EDIDataRepository(db);

                    var processedFiles = ProcessedFilesRepo.GetAll();
                    var filesToBeProccessed = FileOperations.GetFilesToBeProcessed(filesDirectory, processedFiles).ToList();
                    if (!filesToBeProccessed.Any())
                    {
                        "No new files to process".Print();
                        return;
                    }
                    "Found {0} new files to be processed".FormatWith(filesToBeProccessed.Count()).Print();


                    filesToBeProccessed.Each(filename => ProcessFile(filename, filesDirectory));
                }
            }
            catch (SqlException x)
            {
                "Error running SQL Operations".PrintDump();

                x.Message.PrintDump();
            }
        }

        private void ProcessFile(string filename, string filesDirectory)
        {
            var filePath = Path.Combine(filesDirectory, filename);
            Console.Write("\n\n");
            Console.Write("Processing {0}...".FormatWith(filename));

            EdiDataRepo.EdiData = FileOperations.GetDataForFile(filePath).ToList();
            Console.Write("Done\n");
            Console.Write("Inserting Records into database...");
            EdiDataRepo.InsertMany(EdiDataRepo.EdiData);
            Console.Write("Done.  Inserted {0} Entries".FormatWith(EdiDataRepo.EdiDataCount));
            
            Console.Write("Transforming data to Shipping Records...");
            var shippingRecords = EdiDataRepo.GetShippingRecords();
            Console.Write("Done\n");
            var fileId = ProcessedFilesRepo.InsertProcessedFile(filePath, filename, EdiDataRepo.EdiDataCount);
            Console.Write("Inserting Shipping Costs Records...");
            var counts = EdiDataRepo.InsertShippingCosts(shippingRecords, fileId);
            Console.Write("Done. Inserted {0} records into Logistics.ShippingCosts, and {1} records into Logistics.InvalidData"
                .FormatWith(counts.Item1, counts.Item2));


            ProcessedFilesRepo.UpdateFileCount(counts.Item1 +counts.Item2, fileId);
        }
    }
}