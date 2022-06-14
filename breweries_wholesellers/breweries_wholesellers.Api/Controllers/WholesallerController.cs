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
    [ApiController]
    public class WholesallerController : ControllerBase
    {
        private readonly WholesallerService<WholesallerViewModel, Wholesaller> _wholesallerService;
        public WholesallerController(WholesallerService<WholesallerViewModel, Wholesaller> wholesallerService)
        {
            _wholesallerService = wholesallerService;
        }

        [HttpGet]
        public IEnumerable<WholesallerViewModel> GetAll()
        {
            var items = _wholesallerService.GetAll();
            return items;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _wholesallerService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }        

    }
}


