using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using VaultSharp.V1.SystemBackend;

namespace RecruitmentManagementAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public class MastersController : BaseController
    {
        private IMasters _imasters;
        public MastersController(IMasters masters)
        {
            _imasters = masters;
        }

        #region Department

        
        [HttpGet]
        public IActionResult Department()
        {

            List<TblDepartmentMaster> result = _imasters.GetAllDepartment();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult Department(int id)
        {

            TblDepartmentMaster result = _imasters.GetDepartmentByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult Department([FromBody]TblDepartmentMaster tblDepartment)
        {
            int result = _imasters.CreateDepartment(tblDepartment);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Department(int id, [FromBody] TblDepartmentMaster tblDepartment)
        {

            int result = _imasters.UpdateDepartment(tblDepartment);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {

            int result = _imasters.DeleteDepartment(id);
            return Ok(result);

        }

        #endregion

        #region Designation

        [HttpGet]
        public IActionResult Designation()
        {

            List<TblDesignationMaster> result = _imasters.GetAllDesignation();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult Designation(int id)
        {

            TblDesignationMaster result = _imasters.GetDesignationByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult Designation([FromBody] TblDesignationMaster tblDesignation)
        {

            int result = _imasters.CreateDesignation(tblDesignation);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public IActionResult Designation(int id, [FromBody] TblDesignationMaster tblDesignation)
        {

            int result = _imasters.UpdateDesignation(tblDesignation);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDesignation(int id)
        {

            int result = _imasters.DeleteDesignation(id);
            return Ok(result);

        }

        #endregion

        #region EmployeePanelMapping

        [HttpGet]
        public IActionResult EmployeePanel()
        {
            //List<TblEmployeePanelMapping> result = _imasters.GetAllEmployeePanel();
            var EmployeePanelMappingList = _imasters.GetAllEmployeePanel();
            var EmpList = _imasters.GetAllEmployees();
            var PanGList = _imasters.GetAllPanelGroup();
            JsonObject result = new JsonObject();
            result.Add("EmployeePanelMappingList", EmployeePanelMappingList);
            result.Add("EmployeeList", EmpList);
            result.Add("PanelGroupList", PanGList);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult EmployeePanel(int id)
        {

            TblEmployeePanelMapping result = _imasters.GetEmployeePanelByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult EmployeePanel([FromBody] TblEmployeePanelMapping tblEmployee)
        {

            int result = _imasters.CreateEmployeePanel(tblEmployee);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public IActionResult EmployeePanel(int id, [FromBody] TblEmployeePanelMapping tblEmployee)
        {

            int result = _imasters.UpdateEmployeePanel(tblEmployee);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeePanel(int id)
        {

            int result = _imasters.DeleteEmployeePanel(id);
            return Ok(result);

        }

        #endregion

        #region Employees

        [HttpGet]
        public IActionResult Employees()
        {

            List<TblEmployees> result = _imasters.GetAllEmployees();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult Employees(int id)
        {            
            return Ok(_imasters.GetEmployeesByCriteria(id));
        }

        [HttpPost]
        public IActionResult Employees([FromBody] TblEmployees employees)
        {

            int result = _imasters.CreateEmployees(employees);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public IActionResult Employees(int id, [FromBody] TblEmployees employees)
        {

            int result = _imasters.UpdateEmployees(employees);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(int id)
        {

            int result = _imasters.DeleteEmployees(id);
            return Ok(result);

        }

        #endregion

        #region PanelGroup

        [HttpGet]
        public IActionResult PanelGroup()
        {

            List<TblPanelGroupMaster> result = _imasters.GetAllPanelGroup();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult PanelGroup(int id)
        {

            TblPanelGroupMaster result = _imasters.GetPanelGroupByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult PanelGroup([FromBody] TblPanelGroupMaster tblPanelGroup)
        {

            int result = _imasters.CreatePanelGroup(tblPanelGroup);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public IActionResult PanelGroup(int id, [FromBody] TblPanelGroupMaster tblPanelGroup)
        {

            int result = _imasters.UpdatePanelGroup(tblPanelGroup);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeletePanelGroup(int id)
        {

            int result = _imasters.DeletePanelGroup(id);
            return Ok(result);

        }

        #endregion

        #region Role

        [HttpGet]
        public IActionResult Role()
        {

            List<TblRoleMaster> result = _imasters.GetAllRole();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult Role(int id)
        {

            TblRoleMaster result = _imasters.GetRoleByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult Role([FromBody] TblRoleMaster tblRole)
        {

            int result = _imasters.CreateRole(tblRole);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public IActionResult Role(int id, [FromBody] TblRoleMaster tblRole)
        {

            int result = _imasters.UpdateRole(tblRole);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {

            int result = _imasters.DeleteRole(id);
            return Ok(result);

        }

        #endregion

        #region Skill

        [HttpGet]
        public IActionResult Skill()
        {

            List<TblSkillMaster> result = _imasters.GetAllSkill();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult Skill(int id)
        {

            TblSkillMaster result = _imasters.GetSkillByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult Skill([FromBody] TblSkillMaster tblSkill)
        {

            int result = _imasters.CreateSkill(tblSkill);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public IActionResult Skill(int id, [FromBody] TblSkillMaster tblSkill)
        {

            int result = _imasters.UpdateSkill(tblSkill);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSkill(int id)
        {

            int result = _imasters.DeleteSkill(id);
            return Ok(result);

        }

        #endregion

        #region GetMapAddress

        [HttpGet]
        public IActionResult GetMapAddress() 
        {
            List<TblGmapMaster> result = _imasters.GetMapAddress();
            return Ok(result);
        }

        #endregion

        #region Clients


        [HttpGet]
        public IActionResult Clients()
        {
            JsonObject response = new JsonObject();
            List<TblClients> result = _imasters.GetAllClients();
            response.Add("Clients", result);
            var countries = _imasters.GetAllCountries();
            response.Add("Countries", countries);
            return Ok(response);

        }


        [HttpGet("{id}")]
        public IActionResult City(int id)
        {
            List<TblCities> result = _imasters.GetAllCities(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult State(int id)
        {
            List<TblStates> result = _imasters.GetAllStates(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Clients(int id)
        {

            TblClients result = _imasters.GetClientByCriteria(id);
            return Ok(result);

        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult Clients([FromBody]TblClients client)
        {
            int result = _imasters.CreateClient(client);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut("{id}")]
        public IActionResult UpdateClients(int id, [FromBody] TblClients client)
        {

            int result = _imasters.UpdateClient(client);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClients(int id)
        {

            int result = _imasters.DeleteClient(id);
            return Ok(result);

        }

        #endregion

        #region ClientContactDetails

        [HttpGet]
        public IActionResult ClientsContact()
        {

            List<TblClientsContactDetails> result = _imasters.GetAllClientsContact();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult ClientsContact(int id)
        {

            TblClientsContactDetails result = _imasters.GetClientsContactByCriteria(id);
            return Ok(result);

        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPost]
        public IActionResult ClientsContact([FromBody]TblClientsContactDetails clientscontact)
        {
            int result = _imasters.CreateClientsContact(clientscontact);
            return Ok(result);
        }

        [ServiceFilter(typeof(ActionFilter), Order = 1)]
        [HttpPut("{id}")]
        public IActionResult ClientsContact(int id, [FromBody] TblClientsContactDetails clientscontact)
        {

            int result = _imasters.UpdateClientsContact(clientscontact);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClientsContact(int id)
        {

            int result = _imasters.DeleteClientsContact(id);
            return Ok(result);

        }

        #endregion
         
        #region Vendor
        [HttpGet]
        public IActionResult Vendors()
        {
            JsonObject response = new JsonObject();
            List<TblVendors> result = _imasters.GetAllVendors();
            response.Add("Vendors", result);
            var countries = _imasters.GetAllCountries();
            response.Add("Countries", countries);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public IActionResult Vendors(int id)
        {

            TblVendors result = _imasters.GetVendorsByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult Vendors([FromBody] TblVendors tblVendors)
        {
            int result = _imasters.CreateVendors(tblVendors);
            return Ok(result);
        }
       
        [HttpPut("{id}")]
        public IActionResult Vendors(int id, [FromBody] TblVendors tblVendors)
        {

            int result = _imasters.UpdateVendors(tblVendors);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVendors(int id)
        {

            int result = _imasters.DeleteVendors(id);
            return Ok(result);

        }
        //[HttpGet("{id}")]
        //public IActionResult City(int id)
        //{
        //    List<TblCities> result = _imasters.GetAllCities(id);
        //    return Ok(result);
        //}

        //[HttpGet("{id}")]
        //public IActionResult State(int id)
        //{
        //    List<TblStates> result = _imasters.GetAllStates(id);
        //    return Ok(result);
        //}

        #endregion

        #region VendorContacts
        [HttpGet]
        public IActionResult VendorContacts()
        {
            
            List<TblVendorContacts> result = _imasters.GetAllVendorContacts();
            
           
            return Ok(result);



        }

        [HttpGet("{id}")]
        public IActionResult VendorContacts(int id)
        {

            List<TblVendorContacts> result = _imasters.GetVendorContactsByCriteria(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult VendorContacts([FromBody] TblVendorContacts tblVendorContacts)
        {
            int result = _imasters.CreateVendorContacts(tblVendorContacts);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult VendorContacts(int id, [FromBody] TblVendorContacts tblVendorContacts)
        {

            int result = _imasters.UpdateVendorContacts(tblVendorContacts);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVendorContacts(int id)
        {

            int result = _imasters.DeleteVendorContacts(id);
            return Ok(result);

        }

        #endregion

        #region JobPortal
        [HttpGet]
        public IActionResult JobPortal()
        {
            List<TblJobPortal> result = _imasters.GetAllJobPortal();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult JobPortal(int id)
        {
            TblJobPortal result = _imasters.GetJobPortalByCriteria(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult JobPortal([FromBody] TblJobPortal tblJobPortal)
        {
            int result = _imasters.CreateJobPortal(tblJobPortal);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult JobPortal(int id, [FromBody] TblJobPortal tblJobPortal)
        {

            int result = _imasters.UpdateJobPortal(tblJobPortal);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJobPortal(int id)
        {

            int result = _imasters.DeleteJobPortal(id);
            return Ok(result);

        }
        #endregion

        #region SalaryBreakup
        [HttpGet]
        public IActionResult SalaryBreakUp()
        {
            List<TblSalaryBreakupTemplates> result = _imasters.GetAllSalaryTemplates();
            return Ok(result);
        }

        [HttpPost("{TemplateName}")]
        public IActionResult SalaryBreakUp(string TemplateName, IFormFile formFile)
        {
            int result = _imasters.CreateSalaryBreakUpTemplate(TemplateName,Request.Form.Files[0]);
            return Ok(result);
        }

        [HttpPut("{TemplateName}")]
        public IActionResult SalaryBreakUp(int id,string TemplateName, IFormFile formFile)
        {
            int result = _imasters.UpdateSalaryBreakUpTemplate(TemplateName, Request.Form.Files[0]);
            return Ok(result);
        }

        [HttpDelete("{key}")]
        public IActionResult DeleteSalaryBreakUp( int key)
        {
            int result = _imasters.DeleteSalaryBreakUpTemplate(key);
            return Ok(result);
        }

        #endregion
    }
}
