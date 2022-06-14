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
    public class BreweryController : ControllerBase
    {
        private readonly BreweryService<BreweryViewModel, Brewery> _breweryService;
        public BreweryController(BreweryService<BreweryViewModel, Brewery> breweryService)
        {
            _breweryService = breweryService;
        }
        
        [HttpGet]
        public IEnumerable<BreweryViewModel> GetAll()
        {
            var test = _breweryService.DoNothing();
            var items = _breweryService.GetAll();
            return items;
        }

        [HttpGet("GetActiveByName/{name}")]
        public IActionResult GetActiveByName(string name)
        {
            var items = _breweryService.Get(a => a.Name == name);
            return Ok(items);
        }

        //get one
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _breweryService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        //add
        [HttpPost]
        public IActionResult Create([FromBody] BreweryViewModel brewery)
        {
            if (brewery == null)
                return BadRequest();

            var id = _breweryService.Add(brewery);
            return Created($"api/Account/{id}", id); 
        }

        //update
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BreweryViewModel brewery)
        {
            if (brewery == null || brewery.Id != id)
                return BadRequest();

            int retVal = _breweryService.Update(brewery);
            if (retVal == 0)
                return StatusCode(304);  
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  
            else
                return Accepted(brewery);
        }

        //delete 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _breweryService.Remove(id);
            if (retVal == 0)
                return NotFound(); 
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  
            else
                return NoContent();  
        }
    }
}


