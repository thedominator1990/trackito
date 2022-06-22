using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class Exercise
    {
        public Exercise()
        {
            Sets = new HashSet<Set>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PrimaryMuscle { get; set; }

        public virtual ICollection<Set> Sets { get; set; }
    }
}
