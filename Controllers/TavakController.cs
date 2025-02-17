using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using halak.Models;

namespace halak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TavakController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public TavakController(HalakDbContext context)
        {
            _context = context;
        }

        // GET: api/Tavak
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tavak>>> GetTavaks()
        {
            return await _context.Tavaks.ToListAsync();
        }

        // GET: api/Tavak/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tavak>> GetTavak(int id)
        {
            var tavak = await _context.Tavaks.FindAsync(id);

            if (tavak == null)
            {
                return NotFound();
            }

            return tavak;
        }

        // PUT: api/Tavak/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTavak(int id, Tavak tavak)
        {
            if (id != tavak.Id)
            {
                return BadRequest();
            }

            _context.Entry(tavak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TavakExists(id))
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

        // POST: api/Tavak
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tavak>> PostTavak(Tavak tavak)
        {
            _context.Tavaks.Add(tavak);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTavak", new { id = tavak.Id }, tavak);
        }

        // DELETE: api/Tavak/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTavak(int id)
        {
            var tavak = await _context.Tavaks.FindAsync(id);
            if (tavak == null)
            {
                return NotFound();
            }

            _context.Tavaks.Remove(tavak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TavakExists(int id)
        {
            return _context.Tavaks.Any(e => e.Id == id);
        }
    }
}
