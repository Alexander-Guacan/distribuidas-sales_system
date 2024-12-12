using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContractLayer;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : IPermissionService
    {
        [HttpGet("{id}")]
        public Permission RetrieveById(int id)
        {
            var permissionLogic = new PermissionLogic();
            var permissionRetrieved = permissionLogic.RetrieveById(id);
            return permissionRetrieved;
        }
    }
}
