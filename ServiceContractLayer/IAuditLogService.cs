using CoreWCF;
using Entities;

namespace ServiceContractLayer;

[ServiceContract]
public interface IAuditLogService
{
    public AuditLog Create(AuditLog service);

}