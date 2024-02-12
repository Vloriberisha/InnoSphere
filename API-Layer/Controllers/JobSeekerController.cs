using Bussiness_Logic_Layer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerRepository _repository;
        public JobSeekerController(IJobSeekerRepository repository)
        {
            _repository = repository;
        }

        #region GET
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _repository.GetJobSeeker(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
