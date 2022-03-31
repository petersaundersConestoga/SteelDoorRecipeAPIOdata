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
    public class ImagePersonController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<ImagePersonController> _logger;
        private static string folder = System.Configuration.ConfigurationManager.AppSettings["Person"].ToString();
        private static string root = System.Configuration.ConfigurationManager.AppSettings["Root"].ToString();

        public ImagePersonController(CapstoneRecipeDatabaseContext dbContext, ILogger<ImagePersonController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public async Task<IQueryable<ImagePersonImplementation>> Get()
        {
            // we do not store the image in the database
            // thus we need to go get it
            // unfortunately due to this we need to convert between our database model an a version with the byte[]
            List<ImagePerson> imagePeople = await _db.ImagePeople.ToListAsync();
            IAsyncEnumerable<ImagePerson> asyncPeople = (IAsyncEnumerable<ImagePerson>)imagePeople;
            List<ImagePersonImplementation> result = new List<ImagePersonImplementation>();
            ImagePersonImplementation implementation = null;

            await foreach (ImagePerson currentPerson in asyncPeople)
            {
                // create a new implementation, fill, place into the return list
                implementation = new ImagePersonImplementation();
                implementation.Id = currentPerson.Id;
                implementation.PersonId = currentPerson.PersonId;
                implementation.Image = await FileUtil.GetFile(currentPerson.Location);
                implementation.Location = "";

                result.Add(implementation);
            }
            return result.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<ImagePersonImplementation> Get([FromODataUri] int key)
        {
            // get the image model from the db
            IQueryable<ImagePerson> result = _db.ImagePeople.Where(c => c.Id == key);

            // we only want one, so convert to single result
            SingleResult<ImagePerson> myResult = SingleResult.Create(result);

            // create a new image person implementation with the single result
            ImagePersonImplementation i =  new (myResult.Queryable.First());
            
            // turn that image person into a queryable
            IQueryable<ImagePersonImplementation> iq = (IQueryable<ImagePersonImplementation>)i;

            // return the queryable as a single result
            return SingleResult.Create(iq);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] ImagePersonImplementation personImplementation)
        {
            await FileUtil.SaveFile(personImplementation.Image, root, personImplementation.Id);
            ImagePerson person = new ImagePerson(personImplementation);
            person.Location = GetLocation(personImplementation.Id, personImplementation.Image);
            _db.ImagePeople.Add(person);
            await _db.SaveChangesAsync();
            return Created(personImplementation);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<ImagePersonImplementation> note)
        {
            Delta<ImagePerson> person = new();
            person.GetInstance().Id = note.GetInstance().Id;
            person.GetInstance().PersonId = note.GetInstance().PersonId;
            person.GetInstance().Location = GetLocation(note.GetInstance().Id, note.GetInstance().Image);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.ImagePeople.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            person.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
                await FileUtil.SaveFile(note.GetInstance().Image, folder, key);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagePersonExists(key))
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
        public async Task<IActionResult> Put([FromODataUri] int key, Delta<ImagePersonImplementation> note)
        {
            Delta<ImagePerson> person = new();
            person.GetInstance().Id = note.GetInstance().Id;
            person.GetInstance().PersonId = note.GetInstance().PersonId;
            person.GetInstance().Location = GetLocation(note.GetInstance().Id, note.GetInstance().Image);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.ImagePeople.FindAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            person.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
                await FileUtil.SaveFile(note.GetInstance().Image, folder, key);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagePersonExists(key))
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
            ImagePerson existingImagePerson = await _db.ImagePeople.FindAsync(key);
            if (existingImagePerson == null)
            {
                return NotFound();
            }

            _db.ImagePeople.Remove(existingImagePerson);
            await _db.SaveChangesAsync();
            FileUtil.DeleteFile(existingImagePerson.Location);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool ImagePersonExists(int key)
        {
            return _db.ImagePeople.Any(p => p.Id == key);
        }

        private string GetLocation(int id, byte[] img)
        {
            string location = root + folder + "\\" + id + FileUtil.DetermineFileType(img);
            return location;
        }
    }
}
