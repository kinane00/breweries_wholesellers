using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace breweries_wholesellers.Domain
{
    public class BeerViewModel : BaseDomain
    {
        public string Name { get; set; }
        public float AlcoholContent { get; set; }
        public float price { get; set; }
        public float SalePrice { get; set; }

        public int BreweryId { get; set; }

        [JsonIgnore]
        public virtual BreweryViewModel Brewery { get; set; }
        public virtual ICollection<WholesallerStockViewModel> WholesallerStock { get; set; }
    }
}
