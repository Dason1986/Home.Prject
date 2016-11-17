namespace HomeApplication.Interceptors
{
    public interface IUserManager
    {
        DomainModel.UserAgg.UserProfile GetCurrentUser();
    }
    public class UserManager : IUserManager
    {
        public DomainModel.UserAgg.UserProfile GetCurrentUser()
        {
            return new DomainModel.UserAgg.UserProfile();
        }
    }
}