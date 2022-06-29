using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.User;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public UsersController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var allUsers = await _context.Users.ToListAsync();

            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].LastViewedEatingId == null)
                {
                    allUsers[i].LastViewedEatingId = 1;
                }
                if (allUsers[i].LastViewedTrainingId == null)
                {
                    allUsers[i].LastViewedTrainingId = 1;
                }
            }

            var allUsersDTO = mapper.Map <IEnumerable<UserGetDTO>>(await _context.Users.ToListAsync());

            return Ok(allUsersDTO);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDTO>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if(user.LastViewedTrainingId == null)
            {
                user.LastViewedTrainingId = 1;
            }

            if(user.LastViewedEatingId == null)
            {
                user.LastViewedEatingId= 1; 
            }

            var userDTO = mapper.Map<UserGetDTO>(user);

            if (user == null)
            {
                return NotFound();
            }

            return userDTO;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            mapper.Map(userDTO, user);

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCreateDTO>> PostUser(UserCreateDTO userDTO)
        {
          var user = mapper.Map<User>(userDTO);
          if (_context.Users == null)
          {
              return Problem("Entity set 'TrackitoContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
