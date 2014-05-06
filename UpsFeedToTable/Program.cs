﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using Deerso.Data.OrmLite;
using Deerso.Data.OrmLite.Contracts;
using Deerso.Logging;
using MomentX;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;
using UpsFeedToTable.Models;
using UpsFeedToTable.Operations;

namespace UpsFeedToTable
{
    class Program
    {
        static void Main()
        {
            LogManager.LogFactory = new DeersoLogFactory("d6de2449-5940-4d99-8f88-b385a6799d85", true, new [] { "@KyleGobel"});


            var app = new App(LogManager.GetLogger(""));
            app.Run();
            Console.WriteLine("Press any key to exit\n");
            Console.ReadKey();

        }
    }

    public class App
    {
        public Repos Repo { get; set; }
        public ILog Log { get; set; }

        public App(ILog logger = null)
        {
            Log = logger ?? new FileLogger("ediData.log", true, true);
        }
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
            "~/myFile".MapHostAbsolutePath();
            IDbConnectionFactory connectionFactory = new OrmLiteConnectionFactory(connectionString, new SqlServerOrmLiteDialectProvider());
            try
            {

                using (var db = connectionFactory.Open())
                {
                    Repo = new Repos(new LiteRepoProvider(db));
                    SetupDatabase.Setup(db);

                    var processedFiles = Repo.ProcessedFiles.GetAll().Select(x => x.ProcessedFile);
                    var filesToBeProccessed = FileOperations.GetFilesToBeProcessed(filesDirectory, processedFiles).ToList();
                    if (!filesToBeProccessed.Any())
                    {
                        "No new files to process".Print();
                        return;
                    }
                    "Found {0} new files to be processed".FormatWith(filesToBeProccessed.Count()).Print();

                    int counter = 1;

                    filesToBeProccessed.Each(filename =>
                    {
                        Console.Write("\n\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("File {0} of {1}", counter, filesToBeProccessed.Count);
                        Console.ResetColor();
                        Console.Write("Processing {0}...".FormatWith(filename));

                        var starttime = DateTime.Now;
                            ProcessFile(filename, filesDirectory);
                            Console.WriteLine("\n\n" + filename + " processing complete");
                            counter++;
                        ReportTime(DateTime.Now  -starttime);
                    });
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
          
            List<EDI_Data> ediData;

            using (new MethodTimer(ReportTime))
            {
                ediData = FileOperations.GetDataForFile(filePath).ToList();
            }
            Console.Write("\nInserting Records into database...");

            using (new MethodTimer(x => ReportTime(x, "Inserted {0} Rows".FormatWith(ediData.Count))))
            {
                Repo.EdiData.InsertMany(ediData);
            }

            Console.Write("\nTransforming data to Shipping Records...");

            List<ShippingCosts> shippingRecords;
            using (new MethodTimer(ReportTime))
            {
                shippingRecords = TransformToShippingCostRecords(ediData);
            }
            int fileId = (int) Repo.ProcessedFiles.Insert(new EDI_Processed_Files(filename, filePath, ediData.Count));

            Console.Write("\nInserting Shipping Costs Records...");
            Tuple<int, int> counts;
            using (new MethodTimer(ReportTime))
            {
                counts = Repo.ShippingCosts.InsertShippingCosts(Repo.InvalidShippingCosts, shippingRecords, fileId);
            }

            Console.Write("\nInserted {0} records into Logistics.ShippingCosts, and {1} records into Logistics.InvalidData"
                .FormatWith(counts.Item1, counts.Item2));


           Repo.ProcessedFiles.UpdateProcessedRecordsCount(counts.Item1 + counts.Item2, fileId);
        }

        private void Sleep10Seconds()
        {
            Thread.Sleep(10000);
            Console.WriteLine("Done Sleeping");
        }
        private static void ReportTime(TimeSpan time)
        {
            ReportTime(time, "");
        }
        private static void ReportTime(TimeSpan time, string extraMessage)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Finished in {0}. {1}", time.PrettyTime(), extraMessage);
            Console.ResetColor();
        }
        public List<ShippingCosts> TransformToShippingCostRecords(IEnumerable<EDI_Data> data)
        {
            return data.Select(x => new ShippingCosts
            {
                Amount = x.NetAmount,
                OrderNumber = Repo.Tracking.GetOrderNumberFromEDIRecord(x),
                ChargeDescCode = x.ChargeDescriptionCode,
                ChargeDesc = x.ChargeDescription
            }).ToList();
        }
    }
}