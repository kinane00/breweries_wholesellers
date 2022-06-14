using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Entity
{
    public class WholesallerStock: BaseEntity
    {
        public int WholeSallerId { get; set; }
        public int BeerId { get; set; }
        public float SalePrice { get; set; }
        public int UnitStock { get; set; }
        public virtual Beer Beer { get; set; }
        public virtual Wholesaller Wholesaller { get; set; }
    }
}
