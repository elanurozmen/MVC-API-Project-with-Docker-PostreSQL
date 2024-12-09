using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Project.Models;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly CheckDbContext _context;
        
        public CheckController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: api/Check
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckItem>>> GetCheckItem()
        {
            return await _context.CheckItem.ToListAsync();
        }

        // GET: api/Check/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckItem>> GetCheckItemById(int id)
        {
            var checkItem = await _context.CheckItem.FindAsync(id);

            if (checkItem == null)
            {
                return NotFound();
            }

            return Ok(checkItem);
        }

        // PUT: api/Check/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{checkId}")]
        public async Task<IActionResult> PutCheckItem(int checkId, CheckItem checkItem)
        {
            if (checkId != checkItem.checkId)
            {
                return BadRequest();
            }

            _context.Entry(checkItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckItemExists(checkId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Check
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckItem>> PostCheckItem([FromBody] CheckItem checkItem)
        {
            if (checkItem == null)
                {
                    return BadRequest("CheckItem cannot be null.");
                }

            _context.CheckItem.Add(checkItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCheckItemById), new { id=checkItem.checkId }, checkItem);
        }

        // DELETE: api/Check/5
        [HttpDelete("{checkId}")]
        public async Task<IActionResult> DeleteCheckItem(int checkId)
        {
            var checkItem = await _context.CheckItem.FindAsync(checkId);
            if (checkItem == null)
            {
                return NotFound();
            }

            _context.CheckItem.Remove(checkItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckItemExists(int checkId)
        {
            return _context.CheckItem.Any(e => e.checkId == checkId);
        }
    }
}
