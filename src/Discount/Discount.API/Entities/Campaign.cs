using Discount.API.Enum;
using System.ComponentModel.DataAnnotations;

namespace Discount.API.Entities
{
    public class Campaign
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public double PriceManipulationLimit { get; set; }
        [Required]
        public int TargetSalesCount { get; set; }

        public int Status { get; set; }


    }
}
