﻿using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireSort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private IDashboard _dashboard;
        private ApiResponseMessage apiResponseMessage = null;
        private readonly IResumeParsing _resumeParsing;
        public DashboardController(ILogger<HomeController> logger, IDashboard dashboard, IResumeParsing resumeParsing)
        {
            _logger = logger;
            _dashboard = dashboard;
            _resumeParsing = resumeParsing;
        }

        [HttpGet]
        [Route("departments")]
        public async Task<IActionResult> GetDepartment()
        {
            var response = await _dashboard.GetDepartment();
            return Ok(response);

        }
        
        [HttpGet]
        [Route("vacancies-department-wise")]
        public async Task<IActionResult> GetVacanciesDepartmentWise([FromQuery] int departId)
        {
            var response = await _dashboard.GetVacanciesDepartmentWise(departId);
            return Ok(response);
        }
        [HttpGet]
        [Route("department-and-vacancies-details")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int departId, int vacancyId)
        {
            var response = await _dashboard.GetDepartAndVacacyDetails(departId, vacancyId);
            return Ok(response);
        }
        [HttpGet]
        [Route("resume-list")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int departId, int vacancyId, bool isShortListedResume = false)
        {
            var response = await _dashboard.GetAllResumes(departId, vacancyId, isShortListedResume);
            return Ok(response);
        }
        [HttpGet]
        [Route("depart-vacancy-count")]
        public async Task<IActionResult> GetDepartmentVacancyCount()
        {
            var response = await _dashboard.GetDepartmentVacancyCount();
            return Ok(response);
        }
        [HttpGet]
        [Route("depart-vacancy-list")]
        public async Task<IActionResult> GetDepartmentJobs([FromQuery] int departId)
        {
            var response = await _dashboard.GetDepartmentJobs(departId);
            return Ok(response);
        }
        [HttpPost]
        [Route("uploadfile")]

        public Task UploadFiles(IFormFile file, int jobId)
        {
            //foreach (IFormFile file in files)
            //{
            if (file.Length > 0)
            {
                var result = _resumeParsing.ResumeUpload(file, jobId);
            }
            return Task.CompletedTask;
        }
        [HttpPost]
        [Route("check-resume-compatibility")]
        public async Task<string> GetResumeContent([FromQuery] int resumeId, int jobId)
        {
            return await _resumeParsing.resumeCheckCompatibility(resumeId, jobId);
        }
    }
}
