namespace Trackito.ASP.NET.models.Sets
{
    public class SetUpdateDTO : BaseDTO
    {
        public int? Repetition { get; set; }
        public int? Weight { get; set; }

        public string? ExerciseName { get; set; }
    }
}
