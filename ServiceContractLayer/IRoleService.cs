using CoreWCF;
using Entities;

namespace ServiceContractLayer;

[ServiceContract]
public interface IRoleService
{

    public Role RetrieveById(int id);

}