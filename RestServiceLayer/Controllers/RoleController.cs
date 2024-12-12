using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContractLayer;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : IRoleService
    {
        [HttpGet("{id}")]
        public Role RetrieveById(int id)
        {
            var roleLogic = new RoleLogic();
            var roleRetrieved = roleLogic.RetrieveById(id);
            return roleRetrieved;
        }
    }
}
