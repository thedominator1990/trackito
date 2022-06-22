using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.AlimentDays;
using Trackito.ASP.NET.models.Aliments;

namespace Trackito.ASP.NET.models.Aday
{
    public class AdayGetOneDTO : BaseDTO
    {

       
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        public int? IdUser { get; set; }

        public List<AlimentGetDTO> aliments { get; set; }

        public virtual List<AlimentDayGetDTO>? AlimentDays { get; set; }
    }
}
