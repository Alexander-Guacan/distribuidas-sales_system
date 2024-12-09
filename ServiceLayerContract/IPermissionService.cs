using CoreWCF;
using Entities;

namespace ServiceLayerContract;

[ServiceContract]
public interface IPermissionService
{

    public Permission RetrieveById(int id);

}