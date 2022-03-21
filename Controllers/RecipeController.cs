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
    public class RecipeController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<RecipeController> _logger;

        // fully works
        // see postman example here
        /*
        {
            "Id": 12,
            "PersonId": 0,
            "CuisineId": 0,
            "Name": "",
            "CreationDate": "1900-01-01T00:00:00Z",
            "ServingCount": 0,
            "Story": "",
            "Difficulty": 0
        }
         */
        public RecipeController(CapstoneRecipeDatabaseContext dbContext, ILogger<RecipeController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<Recipe> Get()
        {
            return _db.Recipes;
        }

        [EnableQuery]
        public SingleResult<Recipe> Get([FromODataUri] int key)
        {
            var result = _db.Recipes.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Recipe recipe)
        {
            _db.Recipes.Add(recipe);
            await _db.SaveChangesAsync();
            return Created(recipe);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Recipe> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.Recipes.FindAsync(key);
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
                if (!RecipeExists(key))
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
            Recipe existingRecipe = await _db.Recipes.FindAsync(key);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            _db.Recipes.Remove(existingRecipe);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool RecipeExists(int key)
        {
            return _db.Recipes.Any(p => p.Id == key);
        }
    }
}
