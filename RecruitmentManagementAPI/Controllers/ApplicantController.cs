using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecruitmentManagementDataAccess;
using RecruitmentManagementDataAccess.Helpers;
using RecruitmentManagementDataAccess.ModelExtensions;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RecruitmentManagementAPI.Controllers
{
    //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public class ApplicantController : BaseController
    {
        private IApplicant _applicant;
        private IMasters _imasters;
        private IRequisition _requisition;

        public ApplicantController(IRequisition requisition, IApplicant applicant, IMasters masters)
        {
            _requisition = requisition;
            _applicant = applicant;
            _imasters = masters;

        }
       
        #region Applicant 

        [HttpGet("{id}")]
        public IActionResult Applicants(int id)
        {
            TblApplicants result = _applicant.GetApplicantsByCriteria(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Applicant()
        {
            var model = JsonConvert.DeserializeObject<TblApplicants>(Request.Form.Where(r => r.Key == "ApplicantModel").FirstOrDefault().Value);
            string path = _applicant.ImportExcel(Request.Form.Files[0]);
            model.ApplicantResume = path;
            int result = _applicant.CreateApplicants(model);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateApplicant()
        {
            var model = JsonConvert.DeserializeObject<TblApplicants>(Request.Form.Where(r => r.Key == "ApplicantModel").FirstOrDefault().Value);
            string path = _applicant.ImportExcel(Request.Form.Files[0]);
            model.ApplicantResume = path;
            int result = _applicant.UpdateApplicants(model);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteApplicant(int id)
        {
            int result = _applicant.DeleteApplicants(id);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{applicantId}")]
        public IActionResult Interviews(long applicantId)
        {
            return Ok(_applicant.GetAllInterviews(applicantId));
        }

        // [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult Applicants()
        {
           JsonObject response = new JsonObject();
           var Applicants = _applicant.GetAllApplicant();
            response.Add("Applicants", Applicants);
           GetMasterData(ref response);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult ApplicantFromRequisition(int RequisitionId)
        {
            JsonObject response = new JsonObject();
            var Applicants = _applicant.GetAllApplicantFromRequisition(RequisitionId);
            response.Add("Applicants", Applicants);
            GetMasterData(ref response);
            return Ok(response);
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{id}")]
        public IActionResult ShortListedApplicant(int id)
        {
            JsonObject result = new JsonObject();
            var applicants = _applicant.GetRequisitionApplicantDetails(id);
            result.Add("InterviewDetails", _applicant.GetAllApplicantInterviewDetails(id));
            result.Add("ApplicantDetails", applicants);
            result.Add("InterviewerList", _applicant.GetAllInterviewer());           
            result.Add("PanelList", _imasters.GetAllPanelGroup());
            result.Add("SalaryTemplates", _imasters.GetAllSalaryTemplates());
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult ShortListedApplicant()
        {
            return Ok(_applicant.GetAllShortListedApplicant());
        }
        //[ServiceFilter(typeof(ActionFilter), Order = 1)]
        //[HttpGet]
        //public IActionResult ShortListedApplicantClient()
        //{
        //    return Ok(_applicant.GetAllShortListedApplicantClient());
        //}

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult SelectedApplicant()
        {
            return Ok(_applicant.GetAllSelectedApplicant());
        }
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult RejectedApplicant()
        {
            return Ok(_applicant.GetAllRejectedApplicant());
        }
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult BlackListedApplicant()
        {
            return Ok(_applicant.GetAllBlackListedApplicant());
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult AddInterviewApplicant([FromBody] TblInterviewManagement tblInterviewManagement)
        {
            int result = _applicant.SaveInterviewDetails(tblInterviewManagement);
            return Ok(result);
        }
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult UpdateInterviewApplicant([FromBody] TblInterviewManagement tblInterviewManagement)
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
        [HttpPost]
        public IActionResult CreateInternalApplicantJoining([FromBody] TblSelectedApplicants tblSelectedApplicants)
        {
            int result = _applicant.CreateInternalApplicantJoining(tblSelectedApplicants);
            return Ok(result);
        }
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult UpdateInternalApplicantJoining([FromBody] TblSelectedApplicants tblSelectedApplicants)
        {
            long result = _applicant.UpdateInternalApplicantJoining(tblSelectedApplicants);
            return Ok(result);
        }
        #endregion

        #region Requisition
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{IsCancelled}")]
        public IActionResult Requisition(bool IsCancelled = true)
        {
            return Ok(_requisition.GetAllRequisition(IsCancelled));
        }
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult Requisition([FromBody] JsonObject _object)
        {
            // return Ok(_requisition.MapRequisitionToApplicant(Convert.ToInt64(_object["RequisitionID"]), Convert.ToInt64(_object["ApplicantID"]), Convert.ToBoolean(_object["IsInternal"])));
            return Ok(_requisition.MapRequisitionToApplicant(Convert.ToInt64(_object["RequisitionID"]), Convert.ToInt64(_object["ApplicantID"]), true));
        }
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult Requisition([FromBody] TblApplicantRequisition tblApplicantRequisition)
        {
            return Ok(_requisition.UpdateRequisitionToApplicant(tblApplicantRequisition));
        }

        #endregion

        #region Resume Upload
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult ResumeImport()
        {
            var model =  JsonConvert.DeserializeObject<ResumeUpload>(Request.Form.Where(r => r.Key == "ResumeUploadModel").FirstOrDefault().Value);
            string result = _applicant.ImportExcel(Request.Form.Files[0]);
            //return Json(result);
            _applicant.ResumeImport(result,model);
            return Ok(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult SalaryImport()
        {
            string result = _applicant.ImportExcel(Request.Form.Files[0]);
            //return Json(result);
            return Ok(result);
        }

        #endregion

        #region PrivateMethods
        private void GetMasterData(ref JsonObject keyValuePairs)
        {
            var Locations = _imasters.GetAllLocations();
            var Qualifications = _imasters.GetAllQualifications();
            var NoticePeriods = _imasters.GetAllNoticePeriod();
            var ApplicantActiveFrom = _imasters.GetAllApplicantActiveFrom();
            var JobTypes = _imasters.GetAllJobTypes();
            var SkillList = _imasters.GetAllSkill();
            keyValuePairs.Add("Locations", Locations);
            keyValuePairs.Add("Qualifications", Qualifications);
            keyValuePairs.Add("NoticePeriods", NoticePeriods);
            keyValuePairs.Add("ApplicantActiveFrom", ApplicantActiveFrom);
            keyValuePairs.Add("JobTypes", JobTypes);
            keyValuePairs.Add("SkillList", SkillList);
        }
        #endregion
    }
}