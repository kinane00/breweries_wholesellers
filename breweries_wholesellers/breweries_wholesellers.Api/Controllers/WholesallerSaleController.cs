using breweries_wholesellers.Domain;
using breweries_wholesellers.Domain.Service;
using breweries_wholesellers.Entity;
using breweries_wholesellers.Entity.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace breweries_wholesellers.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WholesallerSaleController : ControllerBase
    {
        private readonly WholesallerSaleService<WholesallerSaleViewModel, WholesallerSale> _wholesallerSaleService;
        public WholesallerSaleController( WholesallerSaleService<WholesallerSaleViewModel, WholesallerSale> wholesallerSaleService)
        {
            _wholesallerSaleService = wholesallerSaleService;
        }

        [HttpGet]
        public IEnumerable<WholesallerSaleViewModel> GetAll()
        {
            var items = _wholesallerSaleService.GetAll();
            return items;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _wholesallerSaleService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddSale([FromBody] WholesallerSaleViewModel wholesallerSale)
        {
            if (wholesallerSale == null)
                return BadRequest();

            var id = _wholesallerSaleService.Add(wholesallerSale);
            return Created($"api/Wholesaller/{wholesallerSale.WholeSallerId}", wholesallerSale.WholeSallerId); 
        }
    }
}
