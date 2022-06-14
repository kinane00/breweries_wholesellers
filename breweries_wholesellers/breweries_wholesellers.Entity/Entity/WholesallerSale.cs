using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Entity
{
    public class WholesallerSale : BaseEntity
    {
        public int WholeSallerId { get; set; }
        public int BeerId { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }

        public virtual Beer Beer { get; set; }
        public virtual Wholesaller Wholesaller { get; set; }

    }
}
