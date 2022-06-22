using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class Aliment
    {
        public Aliment()
        {
            AlimentDays = new HashSet<AlimentDay>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Unit { get; set; }
        public int Quantity { get; set; }
        public int? Calories { get; set; }
        public int? Protein { get; set; }
        public int? Fat { get; set; }
        public int? Carbs { get; set; }

        public virtual ICollection<AlimentDay> AlimentDays { get; set; }
    }
}
