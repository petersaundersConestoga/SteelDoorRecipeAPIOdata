using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using SteelDoorRecipeAPIOdata.Models;
using System.IO.Pipelines;
using System.Text;
using System.Text.Json;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class ImageRecipeImplementationController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<ImageRecipeImplementationController> _logger;
        private readonly IConfiguration _config;
        private string folder = "recipeimage";
        private string root = "";

        public ImageRecipeImplementationController(
                CapstoneRecipeDatabaseContext dbContext, 
                ILogger<ImageRecipeImplementationController> logger,
                IConfiguration config)
        {
            _db = dbContext;
            _logger = logger;
            _config = config;
        }

        [EnableQuery(PageSize = 15)]
        public async Task<IQueryable<ImageRecipeImplementation>> Get()
        {
            // we do not store the image in the database
            // thus we need to go get it
            // unfortunately due to this we need to convert between our database model an a version with the byte[]
            List<ImageRecipeImplementation> result = new List<ImageRecipeImplementation>();
            ImageRecipeImplementation implementation = null;

            await foreach (ImageRecipe currentRecipe in _db.ImageRecipes.AsAsyncEnumerable())
            {
                // create a new implementation, fill, place into the return list
                implementation = new ImageRecipeImplementation();
                implementation.Id = currentRecipe.Id;
                implementation.RecipeId = currentRecipe.RecipeId;
                implementation.Image = await FileUtil.GetFile(currentRecipe.Location);
                implementation.Location = "";
                implementation.FileType = currentRecipe.Location.Substring(currentRecipe.Location.Length - 3);

                result.Add(implementation);
            }
            return result.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<ImageRecipeImplementation> Get([FromODataUri] int key)
        {
            // get the image model from the db
            IQueryable<ImageRecipe> result = _db.ImageRecipes.Where(c => c.Id == key);

            // we only want one, so convert to single result
            SingleResult<ImageRecipe> myResult = SingleResult.Create(result);

            // create a new image Recipe implementation with the single result
            ImageRecipeImplementation i =  new (myResult.Queryable.First());

            // implement it as in enumerable
            // follow here
            // https://qawithexperts.com/questions/463/how-do-i-create-object-for-iqueryable-in-c
            IQueryable<ImageRecipeImplementation> iq =
                Enumerable.Empty<ImageRecipeImplementation>().AsQueryable();
            

            // return the queryable as a single result
            return SingleResult.Create(iq);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] ImageRecipeImplementation RecipeImplementation)
        {
            await FileUtil.SaveFile(RecipeImplementation.Image, root, RecipeImplementation.Id);
            ImageRecipe Recipe = new ImageRecipe(RecipeImplementation);
            Recipe.Location = GetLocation(RecipeImplementation.Id, RecipeImplementation.Image);
            _db.ImageRecipes.Add(Recipe);
            await _db.SaveChangesAsync();
            return Created(RecipeImplementation);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key)
        {
            ImageRecipeImplementation image = null;
            try
            {
                var req = Request.Body;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string rawjson = await new StreamReader(req).ReadToEndAsync();
                image = JsonSerializer.Deserialize<ImageRecipeImplementation>(rawjson);
            } catch (Exception ex) {
                Console.WriteLine(ex.InnerException);
            }
            var existingNote = await _db.ImageRecipes.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            ImageRecipe myRecipe = new ImageRecipe(image);

            myRecipe.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
                await FileUtil.SaveFile(image.Image, folder, key);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageRecipeExists(key))
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
        public async Task<IActionResult> Put([FromODataUri] int key)
        {
            ImageRecipeImplementation image = null;
            try
            {
                var req = Request.Body;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string rawjson = await new StreamReader(req).ReadToEndAsync();
                image = JsonSerializer.Deserialize<ImageRecipeImplementation>(rawjson);
            } catch (Exception ex) {
                Console.WriteLine(ex.InnerException);
            }
            var existingNote = await _db.ImageRecipes.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            ImageRecipe myRecipe = new ImageRecipe(image);

            myRecipe.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
                await FileUtil.SaveFile(image.Image, folder, key);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageRecipeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            ImageRecipe existingImageRecipe = await _db.ImageRecipes.FindAsync(key);
            if (existingImageRecipe == null)
            {
                return NotFound();
            }

            _db.ImageRecipes.Remove(existingImageRecipe);
            await _db.SaveChangesAsync();
            FileUtil.DeleteFile(existingImageRecipe.Location);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool ImageRecipeExists(int key)
        {
            return _db.ImageRecipes.Any(p => p.Id == key);
        }

        private string GetLocation(int id, byte[] img)
        {
            string location = root + folder + "\\" + id + FileUtil.DetermineFileType(img);
            return location;
        }
    }
}
