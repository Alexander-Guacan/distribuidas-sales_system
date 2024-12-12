using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContractLayer;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditLogController : IAuditLogService
    {
        [HttpPost]
        public AuditLog Create(AuditLog log)
        {
            var auditLogLogic = new AuditLogLogic();
            var newLog = auditLogLogic.Create(log);
            return newLog;
        }
    }
}
