using AutoMapper;
using breweries_wholesellers.Entity;
using breweries_wholesellers.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace breweries_wholesellers.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        private breweries_wholesellersContext m_Context;

        public MappingProfile(breweries_wholesellersContext context)
        {
            m_Context = context;
        }
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<BreweryViewModel, Brewery>();
            CreateMap<Brewery, BreweryViewModel>();
            CreateMap<BeerViewModel, Beer>();
            CreateMap<Beer, BeerViewModel>();
            CreateMap<WholesallerViewModel, Wholesaller>();
            CreateMap<Wholesaller, WholesallerViewModel>();
            CreateMap<WholesallerStockViewModel, WholesallerStock>();
            CreateMap<WholesallerStock, WholesallerStockViewModel>();
            CreateMap<WholesallerSaleViewModel, WholesallerSale>();
            CreateMap<WholesallerSale, WholesallerSaleViewModel>();

        }

    }





}
