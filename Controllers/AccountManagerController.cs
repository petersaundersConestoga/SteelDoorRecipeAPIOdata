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
    public class AccountManagerController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<AccountManagerController> _logger;

        public AccountManagerController(CapstoneRecipeDatabaseContext dbContext, ILogger<AccountManagerController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<AccountManager> Get()
        {
            return _db.AccountManagers;
        }

        [EnableQuery]
        public SingleResult<AccountManager> Get([FromODataUri] int key)
        {
            var result = _db.AccountManagers.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] AccountManager AccountManager)
        {
            _db.AccountManagers.Add(AccountManager);
            await _db.SaveChangesAsync();
            return Created(AccountManager);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<AccountManager> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.AccountManagers.FindAsync(key);
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
                if (!AccountManagerExists(key))
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
            AccountManager existingAccountManager = await _db.AccountManagers.FindAsync(key);
            if (existingAccountManager == null)
            {
                return NotFound();
            }

            _db.AccountManagers.Remove(existingAccountManager);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool AccountManagerExists(int key)
        {
            return _db.AccountManagers.Any(p => p.Id == key);
        }
    }
}
