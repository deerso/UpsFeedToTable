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


                    filesToBeProccessed.Each(filename =>
                    {
                        var filePath = Path.Combine(filesDirectory, filename);
                        "Processing {0}".FormatWith(filename).Print();

                        EdiDataRepo.EdiData = FileOperations.GetDataForFile(filePath).ToList();
                        EdiDataRepo.InsertMany(EdiDataRepo.EdiData);
                        "Inserted {0} Entries".FormatWith(EdiDataRepo.EdiDataCount).Print();
                        

                        var shippingRecords = EdiDataRepo.GetShippingRecords();
                        var fileId = ProcessedFilesRepo.InsertProcessedFile(filePath, filename, EdiDataRepo.EdiDataCount);
                        var counts = EdiDataRepo.InsertShippingCosts(shippingRecords, fileId);
                        "Inserted {0} records into Logistics.ShippingCosts, and {1} records into Logistics.InvalidData"
                            .FormatWith(counts.Item1, counts.Item2).Print();

                        ProcessedFilesRepo.UpdateFileCount(counts.Item1 +counts.Item2, fileId);
                    });
                }
            }
            catch (SqlException x)
            {
                "Error running SQL Operations".PrintDump();

                x.Message.PrintDump();
            }
        }
    }
}