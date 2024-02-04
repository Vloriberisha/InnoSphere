﻿using Bussiness_Logic_Layer.DTOs;
using Bussiness_Logic_Layer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationRepository _repository;
        public JobApplicationsController(IJobApplicationRepository repository)
        {
            _repository = repository;
        }
        #region GET
        [HttpGet("get-jobseeker-applications/{id}")]
        public async Task<IActionResult> GetJobSeekerApplications(string id)
        {
            try
            {
                var application = await _repository.GetJobSeekerApplications(id);
                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
        [HttpGet("get-job-applications/{id}")]
        public async Task<IActionResult> GetJobApplications(string id)
        {
            try
            {
                var response = await _repository.GetJobApplications(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("has-applied")]
        public async Task<IActionResult> HasApplied([FromQuery] string userId, [FromQuery] string jobId)
        {
            try
            {
                var response = await _repository.HasApplied(userId, jobId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost("add-application")]
        public async Task<IActionResult> AddApplication([FromBody] JobApplicationDTO request)
        {
            try
            {
                await _repository.AddJobApplication(request);
                return Ok("Applied succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut("update-job-application/{id}")]
        public async Task<IActionResult> UpdateJobApplication(string id, [FromBody] string status)
        {
            try
            {
                await _repository.UpdateJobApplication(id, status);
                return Ok("Succesfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("remove-application/{id}")]
        public async Task<IActionResult> RemoveApplication(string id)
        {
            try
            {
                await _repository.RemoveApplication(id);
                return Ok( "Removed succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
