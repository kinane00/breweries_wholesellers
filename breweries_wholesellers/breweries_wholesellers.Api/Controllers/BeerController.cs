using breweries_wholesellers.Domain;
using breweries_wholesellers.Domain.Service;
using breweries_wholesellers.Entity;
using breweries_wholesellers.Entity.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace breweries_wholesellers.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly BeerService<BeerViewModel, Beer> _beerService;
        public BeerController(BeerService<BeerViewModel, Beer> beerService)
        {
            _beerService = beerService;
        }
        
        [HttpGet]
        public IEnumerable<BeerViewModel> GetAll()
        {
            var test = _beerService.DoNothing();
            var items = _beerService.GetAll();
            return items;
        }

        [HttpGet("GetByBreweryId/{breweryId}")]
        public IActionResult GetByBreweryId(int breweryId)
        {
            var items = _beerService.Get(a => a.BreweryId == breweryId);
            return Ok(items);
        }

        //get one
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _beerService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        //add
        [HttpPost]
        public IActionResult Create([FromBody] BeerViewModel beer)
        {
            if (beer == null)
                return BadRequest();

            var id = _beerService.Add(beer);
            return Created($"api/Account/{id}", id);  
        }

        //update
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BeerViewModel beer)
        {
            if (beer == null || beer.Id != id)
                return BadRequest();

            int retVal = _beerService.Update(beer);
            if (retVal == 0)
                return StatusCode(304);
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException"); 
            else
                return Accepted(beer);
        }

        //delete 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _beerService.Remove(id);
            if (retVal == 0)
                return NotFound();
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException"); 
            else
                return NoContent(); 
        }
    }
}


