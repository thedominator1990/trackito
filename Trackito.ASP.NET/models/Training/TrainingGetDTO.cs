using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.Exercise;
using Trackito.ASP.NET.models.Sets;

namespace Trackito.ASP.NET.models.Training
{
    public class TrainingGetDTO : BaseDTO
    {
        public DateTime DateTraining { get; set; }
        public string? Comment { get; set; }
        public int? IdUser { get; set; }

        public List<SetGetDTO> Sets { get; set; }

        public List<ExerciseGetDTO> exercises { get; set; }    


    }
}
