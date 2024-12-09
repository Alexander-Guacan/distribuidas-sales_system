using BusinessLogicLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayerContract;

namespace RestServiceLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FailedLoginAttemptController : IFailedLoginAttemptService
    {
        [HttpPost]
        public FailedLoginAttempt Create(FailedLoginAttempt attempt)
        {
            var attemptLogic = new FailedLoginAttemptLogic();
            var newAttempt = attemptLogic.Create(attempt);
            return newAttempt;
        }

        [HttpGet("{id}")]
        public FailedLoginAttempt RetrieveById(int id)
        {
            var attemptLogic = new FailedLoginAttemptLogic();
            var attemptRetrieved = attemptLogic.RetrieveById(id);
            return attemptRetrieved;
        }

        [HttpPut]
        public bool Update(FailedLoginAttempt attemptToUpdate)
        {
            var attemptLogic = new FailedLoginAttemptLogic();
            var isUpdated = attemptLogic.Update(attemptToUpdate);
            return isUpdated;
        }
    }
}
