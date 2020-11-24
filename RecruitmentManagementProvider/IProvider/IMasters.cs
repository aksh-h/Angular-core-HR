using Microsoft.AspNetCore.Http;
using RecruitmentManagementDataAccess.Models;
using System.Collections.Generic;

namespace RecruitmentManagementProvider.IProvider
{
    public interface IMasters
    {
        #region Clients

        List<TblClients> GetAllClients();

        #endregion

        #region Department

        int CreateDepartment(TblDepartmentMaster tblDepartment);

        int DeleteDepartment(int key);

        List<TblDepartmentMaster> GetAllDepartment();

        TblDepartmentMaster GetDepartmentByCriteria(int key);

        int UpdateDepartment(TblDepartmentMaster tblDepartment);

        #endregion

        #region Designation

        int CreateDesignation(TblDesignationMaster tblDesignation);

        int DeleteDesignation(int key);

        List<TblDesignationMaster> GetAllDesignation();

        TblDesignationMaster GetDesignationByCriteria(int key);

        int UpdateDesignation(TblDesignationMaster tblDesignation);

        #endregion

        #region EmployeePanelMapping

        int CreateEmployeePanel(TblEmployeePanelMapping panelMapping);

        int DeleteEmployeePanel(int key);

        List<TblEmployeePanelMapping> GetAllEmployeePanel();

        TblEmployeePanelMapping GetEmployeePanelByCriteria(int key);

        int UpdateEmployeePanel(TblEmployeePanelMapping panelMapping);

        #endregion

        #region Employees

        int CreateEmployees(TblEmployees employees);

        int DeleteEmployees(int key);

        List<TblEmployees> GetAllEmployees();

        List<TblEmployees> GetEmployeesByCriteria(long key);

        int UpdateEmployees(TblEmployees employees);

        #endregion

        #region PanelGroup

        int CreatePanelGroup(TblPanelGroupMaster tblPanelGroup);

        int DeletePanelGroup(int key);

        List<TblPanelGroupMaster> GetAllPanelGroup();

        TblPanelGroupMaster GetPanelGroupByCriteria(int key);

        int UpdatePanelGroup(TblPanelGroupMaster tblPanelGroup);

        #endregion

        #region Role

        int CreateRole(TblRoleMaster tblRole);

        int DeleteRole(int key);

        List<TblRoleMaster> GetAllRole();

        TblRoleMaster GetRoleByCriteria(int key);

        int UpdateRole(TblRoleMaster tblRole);

        #endregion

        #region Skill

        int CreateSkill(TblSkillMaster tblSkill);

        int DeleteSkill(int key);

        List<TblSkillMaster> GetAllSkill();

        TblSkillMaster GetSkillByCriteria(int key);

        int UpdateSkill(TblSkillMaster tblSkill);

        #endregion


        #region CommonMasters

        List<TblQualificationMaster> GetAllQualifications();


        List<TblNoticePeriodMaster> GetAllNoticePeriod();


        List<TblJobTypeMaster> GetAllJobTypes();


        List<TblApplicantActiveMaster> GetAllApplicantActiveFrom();


        List<TblLocationMaster> GetAllLocations();

        List<TblWorkFlowMaster> GetAllWorkFlowInternal();
        List<TblWorkFlowMaster> GetAllWorkFlowClient();

        List<TblGmapMaster> GetMapAddress();


        #endregion

        #region Vendors
        int CreateVendors(TblVendors tblVendors);

        int DeleteVendors(int key);

        List<TblVendors> GetAllVendors();

        TblVendors GetVendorsByCriteria(int key);

        int UpdateVendors(TblVendors tblVendors);

        //List<TblCountries> GetCountries();
        //List<TblStates> GetStates();
        //List<TblCities> GetCities();
        #endregion

        #region VendorContacts
        int CreateVendorContacts(TblVendorContacts tblVendorContacts);

        int DeleteVendorContacts(int key);

        List<TblVendorContacts> GetAllVendorContacts();

        List<TblVendorContacts> GetVendorContactsByCriteria(int key);

        int UpdateVendorContacts(TblVendorContacts tblVendorContacts);
        #endregion

        #region Clients
        int CreateClient(TblClients client);

        int DeleteClient(int key);

        List<TblClients> GetAllClient();

        TblClients GetClientByCriteria(int key);

        int UpdateClient(TblClients client);
        
        List<TblCountries> GetAllCountries();
        List<TblStates> GetAllStates(int countryid);
        List<TblCities> GetAllCities(int stateid);
        #endregion

        #region ClientsContactDetails

        int CreateClientsContact(TblClientsContactDetails clientscontact);

        int DeleteClientsContact(int key);

        List<TblClientsContactDetails> GetAllClientsContact();

        TblClientsContactDetails GetClientsContactByCriteria(int key);

        int UpdateClientsContact(TblClientsContactDetails client);


        #endregion

        #region JobPortal
        int CreateJobPortal(TblJobPortal tblJobPortal);

        int DeleteJobPortal(int key);
        public List<TblJobPortal> GetAllJobPortal();

        TblJobPortal GetJobPortalByCriteria(int key);

        int UpdateJobPortal(TblJobPortal tblJobPortal);
        #endregion

        #region SalaryBreakUpTemplate
        List<TblSalaryBreakupTemplates> GetAllSalaryTemplates();

        int CreateSalaryBreakUpTemplate(string TemplateName, IFormFile formFile);

        int UpdateSalaryBreakUpTemplate(string TemplateName, IFormFile formFile);

        int DeleteSalaryBreakUpTemplate( int key);
        #endregion


    }
}

