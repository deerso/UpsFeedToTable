using ServiceStack.DataAnnotations;

namespace UpsFeedToTable.Models
{
    [Schema("dbo")]
    public class Tracking
    {
        [Alias("OrderTrackingID")]
        public int Id { get; set; }
        public int OrderNum { get; set; }

        [StringLength(50)]
        public string TrackingId { get; set; }
         
    }
}