using AutoMapper;
using breweries_wholesellers.Entity;
using breweries_wholesellers.Entity.UnitofWork;

namespace breweries_wholesellers.Domain.Service
{
    public class WholesallerStockService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : WholesallerStockViewModel
                                        where Te : WholesallerStock
    {
        //DI must be implemented in specific service as well beside GenericService constructor
        public WholesallerStockService(IUnitOfWork unitOfWork, IMapper mapper)
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
