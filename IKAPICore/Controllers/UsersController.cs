using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKAPICore.Models;
using Newtonsoft.Json;

namespace IKAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly itkotdbContext _context;

        public UsersController(itkotdbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(n => n.Role).FirstOrDefaultAsync(n => n.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(User user)
        {
            user.RoleId = 1;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return JsonConvert.SerializeObject(user.Id);
        }


        [HttpGet("Authorization")]
        public async Task<ActionResult<User>> Authorization(string phone, string password)
        {
            var user = await _context.Users.Include(n => n.Role)
                .FirstOrDefaultAsync(n => n.Phone == phone && n.Password == password);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{userId}/Tours")]
        public async Task<IEnumerable<Tour>> GetUserTours(int userId)
        {
            var userTours = _context.UserTours.Where(n => n.UserId == userId)
                .Include(n => n.Tour).ThenInclude(n => n.FromPlace)
                 .Include(n => n.Tour).ThenInclude(n => n.ToPlace)
                .Include(n => n.Tour)
                .ThenInclude(n => n.TourAttractions).ThenInclude(n => n.Attraction).ThenInclude(n=>n.AttractionImages)
                
                .Include(n => n.Tour).ThenInclude(n=>n.Agent).ThenInclude(n=>n.Company);
            return userTours.Select(n => n.Tour).ToList();
        }

        [HttpPost("{userId}/Tours/{tourId}")]
        public async Task<ActionResult<User>> BookTour(int userId, int tourId)
        {
            var user = _context.Users.FirstOrDefault(n => n.Id == userId);
            var tour = _context.Tours.FirstOrDefault(n => n.Id == tourId);
            _context.UserTours.Add(new UserTour()
            {
                Tour = tour,
                TourId = tourId,
                User = user,
                UserId = userId
            });

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("{userId}/Tours/{tourId}/has")]
        public async Task<Boolean> IsBooked(int userId, int tourId)
        {
            var user = _context.Users.FirstOrDefault(n => n.Id == userId);
            var tour = _context.Tours.FirstOrDefault(n => n.Id == tourId);

            var userTour = await _context.UserTours.FirstOrDefaultAsync(n => n.TourId == tourId && n.UserId == userId);

            return userTour != null;
        }
        
        [HttpPost("{userId}/Tours/{tourId}/cancel")]
        public async Task<ActionResult> DeleteBook(int userId, int tourId)
        {
            var userTour = await _context.UserTours.FirstOrDefaultAsync(n => n.TourId == tourId && n.UserId == userId);
            if (userTour == null)
                return NotFound();

            _context.UserTours.Remove(userTour);
            _context.SaveChanges();
            return Ok();
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}