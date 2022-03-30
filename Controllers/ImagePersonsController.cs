#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteelDoorRecipeAPIOdata.Models;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagePersonsController : ControllerBase
    {
        private readonly CapstoneRecipeDatabaseContext _context;

        public ImagePersonsController(CapstoneRecipeDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ImagePersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagePerson>>> GetImagePeople()
        {
            return await _context.ImagePeople.ToListAsync();
        }

        // GET: api/ImagePersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagePerson>> Get(int id)
        {
            var imagePerson = await _context.ImagePeople.FindAsync(id);

            if (imagePerson == null)
            {
                return NotFound();
            }

            return imagePerson;
        }

        // PUT: api/ImagePersons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ImagePerson imagePerson)
        {
            if (id != imagePerson.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagePerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagePersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImagePersons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagePerson>> Post(ImagePerson imagePerson)
        {
            _context.ImagePeople.Add(imagePerson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImagePersonExists(imagePerson.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImagePerson", new { id = imagePerson.Id }, imagePerson);
        }

        // DELETE: api/ImagePersons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var imagePerson = await _context.ImagePeople.FindAsync(id);
            if (imagePerson == null)
            {
                return NotFound();
            }

            _context.ImagePeople.Remove(imagePerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagePersonExists(int id)
        {
            return _context.ImagePeople.Any(e => e.Id == id);
        }
    }
}
