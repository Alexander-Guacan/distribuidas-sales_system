
using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContractLayer;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ICategoryService
    {
        [HttpPost]
        public Category Create(Category category)
        {
            var categoryLogic = new CategoryLogic();
            var newCategory = categoryLogic.Create(category);
            return newCategory;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var categoryLogic = new CategoryLogic();
            var isDeleted = categoryLogic.Delete(id);
            return isDeleted;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            var categoryLogic = new CategoryLogic();
            var categories = categoryLogic.GetCategories();
            return categories;
        }

        [HttpGet("{id}")]
        public Category RetrieveById(int id)
        {
            var categoryLogic = new CategoryLogic();
            var categoryRetrieved = categoryLogic.RetrieveById(id);
            return categoryRetrieved;
        }

        [HttpPut]
        public bool Update(Category categoryToUpdate)
        {
            var categoryLogic = new CategoryLogic();
            var isUpdated = categoryLogic.Update(categoryToUpdate);
            return isUpdated;
        }
    }
}