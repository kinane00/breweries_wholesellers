using AutoMapper;
using breweries_wholesellers.Entity;
using breweries_wholesellers.Entity.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace breweries_wholesellers.Domain.Service
{
    public class WholesallerSaleService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : WholesallerSaleViewModel
                                        where Te : WholesallerSale
    {
        //DI must be implemented in specific service as well beside GenericService constructor
        public WholesallerSaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }

        //add here any custom service method or override generic service method
        public bool DoNothing()
        {
            return true;
        }
    }

}
