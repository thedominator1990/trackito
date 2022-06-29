namespace Trackito.ASP.NET.models.User
{
    public class UserGetDTO
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int? LastViewedTrainingId { get; set; }

        public int? LastViewedEatingId { get; set; }

    }
}
