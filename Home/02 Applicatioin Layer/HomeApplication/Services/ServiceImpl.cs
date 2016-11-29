namespace HomeApplication.Services
{
    public abstract class ServiceImpl
    {
        public abstract string ServiceName { get; }
        protected virtual Home.DomainModel.Aggregates.UserAgg.UserProfile GetCurrentUser()
        {
            return null;
        }
    }
}
