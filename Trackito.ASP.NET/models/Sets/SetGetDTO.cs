namespace Trackito.ASP.NET.models.Sets
{
    public class SetGetDTO : BaseDTO
    {
        public int? Repetition { get; set; }
        public int? Weight { get; set; }
        public int? TrainingId { get; set; }
        public int? ExerciseId { get; set; }

        public string? exerciseName { get; set; }
    }
}
