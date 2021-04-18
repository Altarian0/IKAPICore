using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IKAPICore.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKAPICore.Models;

namespace IKAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GidHelpersController : ControllerBase
    {
        private readonly itkotdbContext _context;

        public GidHelpersController(itkotdbContext context)
        {
            _context = context;
        }

        // GET: api/GidHelpers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GidHelper>>> GetGidHelpers()
        {
            return await _context.GidHelpers.ToListAsync();
        }

        // GET: api/GidHelpers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GidHelper>> GetGidHelper(int id)
        {
            var gidHelper = await _context.GidHelpers.FindAsync(id);

            if (gidHelper == null)
            {
                return NotFound();
            }

            return gidHelper;
        }

        [HttpGet("Search/{searchText}")]
        public async Task<ActionResult<GidHelper>> SearchGidHelpers(string searchText)
        {
            GidHelper currentGidHelper = null;
            var results = await _context.GidHelpers.ToListAsync();

            foreach (var result in results)
            {
                if (result.Question.ToLower().Contains(searchText.ToLower()))
                {
                    currentGidHelper = result;
                    break;
                }

                if (Levenshtein.Distance(result.Question.ToLower(), searchText.ToLower()) < 10)
                {
                    currentGidHelper = result;
                    break;
                }
            }

            if (currentGidHelper == null)
            {
                return new GidHelper();
            }

            return currentGidHelper;
        }

        // PUT: api/GidHelpers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGidHelper(int id, GidHelper gidHelper)
        {
            if (id != gidHelper.Id)
            {
                return BadRequest();
            }

            _context.Entry(gidHelper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GidHelperExists(id))
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

        // POST: api/GidHelpers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GidHelper>> PostGidHelper(GidHelper gidHelper)
        {
            _context.GidHelpers.Add(gidHelper);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGidHelper", new {id = gidHelper.Id}, gidHelper);
        }

        // DELETE: api/GidHelpers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GidHelper>> DeleteGidHelper(int id)
        {
            var gidHelper = await _context.GidHelpers.FindAsync(id);
            if (gidHelper == null)
            {
                return NotFound();
            }

            _context.GidHelpers.Remove(gidHelper);
            await _context.SaveChangesAsync();

            return gidHelper;
        }

        private bool GidHelperExists(int id)
        {
            return _context.GidHelpers.Any(e => e.Id == id);
        }
    }
}