using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.AlimentDays;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentDaysController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public AlimentDaysController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/AlimentDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlimentDayGetDTO>>> GetAlimentDays()

        {
            var alimentDays = await _context.AlimentDays.ToListAsync(); 
            var aliments = await _context.Aliments.ToListAsync();

           

            
            if(aliments != null || alimentDays != null)
            {
                for (int x = 0; x < alimentDays.Count; x++)
                {
                    for (int i = 0; i < aliments.Count; i++)
                    {
                        if (aliments[i].Id == alimentDays[x].AlimentId)
                        {
                            alimentDays[x].alimentName = aliments[i].Name;
                        }
                    }

                }


            }
            

            var alimentdayDTO = mapper.Map<IEnumerable<AlimentDayGetDTO>>(alimentDays);

            return Ok(alimentdayDTO);
        }

        // GET: api/AlimentDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlimentDayGetDTO>> GetAlimentDay(int id)
        {
          
            var alimentDay = await _context.AlimentDays.FindAsync(id);

            if (alimentDay == null)
            {
                return NotFound();
            }
            var alimentDayDTO = mapper.Map<AlimentDayGetDTO>(alimentDay);

            return alimentDayDTO;
        }

        // PUT: api/AlimentDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlimentDay(int id, AlimentDayUpdateDTO alimentDayDTO)
        {
            if (id != alimentDayDTO.Id)
            {
                return BadRequest();
            }
            var alimentDay = _context.AlimentDays.Find(id);

            if(alimentDay == null)
            {
                return NotFound();
            }

            mapper.Map(alimentDayDTO, alimentDay);

            _context.Entry(alimentDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentDayExists(id))
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

        // POST: api/AlimentDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlimentDay>> PostAlimentDay(AlimentDayCreateDTO alimentDayDTO)
        {
          var alimentDay = mapper.Map<AlimentDay>(alimentDayDTO);
          if (_context.AlimentDays == null)
          {
              return Problem("Entity set 'TrackitoContext.AlimentDays'  is null.");
          }
            _context.AlimentDays.Add(alimentDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlimentDay", new { id = alimentDay.Id }, alimentDay);
        }

        // DELETE: api/AlimentDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlimentDay(int id)
        {
            if (_context.AlimentDays == null)
            {
                return NotFound();
            }
            var alimentDay = await _context.AlimentDays.FindAsync(id);
            if (alimentDay == null)
            {
                return NotFound();
            }

            _context.AlimentDays.Remove(alimentDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlimentDayExists(int id)
        {
            return (_context.AlimentDays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
