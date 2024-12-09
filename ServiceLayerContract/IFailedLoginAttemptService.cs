using CoreWCF;
using Entities;

namespace ServiceLayerContract;

[ServiceContract]
public interface IFailedLoginAttemptService
{
    public FailedLoginAttempt Create(FailedLoginAttempt service);

    public FailedLoginAttempt RetrieveById(int id);

    public bool Update(FailedLoginAttempt failedLoginAttemptToUpdate);
}