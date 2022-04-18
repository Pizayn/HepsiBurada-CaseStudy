namespace Shopping.Aggregator.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public double PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }

        public int Status { get; set; }
    }
}
