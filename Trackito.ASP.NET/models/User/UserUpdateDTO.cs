namespace Trackito.ASP.NET.models.User
{
    public class UserUpdateDTO : BaseDTO
    {

        public int LastViewedTrainingId { get; set; }

        public int LastViewedEatingId { get; set; }
    }
}
