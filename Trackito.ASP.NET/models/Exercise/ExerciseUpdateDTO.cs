namespace Trackito.ASP.NET.models.Exercise
{
    public class ExerciseUpdateDTO : BaseDTO
    {
        public string Name { get; set; } 
        public string? PrimaryMuscle { get; set; }
    }
}
