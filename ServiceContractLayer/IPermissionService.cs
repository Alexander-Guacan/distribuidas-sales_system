using CoreWCF;
using Entities;

namespace ServiceContractLayer;

[ServiceContract]
public interface IPermissionService
{

    public Permission RetrieveById(int id);

}