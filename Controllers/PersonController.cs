using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
//using SteelDoorRecipeAPIOdata.Models;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class PersonController : ODataController
    {
        private readonly rrrdbContext _db;
        private readonly ILogger<PersonController> _logger;
        private string path = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileLocation")["Person"];

        public PersonController(rrrdbContext dbContext, ILogger<PersonController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public async Task<IQueryable<Person>> GetAsync()
        {
            var result = _db.People;
            foreach (var person in result) { person.File = await FileUtil.GetFile(path + person.Image);}
            return result;
        }
        
        [EnableQuery]
        public async Task<SingleResult<Person>> Get([FromODataUri] int key)
        {
            var result = _db.People.Where(c => c.Id == key);

            foreach (Person person in result) { person.File = await FileUtil.GetFile(path + person.Image); }
            
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Person Person)
        {
            _db.People.Add(Person);
            await _db.SaveChangesAsync();
            return Created(Person);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Person> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.People.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            note.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingNote);
        }
        [EnableQuery]
        public async Task<IActionResult> Put([FromODataUri] int key, Delta<Person> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.People.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            note.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingNote);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            Person existingPerson = await _db.People.FindAsync(key);
            if (existingPerson == null)
            {
                return NotFound();
            }

            _db.People.Remove(existingPerson);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool PersonExists(int key)
        {
            return _db.People.Any(p => p.Id == key);
        }
    }
}
