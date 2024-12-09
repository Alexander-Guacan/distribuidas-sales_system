using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayerContract;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : IUserService
    {
        [HttpPost]
        public User Create(User user)
        {
            var userLogic = new UserLogic();
            var newUser = userLogic.Create(user);
            return newUser;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var userLogic = new UserLogic();
            var isDeleted = userLogic.Delete(id);
            return isDeleted;
        }

        [HttpGet("{id}")]
        public User RetrieveById(int id)
        {
            var userLogic = new UserLogic();
            var userRetrieved = userLogic.RetrieveById(id);
            return userRetrieved;
        }

        [HttpPut]
        public bool Update(User userToUpdate)
        {
            var userLogic = new UserLogic();
            var isUpdated = userLogic.Update(userToUpdate);
            return isUpdated;
        }
    }
}
