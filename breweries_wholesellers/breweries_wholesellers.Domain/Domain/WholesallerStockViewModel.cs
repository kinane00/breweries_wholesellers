using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Domain
{
    public class WholesallerStockViewModel : BaseDomain
    {
        public int WholeSallerId { get; set; }
        public int BeerId { get; set; }
        public int UnitStock { get; set; }
        public float SalePrice { get; set; }
        public virtual BeerViewModel Beer { get; set; }
        public virtual WholesallerViewModel Wholesaller { get; set; }
    }
}
