using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class Set
    {
        public int Id { get; set; }
        public int? Repetition { get; set; }
        public int? Weight { get; set; }
        public int? TrainingId { get; set; }
        public int? ExerciseId { get; set; }

        public string? exerciseName { get; set; }

        public virtual Exercise? Exercise { get; set; }
        public virtual training? Training { get; set; }
    }
}
