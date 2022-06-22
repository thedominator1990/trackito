using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.Sets;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public SetsController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Sets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetGetDTO>>> GetSets()
        {
            var allExercise = await _context.Exercises.ToListAsync();
            var allSets = await _context.Sets.ToListAsync();
            for (int i = 0; i < allSets.Count; i++)
            {
                for(int x =0; x<allExercise.Count; x++)
                {
                    if(allSets[i].ExerciseId == allExercise[x].Id )
                    {
                        allSets[i].exerciseName = allExercise[x].Name;
                    }
                }

            }

            var setDTO = mapper.Map<IEnumerable<SetGetDTO>>(allSets);
            return Ok(setDTO) ;
        }

        // GET: api/Sets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetGetDTO>> GetSet(int id)
        {
         
            var set = await _context.Sets.FindAsync(id);

            if (set == null)
            {
                return NotFound();
            }
            var setDTO = mapper.Map<SetGetDTO>(set);

            return setDTO;
        }

        // PUT: api/Sets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(int id, SetUpdateDTO setDTO)
        {
            Console.Write(id);
            Console.Write(setDTO);
            
            if (id != setDTO.Id)
            {
                return BadRequest();
            }

            var set = await _context.Sets.FindAsync(id);

            if(set == null)
            {
                return NotFound();
            }

            mapper.Map(setDTO, set);
            _context.Entry(@set).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Sets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Set>> PostSet(SetCreateDTO setDTO)
        {
            var set = mapper.Map<Set>(setDTO);

          if (_context.Sets == null)
          {
              return Problem("Entity set 'TrackitoContext.Sets'  is null.");
          }
            _context.Sets.Add(@set);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSet", new { id = @set.Id }, @set);
        }

        // DELETE: api/Sets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSet(int id)
        {
            if (_context.Sets == null)
            {
                return NotFound();
            }
            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(@set);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetExists(int id)
        {
            return (_context.Sets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
