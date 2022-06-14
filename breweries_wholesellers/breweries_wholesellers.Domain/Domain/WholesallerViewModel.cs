using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Domain
{
    public class WholesallerViewModel : BaseDomain
    {
        public string Name { get; set; }

        public virtual ICollection<WholesallerStockViewModel> WholesallerStock { get; set; }
    }
}
