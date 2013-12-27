using ServiceStack.DataAnnotations;

namespace UpsFeedToTable.Models
{

    [Schema("Logistics")]
    public class ShippingCosts
    {
        [Ignore]
        public string Id
        {
            get { return OrderNumber + " / " + ChargeDescCode; }
        }

        public int OrderNumber { get; set; }

        [StringLength(3)]
        public string ChargeDescCode { get; set; }
        [StringLength(100)]
        public string ChargeDesc { get; set; }
        public decimal Amount { get; set; }
        public override bool Equals(object obj)
        {
            var item = obj as ShippingCosts;

            return item != null && Id.Equals(item.Id);
        }
    }
}