using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Entity
{
    public class Wholesaller : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<WholesallerStock > WholesallerStock { get; set; }
        public virtual ICollection<WholesallerSale> WholesallerSale { get; set; }
    }
}
