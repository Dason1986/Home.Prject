using System.ServiceModel;

namespace HomeApplication.Services
{
    /// <summary>
    ///
    /// </summary>
    [System.ServiceModel.ServiceContract]
    public interface IPMSService
    {
        [OperationContract]
        bool ValidateUser(string name, string pass);
    }
}