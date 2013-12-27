using System;
using ServiceStack.DataAnnotations;

namespace UpsFeedToTable.Models
{
    [Schema("Logistics")]
    public class InvalidData
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string ChargeDescCode { get; set; }
        public string ChargeDesc { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime DateFailed { get; set; }

        [ForeignKey(typeof(EDI_Processed_Files))]
        public int ProcessedFileId { get; set; }

    }
}