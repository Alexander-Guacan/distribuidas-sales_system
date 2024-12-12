using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContractLayer;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : IProductService
    {
        [HttpPost]
        public Product Create(Product product)
        {
            var productLogic = new ProductLogic();
            var newProduct = productLogic.Create(product);
            return newProduct;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var productLogic = new ProductLogic();
            var isDeleted = productLogic.Delete(id);
            return isDeleted;
        }

        [HttpGet("CategoryId/{categoryId}")]
        public List<Product> FilterByCategoryId(int categoryId)
        {
            var productLogic = new ProductLogic();
            var productsFiltered = productLogic.FilterByCategoryId(categoryId);
            return productsFiltered;
        }

        [HttpGet("{id}")]
        public Product RetrieveById(int id)
        {
            var productLogic = new ProductLogic();
            var productRetrieved = productLogic.RetrieveById(id);
            return productRetrieved;
        }

        [HttpPut]
        public bool Update(Product productToUpdate)
        {
            var productLogic = new ProductLogic();
            var isUpdated = productLogic.Update(productToUpdate);
            return isUpdated;
        }
    }
}