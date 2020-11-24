using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using RestSharp;

namespace RecruitmentManagementAPI.Controllers
{   
    [Route("api/[controller]/[action]")]
    public class LoginController : BaseController
    {
        private ILogin _iLogin;
        public LoginController(ILogin login)
        {
            _iLogin = login;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]TblLogin tblLogin)
        {           
            var result = _iLogin.Authenticate(tblLogin.UserName, tblLogin.Password);          
            if (result != null)
            {
                string token = GenerateToken(tblLogin.EmployeeId.ToString(), result.Role.RoleName);
                JsonObject obj = new JsonObject();
                obj.Add("token", token);
                obj.Add("userid", result.EmployeeId);
                obj.Add("role", result.Role.RoleName);
                obj.Add("username", result.EmployeeName);
                return Ok(obj);
            }           
            return Ok(false);

        }

        [HttpPost]
        public IActionResult Client([FromBody] TblLoginClient tblLoginClient)
        {
            var result = _iLogin.AuthenticateClient(tblLoginClient.UserName, tblLoginClient.Password);
            if (result != null)
            {
                string token = GenerateToken(tblLoginClient.ContactId.ToString(), result.Client.ClientName);
                JsonObject obj = new JsonObject();
                obj.Add("token", token);
                obj.Add("userid", result.ContactId);
                obj.Add("role", "Client");
                obj.Add("username", result.ContactPerson);
                return Ok(obj);
            }
            return Ok(false);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult ApplicantStatusChanging()
        {
            var result = _iLogin.ApplicantStatusChanging();
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult GetCompletedInterviewCount()
        {
            var FinishedInterview = _iLogin.GetCompletedInterviewCount();
            var PendingInterview = _iLogin.GetPendingInterviewCount();
            JsonObject result = new JsonObject();
            result.Add("FinishedInterviews", FinishedInterview);
            result.Add("PendingInterviews", PendingInterview);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult InterviewCount()
        {
            var InterviewCount = _iLogin.TotalInterviewCount();
            var WeeklyCount = _iLogin.WeeklyInterviewCount();
            var MonthlyCount = _iLogin.MonthlyInterviewCount();
            var DailyCount = _iLogin.DailyInterviewCount();
            JsonObject result = new JsonObject();
            result.Add("InterviewCount", InterviewCount);
            result.Add("WeeklyCount", WeeklyCount);
            result.Add("MonthlyCount", MonthlyCount);
            result.Add("DailyCount", DailyCount);
            return Ok(result);

        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult RequisitionCount()
        {
            var PendingRequisitionCount = _iLogin.PendingRequisitionCount();
            var ApprovedRequisitionCount = _iLogin.ApprovedRequisitionCount();
            var CompletedRequisitionCount = _iLogin.CompletedRequisitionCount();
           
            JsonObject result = new JsonObject();
            result.Add("PendingRequisitionCount", PendingRequisitionCount);
            result.Add("ApprovedRequisitionCount", ApprovedRequisitionCount);
            result.Add("CompletedRequisitionCount", CompletedRequisitionCount);
            return Ok(result);

        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult RequisitionAssigned()
        {
            var StaffingRequisitionAssignedTo = _iLogin.StaffingRequisitionAssignedTo();
            var RecruiterRequisitionAssignedTo = _iLogin.RecruiterRequisitionAssignedTo();
            JsonObject result = new JsonObject();
            result.Add("StaffingRequisitionAssignedTo", StaffingRequisitionAssignedTo);
            result.Add("RecruiterRequisitionAssignedTo", RecruiterRequisitionAssignedTo);
            return Ok(result);

        }







    }
}
