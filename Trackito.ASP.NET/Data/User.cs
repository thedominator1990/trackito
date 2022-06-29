using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class User
    {
        public User()
        {
            Adays = new HashSet<Aday>();
            training = new HashSet<training>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int? LastViewedTrainingId { get; set; }        

        public int? LastViewedEatingId { get; set; }

        public virtual ICollection<Aday> Adays { get; set; }
        public virtual ICollection<training> training { get; set; }
    }
}
