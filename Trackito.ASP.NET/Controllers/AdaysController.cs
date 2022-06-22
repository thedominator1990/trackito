using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.Aday;
using Trackito.ASP.NET.models.AlimentDays;
using Trackito.ASP.NET.models.Aliments;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdaysController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public AdaysController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Adays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdayGetOneDTO>>> GetAdays()
        {

            /*
            var adays = mapper.Map<IEnumerable<AdayGetOneDTO>>(await _context.Adays.ToListAsync());

              return Ok(adays);*/

            var Adays = await _context.Adays.ToListAsync();
            var allAliments = await _context.Aliments.ToListAsync();
            var allAlimentsDays = await _context.AlimentDays.ToListAsync();
            
            for (int i = 0; i < allAlimentsDays.Count; i++)
            {
                for (int x = 0; x < allAliments.Count; x++)
                {
                    if (allAlimentsDays[i].AlimentId == allAliments[x].Id)
                    {
                        allAlimentsDays[i].alimentName = allAliments[x].Name;
                    }
                }

            }
            
            var AdayDTO = mapper.Map<List<AdayGetOneDTO>>(Adays);


            for (int i = 0; i < AdayDTO.Count; i++)
            {
                var allAlimentList = new List<AlimentGetDTO>();

                for (int x = 0; x < AdayDTO[i].AlimentDays.Count; x++)
                {
                    var currentAliment = await _context.Aliments.FindAsync(AdayDTO[i].AlimentDays[x].AlimentId);

                    var currentAlimentDTO = mapper.Map<AlimentGetDTO>(currentAliment);

                    allAlimentList.Add(currentAlimentDTO);
                }
                List<AlimentGetDTO> distingExercise = allAlimentList.GroupBy(p => p.Id).Select(g => g.First()).ToList();

                AdayDTO[i].aliments = distingExercise;

            }
            /*
             for (int i = 0; i < Adays.Count; i++)
             {
                 var aday = mapper.Map<AdayGetOneDTO>(Adays[i]);

                 for (int x = 0; x < allAlimentsDays.Count; x++)
                 {
                     if (allAlimentsDays[x].DayId == aday.Id)
                     {
                         //var set = mapper.Map<AlimentDayGetDTO>(allAlimentsDays[x]);
                         //aday.AlimentDays.Add(set);


                     }

                 }

                 AdayDTO.Add(aday);

             }
            */
            return AdayDTO;
        }

        // GET: api/Adays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdayGetOneDTO>> GetAday(int id)
        {
            var Adays = await _context.Adays.FindAsync(id);
            var allAliments = await _context.Aliments.ToListAsync();
            var allAlimentsDays = await _context.AlimentDays.ToListAsync();

            for (int i = 0; i < allAlimentsDays.Count; i++)
            {
                for (int x = 0; x < allAliments.Count; x++)
                {
                    if (allAlimentsDays[i].AlimentId == allAliments[x].Id)
                    {
                        allAlimentsDays[i].alimentName = allAliments[x].Name;
                    }
                }

            }
           
            var AdayDTO = mapper.Map<AdayGetOneDTO>(Adays);

            return AdayDTO;
        }

        // PUT: api/Adays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAday(int id, AdayUpdateDTO adayDTO)
        {
            if (id != adayDTO.Id)
            {
                return BadRequest();
            }

            var aday = await _context.Adays.FindAsync(id);

            if(aday == null)
            {
                return NotFound();
            }

            mapper.Map(adayDTO, aday);
            _context.Entry(aday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdayExists(id))
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

        // POST: api/Adays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdayCreateDTO>> PostAday(AdayCreateDTO adayDTO)
        {
            var aday = mapper.Map<Aday>(adayDTO);

            if (_context.Adays == null)
          {
              return Problem("Entity set 'TrackitoContext.Adays'  is null.");
          }
            _context.Adays.Add(aday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAday", new { id = aday.Id }, aday);
        }

        // DELETE: api/Adays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAday(int id)
        {
            if (_context.Adays == null)
            {
                return NotFound();
            }
            var aday = await _context.Adays.FindAsync(id);
            if (aday == null)
            {
                return NotFound();
            }

            _context.Adays.Remove(aday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdayExists(int id)
        {
            return (_context.Adays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
