using CoreWCF;
using Entities;

namespace ServiceLayerContract;

[ServiceContract]
public interface IRoleService
{

    public Role RetrieveById(int id);

}