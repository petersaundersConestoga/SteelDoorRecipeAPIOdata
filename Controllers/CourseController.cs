using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SteelDoorRecipeAPIOdata.Models;

namespace SteelDoorRecipeAPIOdata.Controllers
{
    public class CourseController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<RecipeController> _logger;

        public CourseController(CapstoneRecipeDatabaseContext db, ILogger<RecipeController> logger)
        {
            _logger = logger;
            _db = db;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<Course> Get()
        {
            return _db.Courses;
        }
    }
}
