namespace Trackito.ASP.NET.models.AlimentDays
{
    public class AlimentDayGetDTO : BaseDTO
    {
        public int Quantity { get; set; }
        public int? DayId { get; set; }
        public int? AlimentId { get; set; }

        public string AlimentName { get; set; }
    }
}
