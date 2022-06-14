using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Entity
{
    public class Brewery : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }
        public virtual ICollection<WholesallerSale> WholesallerOrder { get; set; }

    }
}
