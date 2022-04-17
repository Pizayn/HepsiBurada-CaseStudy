using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order : EntityBase
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

       
    }
}
