using CoreWCF;
using Entities;

namespace ServiceLayerContract;

[ServiceContract]
public interface IAuditLogService
{
    public AuditLog Create(AuditLog service);

}