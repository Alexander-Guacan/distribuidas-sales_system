using CoreWCF;
using Entities;

namespace ServiceLayerContract;

[ServiceContract]
public interface IProductService
{
    [OperationContract]
    public Product Create(Product product);

    [OperationContract]
    public Product RetrieveById(int id);

    [OperationContract]
    public bool Update(Product productToUpdate);

    [OperationContract]
    public bool Delete(int id);

    [OperationContract]
    public List<Product> FilterByCategoryId(int categoryId);
}
