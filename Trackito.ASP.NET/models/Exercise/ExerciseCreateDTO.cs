using System.ComponentModel.DataAnnotations;

namespace Trackito.ASP.NET.models.Exercise
{
    public class ExerciseCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string? PrimaryMuscle { get; set; }
    }
}
