using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.Exercise;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public ExercisesController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseGetDTO>>> GetExercises()
        {
            var exerciseDTO = mapper.Map<IEnumerable<ExerciseGetDTO>>(await _context.Exercises.ToListAsync());
            return Ok(exerciseDTO);
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseGetDTO>> GetExercise(int id)
        {
          
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseDTO = mapper.Map<ExerciseGetDTO>(exercise);

            return exerciseDTO;
        }

        // PUT: api/Exercises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, ExerciseUpdateDTO exerciseDTO)
        {
            if (id != exerciseDTO.Id)
            {
                return BadRequest();
            }

            var exercise = await _context.Exercises.FindAsync(id);

            if(exercise == null)
            {
                return NotFound();
            }
            mapper.Map(exerciseDTO, exercise);
            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // POST: api/Exercises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(ExerciseCreateDTO exerciseDTO)
        {
            var exercise = mapper.Map<Exercise>(exerciseDTO);
          if (_context.Exercises == null)
          {
              return Problem("Entity set 'TrackitoContext.Exercises'  is null.");
          }
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new { id = exercise.Id }, exercise);
        }

        // DELETE: api/Exercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            if (_context.Exercises == null)
            {
                return NotFound();
            }
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return (_context.Exercises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
