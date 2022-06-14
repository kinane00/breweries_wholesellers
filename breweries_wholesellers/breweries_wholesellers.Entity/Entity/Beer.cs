using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Entity
{
    public class Beer : BaseEntity
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public float AlcoholContent { get; set; }
        public float Price { get; set; }
        public float SalePrice { get; set; }

        public int BreweryId { get; set; }
        public virtual Brewery Brewery { get; set; }
        public virtual ICollection<WholesallerStock> WholesallerStock { get; set; }
        public virtual ICollection<WholesallerSale> WholesallerSale { get; set; }

    }
}
