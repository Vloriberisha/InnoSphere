using Azure.Core;
using Bussiness_Logic_Layer.DTOs;
using Bussiness_Logic_Layer.Repositories;
using Bussiness_Logic_Layer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _repository;
        public JobsController(IJobRepository repository)
        {
            _repository = repository;
        }
        #region GET
        [HttpGet("get-job/{id}")]
        public async Task<IActionResult> GetJob(string id)
        {
            try
            {
                var job = await _repository.GetJob(id);
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-my-jobs/{id}")]
        public async Task<IActionResult> GetEmployersJobs(string id)
        {
            try
            {
                var jobs = await _repository.GetEmployersJobs(id);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }

        [HttpGet("get-jobs")]
        public async Task<IActionResult> GetPopularJobs()
        {
            try
            {
                var jobs = await _repository.GetJobs();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }

        [HttpGet("get-filtered-jobs")]
        public async Task<IActionResult> GetFilteredJobs([FromQuery] string? keyword,[FromQuery] string? location, [FromQuery] string? type)
        {
            try
            {
                var jobs = await _repository.GetFilteredJobs(keyword,location,type);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost("post-job")]
        public async Task<IActionResult> PostJob([FromBody] JobDTO request)
        {
            try
            {
                await _repository.PostJob(request);
                return Ok ("Job posted succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message );
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("delete-job/{id}")]
        public async Task<IActionResult> DeleteJob(string id)
        {
            try
            {
                await _repository.DeleteJob(id);
                return Ok("Removed succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }
        #endregion

    }
}
