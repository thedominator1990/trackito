using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models;
using Trackito.ASP.NET.models.Exercise;
using Trackito.ASP.NET.models.Sets;
using Trackito.ASP.NET.models.Training;

namespace Trackito.ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class trainingsController : ControllerBase
    {
        private readonly TrackitoContext _context;
        private readonly IMapper mapper;

        public trainingsController(TrackitoContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/trainings
        /*
         [HttpGet]
         public async Task<ActionResult<List<TrainingGetDTO>>> Gettraining()
         {


            var sets = mapper.Map<List<SetGetDTO>>(await _context.Sets.ToListAsync());
          var trainingDTO = mapper.Map<List<TrainingGetDTO>>(await _context.training.ToListAsync());


           return Ok(trainingDTO);


  } *//*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> Gettraining()
        {


            // Get all the data from the database
            var trainings = await _context.training.ToListAsync();
            var Sets = await _context.Sets.ToListAsync();
            var Exercises = await _context.Exercises.ToListAsync();

            //Create a Expanded object list for each table we imported
            List<ExpandoObject> allTrainings = new List<ExpandoObject>();
            List<ExpandoObject> allSetsList = new List<ExpandoObject>();
            List<ExpandoObject> allExercise = new List<ExpandoObject>();

            //dynamic setList = new List<dynamic>;

            //

            for (int i = 0; i < trainings.Count; i++)
            {
                dynamic objt = new ExpandoObject();
                objt.id = trainings[i].Id;
                objt.date = trainings[i].DateTraining;
                objt.setList = new List<dynamic>();
                objt.setList.Add("test");
                objt.setList.Add(3);
                allTrainings.Add(objt);
            }
            
            //foreach (var training in trainingDTO) { }

            return allTrainings;


        }
        */
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingGetDTO>>> Gettraining()
        {
            var trainings = await _context.training.ToListAsync(); 
            var allExercise = await _context.Exercises.ToListAsync();
            var sets = await _context.Sets.ToListAsync();


            for (int i = 0; i < sets.Count; i++)
            {
                for (int x = 0; x < allExercise.Count; x++)
                {
                    if (sets[i].ExerciseId == allExercise[x].Id)
                    {
                        sets[i].exerciseName = allExercise[x].Name;
                    }
                }

            }

            

            var trainingDTO = mapper.Map<List<TrainingGetDTO>>(trainings);

           

            for (int i = 0; i < trainingDTO.Count; i++)
            {
                var allExerciseList = new List<ExerciseGetDTO>();

                for (int x = 0; x < trainingDTO[i].Sets.Count; x++)
                {
                    var currentExercise = await _context.Exercises.FindAsync(trainingDTO[i].Sets[x].ExerciseId);

                    var currentExerciseDTO = mapper.Map<ExerciseGetDTO>(currentExercise);

                    allExerciseList.Add(currentExerciseDTO);
                }
                List<ExerciseGetDTO> distingExercise = allExerciseList.GroupBy(p => p.Id).Select(g => g.First()).ToList();

                trainingDTO[i].exercises = distingExercise;

            }

            /*
            for (int x = 0; x < trainingDTO.Count; x++)
            { var exerciseNameList = new List<string>();
                for (int i = 0; i < trainingDTO[x].Sets.Count ; i++)
                {
                    if(trainingDTO[x].Sets[i].exerciseName != null)
                    {
                        exerciseNameList.Add(trainingDTO[x].Sets[i].exerciseName);
                    }
                    
                }
                exerciseNameList = exerciseNameList.Distinct().ToList();

                for (int y = 0; y < exerciseNameList.Count; y++)
                {
                    var newExercise = new exerciseWithSetList();
                    newExercise.name = exerciseNameList[y];

                    trainingDTO[x].exercises.Add(newExercise);

                }

                for (int z = 0; z < trainingDTO[x].Sets.Count; z++)
                {
                    for (int zz = 0; zz < trainingDTO[x].exercises.Count; zz++)
                    {
                        if (trainingDTO[x].exercises[zz].name == trainingDTO[x].Sets[z].exerciseName)
                        {
                            trainingDTO[x].exercises[zz].sets.Add(trainingDTO[x].Sets[z]);
                        }
                    }


                }
            }
         
            */
          
            /*
            for (int i = 0; i < trainings.Count; i++)
            {
                var training = mapper.Map<TrainingGetDTO>(trainings[i]);
                
                for (int x = 0; x < sets.Count; x++)
                {
                    if (sets[x].TrainingId == training.Id)
                    {
                        var set = mapper.Map<SetGetDTO>(sets[x]);
                            training.Sets.Add(set);
                        
                        
                    }

                }
                
                trainingDTO.Add(training);

            }
            */
            return trainingDTO;

        }
        

        // GET: api/trainings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingGetDTO>> Gettraining(int id)
        {
            var trainings = await _context.training.FindAsync(id);
            var allExercise = await _context.Exercises.ToListAsync();
            var sets = await _context.Sets.ToListAsync();
            for (int i = 0; i < sets.Count; i++)
            {
                for (int x = 0; x < allExercise.Count; x++)
                {
                    if (sets[i].ExerciseId == allExercise[x].Id)
                    {
                        sets[i].exerciseName = allExercise[x].Name;
                    }
                }

            }

            var trainingDTO = mapper.Map<TrainingGetDTO>(trainings);

            return trainingDTO;


        }

        // PUT: api/trainings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttraining(int id, TrainingUpdateDTO trainingDTO)
        {
            if (id != trainingDTO.Id)
            {
                return BadRequest();
            }

            var training = await _context.training.FindAsync(id);
            if(training == null)
            {
                return NotFound();
            }

            mapper.Map(trainingDTO, training);
            _context.Entry(training).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!trainingExists(id))
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

        // POST: api/trainings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<training>> Posttraining(TrainingCreateDTO trainingDTO)
        {
            var training = mapper.Map<training>(trainingDTO);
            var dateOfTraining = training.DateTraining.ToString().Substring(0, 10);
            var allTrainings = await _context.training.ToListAsync();

            for (int i = 0; i < allTrainings.Count; i++)
            {
                var dateOfEachTraining = allTrainings[i].DateTraining.ToString().Substring(0, 10);
                if (dateOfEachTraining == dateOfTraining && allTrainings[i].IdUser == training.IdUser)
                {
                    return Problem("there is already a training created for this day for this user");
                }

            }

          if (_context.training == null)
          {
              return Problem("Entity set 'TrackitoContext.training'  is null.");
          }
            _context.training.Add(training);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettraining", new { id = training.Id }, training);
        }

        // DELETE: api/trainings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetraining(int id)
        {
            if (_context.training == null)
            {
                return NotFound();
            }
            var training = await _context.training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            _context.training.Remove(training);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool trainingExists(int id)
        {
            return (_context.training?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
