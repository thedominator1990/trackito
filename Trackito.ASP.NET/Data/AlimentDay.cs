using System;
using System.Collections.Generic;

namespace Trackito.ASP.NET.Data
{
    public partial class AlimentDay
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? DayId { get; set; }
        public int? AlimentId { get; set; }

        public string? alimentName { get; set; }

        public virtual Aliment? Aliment { get; set; }
        public virtual Aday? Day { get; set; }
    }
}
