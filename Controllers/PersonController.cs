using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class PersonController : ODataController
    {
        private readonly rrrdbContext _db;
        private readonly ILogger<PersonController> _logger;
        private string path = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileLocation")["Person"];
        private readonly string NO_FILE_NAME = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileLocation")["DEFAULT"];
        private readonly string ENCODING_PNG = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileEncoding")["png"];
        private readonly string ENCODING_JPG = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileEncoding")["jpg"];

        public PersonController(rrrdbContext dbContext, ILogger<PersonController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public async Task<IQueryable<Person>> Get()
        {
            var result = _db.People;

            byte[] file;
            await foreach (Person person in result)
            {
                file = await FileUtil.GetFile(path + person.Image);
                person.File = FileUtil.DetermineFileType(file) == ".png" ? ENCODING_PNG : ENCODING_JPG;
                person.File += Convert.ToBase64String(file);
            }

            return result;
        }
        
        [EnableQuery]
        public async Task<SingleResult<Person>> Get([FromODataUri] int key)
        {
            IQueryable<Person> result = _db.People.Where(c => c.Id == key);

            byte[] file; 
            foreach (Person person in result) {
                file = await FileUtil.GetFile(path + person.Image);
                person.File = FileUtil.DetermineFileType(file) == ".png" ? ENCODING_PNG  : ENCODING_JPG;
                person.File += Convert.ToBase64String(file); 
            }
            
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<Person> Post([FromBody] Person Person)
        {
            string file = Person.File;
            string extension = ENCODING_PNG;
           
            if ((Person.File + "").Length <= 0)
            {
                Person.Image = NO_FILE_NAME;
                Person.File = Convert.ToBase64String(await FileUtil.GetFile(path + NO_FILE_NAME));
                file = Person.File;
            } else
            {
                extension = file.Substring(file.IndexOf(","));
                file = file.Substring(file.IndexOf(",") + 1);

            }
            Person.File = null;

            _db.People.Add(Person);

            await _db.SaveChangesAsync();
            await FileUtil.SaveFile(Convert.FromBase64String(file), path, Person.Id);

            Person myPerson = Created(Person).Entity;
            myPerson.File = extension + file;
            return myPerson;
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

            FileUtil.DeleteFile(path + existingPerson.Id);

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
