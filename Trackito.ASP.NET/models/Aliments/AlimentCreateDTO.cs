namespace Trackito.ASP.NET.models.Aliments
{
    public class AlimentCreateDTO
    {

        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
        public int Quantity { get; set; }
        public int? Calories { get; set; }
        public int? Protein { get; set; }
        public int? Fat { get; set; }
        public int? Carbs { get; set; }

        
    }
}
