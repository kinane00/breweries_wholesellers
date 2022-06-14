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
    public class WholesallerStockController : ControllerBase
    {
        private readonly WholesallerStockService<WholesallerStockViewModel, WholesallerStock> _wholesallerStockService;
        public WholesallerStockController(WholesallerStockService<WholesallerStockViewModel, WholesallerStock> wholesallerStockService)
        {
            _wholesallerStockService = wholesallerStockService;
        }

        [HttpGet]
        public IEnumerable<WholesallerStockViewModel> GetAll()
        {
            var items = _wholesallerStockService.GetAll();
            return items;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _wholesallerStockService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("{wholesallerId}-{beerId}-{quantity}")]
        public IActionResult GetClientQuote(int wholesallerId, int beerId, int quantity)
        {
            var wholeStock = _wholesallerStockService.Get(w => w.WholeSallerId == wholesallerId && w.BeerId == beerId).FirstOrDefault();
            if (wholeStock == null)
                return NotFound();
            if (quantity > wholeStock.UnitStock)
                throw new Exception("Out of stock");
            double total = 0;
            if (quantity > 20)
            {
                total = (wholeStock.SalePrice - (wholeStock.SalePrice * 0.2)) * quantity;
            }
            else if (quantity > 10)
            {
                total = (wholeStock.SalePrice - (wholeStock.SalePrice * 0.1)) * quantity;
            }
            else
            {
                total = wholeStock.SalePrice * quantity;
            }
            return Ok(total);
        }

        [HttpPut]
        public IActionResult UpdateStock([FromBody] WholesallerStockViewModel wholesallerStockViewModel)
        {
            if (wholesallerStockViewModel == null)
                return BadRequest();

            int retVal = _wholesallerStockService.Update(wholesallerStockViewModel);
            if (retVal == 0)
                return StatusCode(304);  //Not Modified
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //412 Precondition Failed  - concurrency
            else
                return Accepted(wholesallerStockViewModel);
        }
    }
}
