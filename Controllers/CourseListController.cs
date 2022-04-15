using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class CourseListController : ODataController
    {
        private readonly rrrdbContext _db;
        private readonly ILogger<CourseListController> _logger;

        public CourseListController(rrrdbContext dbContext, ILogger<CourseListController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<CourseList> Get()
        {
            return _db.CourseLists;
        }

        [EnableQuery]
        public SingleResult<CourseList> Get([FromODataUri] int key)
        {
            var result = _db.CourseLists.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CourseList CourseList)
        {
            _db.CourseLists.Add(CourseList);
            await _db.SaveChangesAsync();
            return Created(CourseList);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<CourseList> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.CourseLists.FindAsync(key);
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
                if (!CourseListExists(key))
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
        public async Task<IActionResult> Put([FromODataUri] int key, Delta<CourseList> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.CourseLists.FindAsync(key);
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
                if (!CourseListExists(key))
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
            CourseList existingCourseList = await _db.CourseLists.FindAsync(key);
            if (existingCourseList == null)
            {
                return NotFound();
            }

            _db.CourseLists.Remove(existingCourseList);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool CourseListExists(int key)
        {
            return _db.CourseLists.Any(p => p.Id == key);
        }
    }
}
