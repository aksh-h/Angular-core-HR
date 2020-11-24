using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using RestSharp;

namespace RecruitmentManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicantStaffingController : ControllerBase
    {
        private IApplicantStaffing _applicantstaffing;
        private IMasters _imasters;
        private IRequisitionStaffing _requisitionstaffing;

        public ApplicantStaffingController(IRequisitionStaffing requisition, IApplicantStaffing applicant, IMasters masters)
        {
            _requisitionstaffing = requisition;
            _applicantstaffing = applicant;
            _imasters = masters;

        }
  
        #region Applicant 



        [HttpGet("{id}")]
        public async Task<IActionResult>  Applicants(int id)
        {

            TblApplicants result =await _applicantstaffing.GetApplicantsByCriteria(id);
            return Ok(result);

        }


        [HttpPost]
        public IActionResult Applicant([FromBody] TblApplicants tblApplicants)
        {

            int result = _applicantstaffing.CreateApplicants(tblApplicants);
            return Ok(result);

        }

        [HttpPut]
        public IActionResult UpdateApplicant([FromBody] TblApplicants tblApplicants)
        {

            int result = _applicantstaffing.UpdateApplicants(tblApplicants);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteApplicant(int id)
        {

            int result = _applicantstaffing.DeleteApplicants(id);
            return Ok(result);

        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{applicantId}")]
        public IActionResult Interviews(long applicantId)
        {
            return Ok(_applicantstaffing.GetAllInterviews(applicantId));
        }

        // [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult Applicant()
        {
            JsonObject response = new JsonObject();
            var Applicants = _applicantstaffing.GetAllApplicant();
            response.Add("Applicants", Applicants);
            GetMasterData(ref response);
            return Ok(response);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{id}")]
        public IActionResult ShortListedApplicant(int id)
        {
            JsonObject result = new JsonObject();
            var applicants = _applicantstaffing.GetRequisitionApplicantDetails(id);
            result.Add("InterviewDetails", _applicantstaffing.GetAllApplicantInterviewDetails(id));
            result.Add("ApplicantDetails", applicants);
            result.Add("InterviewerList", _applicantstaffing.GetAllInterviewer());
            result.Add("PanelList", _imasters.GetAllPanelGroup());
            result.Add("SalaryTemplates", _imasters.GetAllSalaryTemplates());
            return Ok(result);
        }
       

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult ShortListedApplicant()
        {
            return Ok(_applicantstaffing.GetAllShortListedApplicant());
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult ShortListedApplicantClient()
        {
            return Ok(_applicantstaffing.GetAllShortListedApplicantClient());
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult SelectedApplicant()
        {
            return Ok(_applicantstaffing.GetAllSelectedApplicant());
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult RejectedApplicant()
        {
            return Ok(_applicantstaffing.GetAllRejectedApplicant());
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult BlackListedApplicant()
        {
            return Ok(_applicantstaffing.GetAllBlackListedApplicant());
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult AddInterviewApplicant([FromBody] TblInterviewManagementStaffing tblInterviewManagement)
        {

            int result = _applicantstaffing.SaveInterviewDetails(tblInterviewManagement);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult UpdateInterviewApplicant([FromBody] TblInterviewManagementStaffing tblInterviewManagement)
        {
            int result = _applicantstaffing.UpdateInterview(tblInterviewManagement);
            return Ok(result);
        }


        //[ServiceFilter(typeof(ActionFilter), Order = 1)]
        //[HttpPost]
        //public IActionResult UpdateInterview([FromBody] TblInterviewManagementStaffing tblInterviewManagement)
        //{
        //    int result = _applicantstaffing.UpdateInterview(tblInterviewManagement);
        //    return Ok(result);
        //}

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult UpdateInterview([FromBody] TblInterviewManagementStaffing tblInterviewManagement)
        {
            int result = _applicantstaffing.UpdateInterview(tblInterviewManagement);
            return Ok(result);
        }

        
        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult UpdateStatusOnStaffingFeedback([FromBody] TblInterviewManagementStaffing tblInterviewManagement)
        {
            int result = _applicantstaffing.UpdateStatusOnStaffingFeedback(tblInterviewManagement);
            return Ok(result);
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpDelete("{id}")]
        public IActionResult CancelInterview(long id)
        {
            int result = _applicantstaffing.CancelInterview(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetInterviewById(int id)
        {

            TblInterviewManagement result = _applicantstaffing.GetInterviewById(id);
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetApplicantJoiningDetails(int id)
        {
            var result = _applicantstaffing.GetApplicantJoiningDetails(id);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult CreateApplicantJoining([FromBody] TblSelectedApplicantsStaffing tblSelectedApplicants)
        {
            int result = _applicantstaffing.CreateApplicantJoining(tblSelectedApplicants);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult UpdateApplicantJoining([FromBody] TblSelectedApplicantsStaffing tblSelectedApplicants)
        {
            long result = _applicantstaffing.UpdateApplicantJoining(tblSelectedApplicants);
            return Ok(result);
        }


        #endregion

        #region Requisition

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{IsCancelled}")]
        public IActionResult Requisition(bool IsCancelled = true)
        {
            return Ok(_requisitionstaffing.GetAllRequisitionStaffing(IsCancelled));
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult Requisition([FromBody] JsonObject _object)
        {
            return Ok(_requisitionstaffing.MapRequisitionStaffingToApplicant(Convert.ToInt64(_object["RequisitionID"]), Convert.ToInt64(_object["ApplicantID"]), false));
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult Requisition([FromBody] TblApplicantRequisitionStaffing tblApplicantRequisition)
        {
            return Ok(_requisitionstaffing.UpdateRequisitionStaffingToApplicant(tblApplicantRequisition));
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

