using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
    public class ProcessedFiles
    {
        protected IDbConnection Db { get; set; }
        public ProcessedFiles(IDbConnection db)
        {
            Db = db;
        }

        public IEnumerable<string> GetAll()
        {
            return Db.Select<EDI_Processed_Files>().Select(x => x.ProcessedFile);
        }

        public int InsertProcessedFile(string filePath, string processedFile, int dataCount)
        {
            return (int)Db.Insert(new EDI_Processed_Files
            {
                DateProcessed = DateTime.Now,
                OriginalFileLocation = filePath,
                ProcessedFile = processedFile,
                RecordsInserted = dataCount
            }, true);
        }

        public void UpdateFileCount(int count, int fileId)
        {
            var item = Db.SingleById<EDI_Processed_Files>(fileId);
            item.RecordsProcessed = count;
            Db.Update(item);
        }

    }
}