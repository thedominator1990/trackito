using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class Aday
    {
        public Aday()
        {
            AlimentDays = new HashSet<AlimentDay>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        public int? IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
        public virtual ICollection<AlimentDay> AlimentDays { get; set; }
    }
}
