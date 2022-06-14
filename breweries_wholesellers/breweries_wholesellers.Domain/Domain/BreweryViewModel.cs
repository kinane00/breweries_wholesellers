using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Domain
{
    public  class BreweryViewModel : BaseDomain
    {
        public string Name { get; set; }
        public virtual ICollection<BeerViewModel> Beers { get; set; }
    }
}
