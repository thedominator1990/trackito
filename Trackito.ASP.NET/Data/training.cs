using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class training
    {
        public training()
        {
            Sets = new HashSet<Set>();
        }

        public int Id { get; set; }
        public DateTime DateTraining { get; set; }
        public string? Comment { get; set; }
        public int? IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
        public virtual ICollection<Set> Sets { get; set; }
    }
}
