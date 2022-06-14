using AutoMapper;
using System;
using System.Collections.Generic;

namespace breweries_wholesellers.Domain
{
    /// <summary>
    /// A account with users
    /// </summary>
    public class WholesallerSaleViewModel : BaseDomain
    {
        public int WholeSallerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        public float TotalPrice { get; set; }
        
        public virtual BeerViewModel Beer { get; set; }
        public virtual WholesallerViewModel Wholesaller { get; set; }
    }

}


