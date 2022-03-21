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
    public class InstructionController : ODataController
    {
        private readonly CapstoneRecipeDatabaseContext _db;
        private readonly ILogger<InstructionController> _logger;

        public InstructionController(CapstoneRecipeDatabaseContext dbContext, ILogger<InstructionController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<Instruction> Get()
        {
            return _db.Instructions;
        }

        [EnableQuery]
        public SingleResult<Instruction> Get([FromODataUri] int key)
        {
            var result = _db.Instructions.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Instruction Instruction)
        {
            _db.Instructions.Add(Instruction);
            await _db.SaveChangesAsync();
            return Created(Instruction);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Instruction> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.Instructions.FindAsync(key);
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
                if (!InstructionExists(key))
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
            Instruction existingInstruction = await _db.Instructions.FindAsync(key);
            if (existingInstruction == null)
            {
                return NotFound();
            }

            _db.Instructions.Remove(existingInstruction);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool InstructionExists(int key)
        {
            return _db.Instructions.Any(p => p.Id == key);
        }
    }
}
