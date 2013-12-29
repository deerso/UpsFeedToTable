using Deerso.Data.OrmLite.Contracts;
using UpsFeedToTable.Models;

namespace UpsFeedToTable.Operations
{
    public static class ProcessedFiles
    {
        public static void UpdateProcessedRecordsCount(this ILiteRepository<EDI_Processed_Files> This, int count, int fileId)
        {
            var item = This.FindById(fileId);
            if (item != null)
                item.RecordsProcessed = count;

            This.Update(item);
        }

    }
}