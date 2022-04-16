using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
//using SteelDoorRecipeAPIOdata.Models;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class RecipeController : ODataController
    {
        private readonly rrrdbContext _db;
        private readonly ILogger<RecipeController> _logger;
        private string path = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileLocation")["Recipe"];
        private readonly string NO_FILE_NAME = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileLocation")["DEFAULT"];
        private readonly string ENCODING_PNG = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileEncoding")["png"];
        private readonly string ENCODING_JPG = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileEncoding")["jpg"];

        public RecipeController(rrrdbContext dbContext, ILogger<RecipeController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public async Task<IQueryable<Recipe>> Get()
        {
            var result = _db.Recipes;

            byte[] file; 
            foreach (Recipe recipe in result) {
                file = await FileUtil.GetFile(path + recipe.Image);
                recipe.File = FileUtil.DetermineFileType(file) == ".png" ? ENCODING_PNG  : ENCODING_JPG;
                recipe.File += Convert.ToBase64String(file); 
            }
            return result;
        }

        [EnableQuery]
        public async Task<SingleResult<Recipe>> Get([FromODataUri] int key)
        {
            IQueryable<Recipe> result = _db.Recipes.Where(c => c.Id == key);

            byte[] file; 
            foreach (Recipe person in result) {
                file = await FileUtil.GetFile(path + person.Image);
                person.File = FileUtil.DetermineFileType(file) == ".png" ? ENCODING_PNG  : ENCODING_JPG;
                person.File += Convert.ToBase64String(file); 
            }
            
            return SingleResult.Create(result);

        }

        [EnableQuery]
        public async Task<Recipe> Post([FromBody] Recipe recipe)
        {
            string file = recipe.File;
            string extension = ENCODING_PNG;
           
            if ((recipe.File + "").Length <= 0)
            {
                recipe.Image = NO_FILE_NAME;
                recipe.File = Convert.ToBase64String(await FileUtil.GetFile(path + NO_FILE_NAME));
                file = recipe.File;
            } else
            {
                extension = file.Substring(file.IndexOf(","));
                file = file.Substring(file.IndexOf(",") + 1);

            }
            recipe.File = null;

            _db.Recipes.Add(recipe);

            await _db.SaveChangesAsync();
            await FileUtil.SaveFile(Convert.FromBase64String(file), path, recipe.Id);

            Recipe myPerson = Created(recipe).Entity;
            myPerson.File = extension + file;
            return myPerson;

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

            if ((note.GetInstance().File + "").Length > 0){
                string file = note.GetInstance().File;
                file = file.Substring(file.IndexOf(",") + 1);
                await FileUtil.SaveFile(Convert.FromBase64String(file), path, key);
                note.GetInstance().File = null;
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
        public async Task<IActionResult> Put([FromODataUri] int key, Delta<Recipe> note)
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

            if ((note.GetInstance().File + "").Length > 0){
                string file = note.GetInstance().File;
                file = file.Substring(file.IndexOf(",") + 1);
                await FileUtil.SaveFile(Convert.FromBase64String(file), path, key);
                note.GetInstance().File = null;
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
