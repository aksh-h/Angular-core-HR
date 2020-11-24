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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public class RequisitionStaffingController : BaseController
    {
        private IRequisitionStaffing _requisitionstaffing;
        private IMasters _imasters;
        public RequisitionStaffingController(IRequisitionStaffing requisition, IMasters masters)
        {
            _requisitionstaffing = requisition;
            _imasters = masters;
        }



        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]
        public IActionResult RequisitionStaffing()
        {
            JsonObject response = new JsonObject();
            var RequisitionList = _requisitionstaffing.GetAllRequisitionStaffing();
            response.Add("RequisitionList", RequisitionList);
            GetMasterData(ref response);
            return Ok(response);
        }



        [HttpGet("{id}")]
        public IActionResult RequisitionStaffing(int id)
        {
            var result = _requisitionstaffing.GetRequisitionStaffingByCriteria(id);
            return Ok(result);
        }



        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult RequisitionStaffing([FromBody] TblRequisitionStaffing requisition)
        {

            var result = _requisitionstaffing.CreateRequisitionStaffing(requisition);
            return Ok(result);
        }



        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult UpdateRequisitionStaffing([FromBody] TblRequisitionStaffing requisition)
        {
            var result = _requisitionstaffing.UpdateRequisitionStaffing(requisition);
            return Ok(result);
        }



        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult CloseRequisitionStaffing([FromBody] TblRequisitionStaffing requisition)
        {
            var result = _requisitionstaffing.CloseRequisitionStaffing(requisition);
            return Ok(result);
        }



        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpDelete("{id}")]
        public IActionResult DeleteRequisitionStaffing(int id)
        {
            var result = _requisitionstaffing.DeleteRequisitionStaffing(id);
            return Ok(result);
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{id}")]
        public IActionResult GetApplicantsRequisitionStaffing(int id)
        {
            var result = _requisitionstaffing.GetAllApplicantsForRequisitionStaffing(id);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{id}")]
        public IActionResult ClientsContact(int id)
        {
            var result = _requisitionstaffing.GetClientsContactByCriteria(id);
            return Ok(result);
        }


       






        #region PrivateMethods

        private void GetMasterData(ref JsonObject keyValuePairs)
        {
            var DeptList = _imasters.GetAllDepartment();
            var DesigList = _imasters.GetAllDesignation();
            var SkillList = _imasters.GetAllSkill();
            var Clients = _imasters.GetAllClients();
            var EmployeeList = _imasters.GetEmployeesByCriteria(CurrentContext.EmployeeID);
            var WorkFlows = _imasters.GetAllWorkFlowClient();
            keyValuePairs.Add("DepartmentList", DeptList);
            keyValuePairs.Add("DesignationList", DesigList);
            keyValuePairs.Add("SkillList", SkillList);
            keyValuePairs.Add("EmployeeList", EmployeeList);
            keyValuePairs.Add("WorkFlows", WorkFlows);
            keyValuePairs.Add("Clients", Clients);
        }

        #endregion

    }
}

