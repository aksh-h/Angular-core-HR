using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RecruitmentManagementDataAccess;
using RecruitmentManagementDataAccess.Helpers;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using RestSharp;
using System.Collections.Generic;

namespace RecruitmentManagementAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployee _employee;
        private IApplicant _applicant;
        public EmployeeController(IEmployee employee, IApplicant applicant)
        {
            _applicant = applicant;
            _employee = employee;
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult Employee()
        {
            return Ok(_employee.GetAllInterviews());
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult UpdateInterview([FromBody]TblInterviewManagement tblInterviewManagement)
        {
            int result = _applicant.UpdateInterview(tblInterviewManagement);
            return Ok(result);
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpDelete("{id}")]
        public IActionResult CancelInterview(long id)
        {
            int result = _applicant.CancelInterview(id);
            return Ok(result);
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult GetAllClientInterviews()
        {
            //var result=_employee.GetAllClientInterviews();
            return Ok(_employee.GetAllClientInterviews());
            //return Ok(result);
         }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{id}")]
        public IActionResult GetInterviewHistory(long id)
        {
            //var result=_employee.GetAllClientInterviews();
            return Ok(_employee.GetAllInterviewsHistory(id));
            //return Ok(result);
        }

     



        //[ServiceFilter(typeof(ActionFilter), Order = 1)]
        // [HttpGet]
        // public IActionResult GetAllClientInterviews()
        // {
        //     return Ok(_employee.GetAllClientInterviews());
        // }



    }
}
