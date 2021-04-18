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
    public class ToursController : ControllerBase
    {
        private readonly itkotdbContext _context;

        public ToursController(itkotdbContext context)
        {
            _context = context;
        }

        // GET: api/Tours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTours()
        {
            return await _context.Tours.Where(n=>n.StartDate > DateTime.Now).Include(n=>n.TourAttractions).
                                        ThenInclude(n=>n.Attraction).
                                        ThenInclude(n=>n.AttractionImages).
                                        Include(n=>n.FromPlace).
                                        Include(n=>n.ToPlace).
                                        Include(n=>n.Agent).ThenInclude(n=>n.Company).
                                        ToListAsync();
        }
        
        [HttpGet("SearchTours/{text}")]
        public async Task<ActionResult<IEnumerable<Tour>>> SearchTours(string text)
        {
            return await _context.Tours.Where(n=>n.StartDate > DateTime.Now).
                Include(n=>n.TourAttractions).
                ThenInclude(n=>n.Attraction).
                ThenInclude(n=>n.AttractionImages).
                Include(n=>n.FromPlace).
                Include(n=>n.ToPlace).
                Where(n=>n.Name.Contains(text) || n.ToPlace.Name.Contains(text) || n.FromPlace.Name.Contains(text)).
                ToListAsync();
        }

        // GET: api/Tours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetTour(int id)
        {
            var tour = await _context.Tours.FindAsync(id);

            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        [HttpGet("GetTime")]
        public async Task<ActionResult<DateTime>> GetTime()
        {
            
            return DateTime.Now;
        }

        [HttpGet("{tourId}/Comments")]
        public async Task<ActionResult<IEnumerable<TourComment>>> GetTourComments(int tourId)
        {
            var tour = await _context.Tours.Include(n=>n.TourComments).ThenInclude(n=>n.Author).FirstOrDefaultAsync(n=>n.Id == tourId);

            if (tour == null)
            {
                return NotFound();
            }

            return tour.TourComments.ToList();
        }
        
        [HttpPost("{tourId}/Comment")]
        public async Task<ActionResult<TourComment>> AddTourComment(int tourId,TourComment tourComment)
        {
            tourComment.TourId = tourId;
            _context.TourComments.Add(tourComment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TourCommentExists(tourComment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        
        

        // PUT: api/Tours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(int id, Tour tour)
        {
            if (id != tour.Id)
            {
                return BadRequest();
            }

            _context.Entry(tour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(id))
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

        // POST: api/Tours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(Tour tour)
        {
            _context.Tours.Add(tour);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TourExists(tour.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTour", new { id = tour.Id }, tour);
        }

        // DELETE: api/Tours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tour>> DeleteTour(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();

            return tour;
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }

        private bool TourCommentExists(int id)
        {
            return _context.TourComments.Any(e => e.Id == id);
        }
    }
}
