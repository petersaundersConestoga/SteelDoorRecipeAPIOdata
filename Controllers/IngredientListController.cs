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
    public class IngredientListController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<IngredientListController> _logger;

        public IngredientListController(CapstoneRecipeDatabaseContext dbContext, ILogger<IngredientListController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<IngredientList> Get()
        {
            return _db.IngredientLists;
        }

        [EnableQuery]
        public SingleResult<IngredientList> Get([FromODataUri] int key)
        {
            var result = _db.IngredientLists.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] IngredientList IngredientList)
        {
            _db.IngredientLists.Add(IngredientList);
            await _db.SaveChangesAsync();
            return Created(IngredientList);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<IngredientList> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.IngredientLists.FindAsync(key);
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
                if (!IngredientListExists(key))
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
            IngredientList existingIngredientList = await _db.IngredientLists.FindAsync(key);
            if (existingIngredientList == null)
            {
                return NotFound();
            }

            _db.IngredientLists.Remove(existingIngredientList);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool IngredientListExists(int key)
        {
            return _db.IngredientLists.Any(p => p.Id == key);
        }
    }
}
