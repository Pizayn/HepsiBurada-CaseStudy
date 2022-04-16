namespace Discount.API.Entities
{
    public class Campaign
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }

    }
}
