namespace HomeApplication.Services
{
    public abstract class ServiceImpl
    {
        public abstract string ServiceName { get; }
        protected virtual DomainModel.UserAgg.UserProfile GetCurrentUser()
        {
            return null;
        }
    }
}
