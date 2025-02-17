using halak.DTOs;
using halak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Include(h => h.To)
                .Select(h => new HalakDTO
                {
                    Nev = h.Nev,
                    Faj = h.Faj,
                    MeretCm = Convert.ToInt32(h.MeretCm),
                    ToNev = h.To.Nev
                })
                .ToListAsync();

            return Ok(halak);
        }
    }

}

