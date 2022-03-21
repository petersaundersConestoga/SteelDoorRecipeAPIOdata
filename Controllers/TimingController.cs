using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using SteelDoorRecipeAPIOdata.Models;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class TimingController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<TimingController> _logger;

        public TimingController(CapstoneRecipeDatabaseContext dbContext, ILogger<TimingController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<Timing> Get()
        {
            return _db.Timings;
        }

        [EnableQuery]
        public SingleResult<Timing> Get([FromODataUri] int key)
        {
            var result = _db.Timings.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Timing Timing)
        {
            _db.Timings.Add(Timing);
            await _db.SaveChangesAsync();
            return Created(Timing);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Timing> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.Timings.FindAsync(key);
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
                if (!TimingExists(key))
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
            Timing existingTiming = await _db.Timings.FindAsync(key);
            if (existingTiming == null)
            {
                return NotFound();
            }

            _db.Timings.Remove(existingTiming);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool TimingExists(int key)
        {
            return _db.Timings.Any(p => p.Id == key);
        }
    }
}
