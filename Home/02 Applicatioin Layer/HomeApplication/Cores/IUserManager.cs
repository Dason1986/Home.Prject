using Home.DomainModel.Aggregates.UserAgg;

namespace HomeApplication.Interceptors
{
    public interface IUserManager
    {
        UserProfile GetCurrentUser();
    }
    public class UserManager : IUserManager
    {
        public  UserProfile GetCurrentUser()
        {
            return new  UserProfile();
        }
    }


}