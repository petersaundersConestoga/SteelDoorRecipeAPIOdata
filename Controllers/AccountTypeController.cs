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
    public class AccountTypeController : ODataController
    {
        private readonly rrrdbContext _db;
        private readonly ILogger<AccountTypeController> _logger;

        public AccountTypeController(rrrdbContext dbContext, ILogger<AccountTypeController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<AccountType> Get()
        {
            return _db.AccountTypes;
        }

        [EnableQuery]
        public SingleResult<AccountType> Get([FromODataUri] int key)
        {
            var result = _db.AccountTypes.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] AccountType AccountType)
        {
            _db.AccountTypes.Add(AccountType);
            await _db.SaveChangesAsync();
            return Created(AccountType);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<AccountType> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.AccountTypes.FindAsync(key);
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
                if (!AccountTypeExists(key))
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
        public async Task<IActionResult> Put([FromODataUri] int key, Delta<AccountType> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.AccountTypes.FindAsync(key);
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
                if (!AccountTypeExists(key))
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
            AccountType existingAccountType = await _db.AccountTypes.FindAsync(key);
            if (existingAccountType == null)
            {
                return NotFound();
            }

            _db.AccountTypes.Remove(existingAccountType);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool AccountTypeExists(int key)
        {
            return _db.AccountTypes.Any(p => p.Id == key);
        }
    }
}
