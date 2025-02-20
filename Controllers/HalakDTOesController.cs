using halak.DTOs;
using halak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace halakDTOes.Controllers
{

    [ApiController]
    [Route("api/halakDTO")]
    public class HalakDTOesController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public HalakDTOesController(HalakDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HalakDTO>>> GetHalak()
        {
            var halak = await _context.Halaks
                .Select(h => new HalakDTO
                {
                    Id = h.Id,
                    Nev = h.Nev,
                    Faj = h.Faj,
                    MeretCm = h.MeretCm, 
                    ToNev = h.To != null ? h.To.Nev : "N/A",
                    Kep = h.Kep != null ? Convert.ToBase64String(h.Kep) : null
                })
                .ToListAsync();

            return Ok(halak);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HalakDTO>> GetHalak(int id)
        {
            var halak = await _context.Halaks
                .Where(h => h.Id == id)
                .Select(h => new HalakDTO
                {
                    Id = h.Id,
                    Nev = h.Nev,
                    Faj = h.Faj,
                    MeretCm = h.MeretCm,
                    ToNev = h.To != null ? h.To.Nev : "N/A",
                    Kep = h.Kep != null ? Convert.ToBase64String(h.Kep) : null
                })
                .FirstOrDefaultAsync();

            if (halak == null)
            {
                return NotFound();
            }

            return Ok(halak);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHalak(int id, HalakDTO halDto)
        {
            var hal = await _context.Halaks.FindAsync(id);
            if (hal == null)
            {
                return NotFound();
            }
            hal.Id = halDto.Id;
            hal.Nev = halDto.Nev;
            hal.Faj = halDto.Faj;
            hal.MeretCm = halDto.MeretCm; // Decimal marad
            hal.Kep = !string.IsNullOrEmpty(halDto.Kep) ? Convert.FromBase64String(halDto.Kep) : null;

            if (!string.IsNullOrEmpty(halDto.ToNev))
            {
                var to = await _context.Tavaks.FirstOrDefaultAsync(t => t.Nev == halDto.ToNev);
                if (to != null)
                {
                    hal.ToId = to.Id;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HalakDTO>> PostHalak(HalakDTO halDto)
        {
            var halak = new Halak
            {
                Id = halDto.Id,
                Nev = halDto.Nev,
                Faj = halDto.Faj,
                MeretCm = halDto.MeretCm, // Decimal marad
                Kep = !string.IsNullOrEmpty(halDto.Kep) ? Convert.FromBase64String(halDto.Kep) : null
            };

            if (!string.IsNullOrEmpty(halDto.ToNev))
            {
                var to = await _context.Tavaks.FirstOrDefaultAsync(t => t.Nev == halDto.ToNev);
                if (to != null)
                {
                    halak.ToId = to.Id;
                }
            }

            _context.Halaks.Add(halak);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHalak), new { id = halak.Id }, halDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHalak(int id)
        {
            var halak = await _context.Halaks.FindAsync(id);
            if (halak == null)
            {
                return NotFound();
            }

            _context.Halaks.Remove(halak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}

