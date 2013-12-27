using System;
using ServiceStack.DataAnnotations;

namespace UpsFeedToTable.Models
{
    [Schema("UPS")]
    [Alias("EDI_ProcessedFiles")]
    public class EDI_Processed_Files
    {
        [AutoIncrement]
        public int Id { get; set; } 

        [StringLength(255)]
        public string ProcessedFile { get; set; }

        public string OriginalFileLocation { get; set; }

        public int RecordsProcessed { get; set; }
        public int RecordsInserted { get; set; }

        public DateTime DateProcessed { get; set; }
    }
}