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
    public class RequisitionController : BaseController
    {
        private IRequisition _requisition;
        private IMasters _imasters;
        public RequisitionController(IRequisition requisition, IMasters masters)
        {
            _requisition = requisition;
            _imasters = masters;
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet]       
        public IActionResult Requisition()
        {
            JsonObject response = new JsonObject();
            var RequisitionList = _requisition.GetAllRequisition();
            response.Add("RequisitionList", RequisitionList);
            GetMasterData(ref response);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Requisition(int id)
        {
            var result = _requisition.GetRequisitionByCriteria(id);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult Requisition([FromBody]TblRequisition requisition)
        {

             var result = _requisition.CreateRequisition(requisition);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult UpdateRequisition([FromBody] TblRequisition requisition)
        {
            var result = _requisition.UpdateRequisition(requisition);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut]
        public IActionResult CloseRequisition([FromBody] TblRequisition requisition)
        {
            var result = _requisition.CloseRequisition(requisition);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpDelete("{id}")]
        public IActionResult DeleteRequisition(int id)
        {
            var result = _requisition.DeleteRequisition(id);
            return Ok(result);
        }


        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpGet("{id}")]
        public IActionResult GetApplicantsRequisition(int id)
        {
            var result = _requisition.GetAllApplicantsForRequisition(id);
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
            var WorkFlows = _imasters.GetAllWorkFlowInternal();
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
