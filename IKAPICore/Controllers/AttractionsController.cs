using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKAPICore.Models;

namespace IKAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase
    {
        private readonly itkotdbContext _context;

        public AttractionsController(itkotdbContext context)
        {
            _context = context;
        }

        // GET: api/Attractions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            return await _context.Attractions.Include(n=>n.AttractionImages).ToListAsync();
        }

        // GET: api/Attractions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attraction>> GetAttraction(int id)
        {
            var attraction = await _context.Attractions.FindAsync(id);

            if (attraction == null)
            {
                return NotFound();
            }

            return attraction;
        }

        // PUT: api/Attractions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttraction(int id, Attraction attraction)
        {
            if (id != attraction.Id)
            {
                return BadRequest();
            }

            _context.Entry(attraction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(id))
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

        // POST: api/Attractions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Attraction>> PostAttraction(Attraction attraction)
        {
            _context.Attractions.Add(attraction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttraction", new { id = attraction.Id }, attraction);
        }

        // DELETE: api/Attractions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attraction>> DeleteAttraction(int id)
        {
            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();

            return attraction;
        }

        private bool AttractionExists(int id)
        {
            return _context.Attractions.Any(e => e.Id == id);
        }
    }
}
