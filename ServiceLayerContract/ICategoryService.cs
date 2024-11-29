using CoreWCF;
using Entities;

namespace ServiceLayerContract;

[ServiceContract]
public interface ICategoryService
{
    [OperationContract]
    public Category Create(Category service);

    [OperationContract]
    public Category RetrieveById(int id);

    [OperationContract]
    public bool Update(Category categoryToUpdate);

    [OperationContract]
    public bool Delete(int id);
}