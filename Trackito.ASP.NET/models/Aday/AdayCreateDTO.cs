using System.ComponentModel.DataAnnotations;

namespace Trackito.ASP.NET.models.Aday
{
    public class AdayCreateDTO
    {
        [Required]
        public DateTime Date { get; set; }

        public string? Comment { get; set; }
        [Required]
        public int? IdUser { get; set; }
    }
}
