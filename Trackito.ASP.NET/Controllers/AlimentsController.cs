using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.Aliments;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentsController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public AlimentsController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Aliments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlimentGetDTO>>> GetAliments()
        {
       

          var alimentDTO = mapper.Map<IEnumerable<AlimentGetDTO>>(await _context.Aliments.ToListAsync());

          return Ok(alimentDTO);
        }

        // GET: api/Aliments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlimentGetDTO>> GetAliment(int id)
        {
          
            var aliment = await _context.Aliments.FindAsync(id);

            if (aliment == null)
            {
                return NotFound();
            }

            var alimentDTO = mapper.Map<AlimentGetDTO>(aliment);

            return alimentDTO;
        }

        // PUT: api/Aliments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAliment(int id, AlimentUpdateDTO alimentDTO)
        {
            if (id != alimentDTO.Id)
            {
                return BadRequest();
            }
            var aliment = await _context.Aliments.FindAsync(id);

            if(aliment == null)
            {
                return NotFound();
            }

            mapper.Map(alimentDTO, aliment);
            _context.Entry(aliment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentExists(id))
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

        // POST: api/Aliments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aliment>> PostAliment(AlimentCreateDTO alimentDTO)
        {
            var aliment = mapper.Map<Aliment>(alimentDTO);
            Console.Write(alimentDTO.ToString());
          if (_context.Aliments == null)
          {
              return Problem("Entity set 'TrackitoContext.Aliments'  is null.");
          }
            _context.Aliments.Add(aliment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAliment", new { id = aliment.Id }, aliment);
        }

        // DELETE: api/Aliments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAliment(int id)
        {
            if (_context.Aliments == null)
            {
                return NotFound();
            }
            var aliment = await _context.Aliments.FindAsync(id);
            if (aliment == null)
            {
                return NotFound();
            }

            _context.Aliments.Remove(aliment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlimentExists(int id)
        {
            return (_context.Aliments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
