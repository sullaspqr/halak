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
    public class HorgaszokController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public HorgaszokController(HalakDbContext context)
        {
            _context = context;
        }

        // GET: api/Horgaszok
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horgaszok>>> GetHorgaszoks()
        {
            return await _context.Horgaszoks.ToListAsync();
        }

        // GET: api/Horgaszok/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Horgaszok>> GetHorgaszok(int id)
        {
            var horgaszok = await _context.Horgaszoks.FindAsync(id);

            if (horgaszok == null)
            {
                return NotFound();
            }

            return horgaszok;
        }

        // PUT: api/Horgaszok/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorgaszok(int id, Horgaszok horgaszok)
        {
            if (id != horgaszok.Id)
            {
                return BadRequest();
            }

            _context.Entry(horgaszok).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorgaszokExists(id))
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

        // POST: api/Horgaszok
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Horgaszok>> PostHorgaszok(Horgaszok horgaszok)
        {
            _context.Horgaszoks.Add(horgaszok);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorgaszok", new { id = horgaszok.Id }, horgaszok);
        }

        // DELETE: api/Horgaszok/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorgaszok(int id)
        {
            var horgaszok = await _context.Horgaszoks.FindAsync(id);
            if (horgaszok == null)
            {
                return NotFound();
            }

            _context.Horgaszoks.Remove(horgaszok);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorgaszokExists(int id)
        {
            return _context.Horgaszoks.Any(e => e.Id == id);
        }
    }
}
