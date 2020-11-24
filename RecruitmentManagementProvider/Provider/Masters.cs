using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using RecruitmentManagementDataAccess.Masters;
using RecruitmentManagementDataAccess.ModelExtensions;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Xml;

namespace RecruitmentManagementProvider.Provider
{
    public class Masters : IMasters
    {
        MastersDAL MastersDAL = new MastersDAL();
        #region Clients

        public List<TblClients> GetAllClients()
        {
            return MastersDAL.GetAllClients();
        }

        #endregion

        #region Department

        public int CreateDepartment(TblDepartmentMaster tblDepartment)
        {
            return MastersDAL.CreateDepartment(tblDepartment);
        }

        public int DeleteDepartment(int key)
        {
            return MastersDAL.DeleteDepartment(key);
        }

        public List<TblDepartmentMaster> GetAllDepartment()
        {
            return MastersDAL.GetAllDepartment();
        }

        public int UpdateDepartment(TblDepartmentMaster tblDepartment)
        {
            return MastersDAL.UpdateDepartment(tblDepartment);
        }

        public TblDepartmentMaster GetDepartmentByCriteria(int key)
        {
            return MastersDAL.GetDepartmentByCriteria(key);
        }

        #endregion

        #region Designation

        public int CreateDesignation(TblDesignationMaster tblDesignation)
        {
            return MastersDAL.CreateDesignation(tblDesignation);
        }

        public int UpdateDesignation(TblDesignationMaster tblDesignation)
        {
            return MastersDAL.UpdateDesignation(tblDesignation);
        }

        public int DeleteDesignation(int key)
        {
            return MastersDAL.DeleteDesignation(key);
        }

        public List<TblDesignationMaster> GetAllDesignation()
        {
            return MastersDAL.GetAllDesignation();
        }

        public TblDesignationMaster GetDesignationByCriteria(int key)
        {
            return MastersDAL.GetDesignationByCriteria(key);
        }

        #endregion

        #region EmployeePanelMapping

        public int CreateEmployeePanel(TblEmployeePanelMapping panelMapping)
        {
            return MastersDAL.CreateEmployeePanel(panelMapping);
        }

        public int UpdateEmployeePanel(TblEmployeePanelMapping panelMapping)
        {
            return MastersDAL.UpdateEmployeePanel(panelMapping);
        }

        public int DeleteEmployeePanel(int key)
        {
            return MastersDAL.DeleteEmployeePanel(key);
        }

        public List<TblEmployeePanelMapping> GetAllEmployeePanel()
        {
            return MastersDAL.GetAllEmployeePanel();
        }

        public TblEmployeePanelMapping GetEmployeePanelByCriteria(int key)
        {
            return MastersDAL.GetEmployeePanelByCriteria(key);
        }

        #endregion

        #region Employees

        public int CreateEmployees(TblEmployees employees)
        {
            return MastersDAL.CreateEmployees(employees);
        }

        public int UpdateEmployees(TblEmployees employees)
        {
            return MastersDAL.UpdateEmployees(employees);
        }

        public int DeleteEmployees(int key)
        {
            return MastersDAL.DeleteEmployees(key);
        }

        public List<TblEmployees> GetAllEmployees()
        {
            return MastersDAL.GetAllEmployees();
        }

        public List<TblEmployees> GetEmployeesByCriteria(long key)
        {
            return MastersDAL.GetEmployeesByCriteria(key);
        }

        #endregion

        #region PanelGroup

        public int CreatePanelGroup(TblPanelGroupMaster tblPanelGroup)
        {
            return MastersDAL.CreatePanelGroup(tblPanelGroup);
        }

        public int UpdatePanelGroup(TblPanelGroupMaster tblPanelGroup)
        {
            return MastersDAL.UpdatePanelGroup(tblPanelGroup);
        }

        public int DeletePanelGroup(int key)
        {
            return MastersDAL.DeletePanelGroup(key);
        }

        public List<TblPanelGroupMaster> GetAllPanelGroup()
        {
            return MastersDAL.GetAllPanelGroup();
        }

        public TblPanelGroupMaster GetPanelGroupByCriteria(int key)
        {
            return MastersDAL.GetPanelGroupByCriteria(key);
        }

        #endregion

        #region Role

        public int CreateRole(TblRoleMaster tblRole)
        {
            return MastersDAL.CreateRole(tblRole);
        }

        public int UpdateRole(TblRoleMaster tblRole)
        {
            return MastersDAL.UpdateRole(tblRole);
        }

        public int DeleteRole(int key)
        {
            return MastersDAL.DeleteRole(key);
        }

        public List<TblRoleMaster> GetAllRole()
        {
            return MastersDAL.GetAllRole();
        }

        public TblRoleMaster GetRoleByCriteria(int key)
        {
            return MastersDAL.GetRoleByCriteria(key);
        }

        #endregion

        #region Skill

        public int CreateSkill(TblSkillMaster tblSkill)
        {
            return MastersDAL.CreateSkill(tblSkill);
        }

        public int UpdateSkill(TblSkillMaster tblSkill)
        {
            return MastersDAL.UpdateSkill(tblSkill);
        }

        public int DeleteSkill(int key)
        {
            return MastersDAL.DeleteSkill(key);
        }

        public List<TblSkillMaster> GetAllSkill()
        {
            return MastersDAL.GetAllSkill();
        }

        public TblSkillMaster GetSkillByCriteria(int key)
        {
            return MastersDAL.GetSkillByCriteria(key);
        }

        #endregion

        #region CommonMasters

        public  List<TblQualificationMaster> GetAllQualifications()
        {
            return MastersDAL.GetAllQualifications();
        }

        public  List<TblNoticePeriodMaster> GetAllNoticePeriod()
        {
            return MastersDAL.GetAllNoticePeriod();
        }

        public  List<TblJobTypeMaster> GetAllJobTypes()
        {
            return MastersDAL.GetAllJobTypes();
        }

        public  List<TblApplicantActiveMaster> GetAllApplicantActiveFrom()
        {
            return MastersDAL.GetAllApplicantActiveFrom();
        }

        public  List<TblLocationMaster> GetAllLocations()
        {
            return MastersDAL.GetAllLocations();
        }

        public List<TblWorkFlowMaster> GetAllWorkFlowInternal()
        {
            return MastersDAL.GetAllWorkFlowInternal();
        }
        public List<TblWorkFlowMaster> GetAllWorkFlowClient()
        {
            return MastersDAL.GetAllWorkFlowClient();
        }

        public List<TblGmapMaster> GetMapAddress()
        {
            return MastersDAL.GetMapAddress();
        }

        #endregion

        #region Vendor

        public int CreateVendors(TblVendors tblVendors)
        {
            return MastersDAL.CreateVendors(tblVendors);
        }

        public int DeleteVendors(int key)
        {
            return MastersDAL.DeleteVendors(key);
        }

        public List<TblVendors> GetAllVendors()
        {
            return MastersDAL.GetAllVendors();
        }

        public int UpdateVendors(TblVendors tblVendors)
        {
            return MastersDAL.UpdateVendors(tblVendors);
        }

        public TblVendors GetVendorsByCriteria(int key)
        {
            return MastersDAL.GetVendorsByCriteria(key);
        }

        //public List<TblCountries> GetCountries()
        //{
        //    return MastersDAL.GetCountries();
        //}



        //public List<TblStates> GetStates()
        //{
        //    return MastersDAL.GetStates();
        //}
        //public List<TblCities> GetCities()
        //{
        //    return MastersDAL.GetCities();
        //}

        #endregion

        #region Clients
        public int CreateClient(TblClients client)
        {
            return MastersDAL.CreateClient(client);
        }

        public int DeleteClient(int key)
        {
            return MastersDAL.DeleteClient(key);
        }

        public List<TblClients> GetAllClient()
        {
            return MastersDAL.GetAllClient();
        }

        public int UpdateClient(TblClients client)
        {
            return MastersDAL.UpdateClient(client);
        }

        public TblClients GetClientByCriteria(int key)
        {
            return MastersDAL.GetClientByCriteria(key);
        }

        public List<TblCountries> GetAllCountries()
        {
            return MastersDAL.GetAllCountries();
        }

        public List<TblStates> GetAllStates(int countryid)
        {
            return MastersDAL.GetAllStates(countryid);
        }
        public List<TblCities> GetAllCities(int stateid)
        {
            return MastersDAL.GetAllCities(stateid);
        }

        #endregion

        #region ClientsContactDetails

        public int CreateClientsContact(TblClientsContactDetails clientscontact)
        {
            return MastersDAL.CreateClientsContact(clientscontact);
        }

        public int DeleteClientsContact(int key)
        {
            return MastersDAL.DeleteClientsContact(key);
        }

        public List<TblClientsContactDetails> GetAllClientsContact()
        {
            return MastersDAL.GetAllClientsContact();
        }

        public int UpdateClientsContact(TblClientsContactDetails clientscontact)
        {
            return MastersDAL.UpdateClientsContact(clientscontact);
        }

        public TblClientsContactDetails GetClientsContactByCriteria(int key)
        {
            return MastersDAL.GetClientsContactByCriteria(key);
        }

        #endregion

        #region VendorContacts

        public int CreateVendorContacts(TblVendorContacts tblVendorContacts)
        {
            return MastersDAL.CreateVendorContacts(tblVendorContacts);
        }

        public int DeleteVendorContacts(int key)
        {
            return MastersDAL.DeleteVendorContacts(key);
        }

        public List<TblVendorContacts> GetAllVendorContacts()
        {
            return MastersDAL.GetAllVendorContacts();
        }

        public int UpdateVendorContacts(TblVendorContacts tblVendorContacts)
        {
            return MastersDAL.UpdateVendorContacts(tblVendorContacts);
        }

        public List<TblVendorContacts> GetVendorContactsByCriteria(int key)
        {
            return MastersDAL.GetVendorContactsByCriteria(key);
        }





        #endregion

        #region JobPortal
        public int CreateJobPortal(TblJobPortal tblJobPortal)
        {
            return MastersDAL.CreateJobPortal(tblJobPortal);
        }


        public int DeleteJobPortal(int key)
        {
            return MastersDAL.DeleteJobPortal(key);
        }
        public List<TblJobPortal> GetAllJobPortal()
        {
            return  MastersDAL.GetAllJobPortal();
        }

        public TblJobPortal GetJobPortalByCriteria(int key)
        {
            return  MastersDAL.GetJobPortalByCriteria(key);

        }

        public int UpdateJobPortal(TblJobPortal tblJobPortal)
        {
            return  MastersDAL.UpdateJobPortal(tblJobPortal);
        }
        #endregion


        #region SalaryBreakUpTemplate

        public int CreateSalaryBreakUpTemplate(string TemplateName, IFormFile formFile)
        {

            string FullFilePath = ImportFiletoFolder(formFile);
            string[] ParsedResult = LoadSalarySheetFromPath(FullFilePath);
            TblSalaryBreakupTemplates tblSalaryBreakup = new TblSalaryBreakupTemplates()
            {
                FormalsJson = ParsedResult[1],
                IsAcitve = true,
                TemplateName = TemplateName,
                TemplateJson = ParsedResult[0]
            };
            return MastersDAL.CreateSalaryBreakUpTemplate(tblSalaryBreakup);
        }
        public int UpdateSalaryBreakUpTemplate(string TemplateName, IFormFile formFile)
        {

            string FullFilePath = ImportFiletoFolder(formFile);
            string[] ParsedResult = LoadSalarySheetFromPath(FullFilePath);
            TblSalaryBreakupTemplates tblSalaryBreakup = new TblSalaryBreakupTemplates()
            {
                FormalsJson = ParsedResult[1],
                IsAcitve = true,
                TemplateName = TemplateName,
                TemplateJson = ParsedResult[0]
            };
            return MastersDAL.UpdateSalaryBreakUpTemplate(tblSalaryBreakup);
        }

        public int DeleteSalaryBreakUpTemplate(int key)
        {

            return MastersDAL.DeleteSalaryBreakUpTemplate(key);
        }


        public List<TblSalaryBreakupTemplates> GetAllSalaryTemplates()
        {
            return MastersDAL.GetAllSalaryTemplates();
        }
        public string[] LoadSalarySheetFromPath(string FullPath)
        {
            string[] result = new string[2];
            try
            {
                byte[] bytearray = File.ReadAllBytes(FullPath);
                using (MemoryStream stream = new MemoryStream(bytearray))
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                    Dictionary<string, string> formulasList = new Dictionary<string, string>();
                    SalaryBreakModel salaryBreakModel = new SalaryBreakModel();
                    salaryBreakModel.Rows = new List<SalaryBreakUpRow>();

                    for (int rowindex = 1; 100 > rowindex; rowindex++)
                    {
                        if (!string.IsNullOrEmpty(worksheet.Cells[rowindex, 1].Text) || !string.IsNullOrEmpty(worksheet.Cells[rowindex, 3].Text) || !string.IsNullOrEmpty(worksheet.Cells[rowindex, 4].Text))
                        {
                            SalaryBreakUpRow salaryBreakUpRow = new SalaryBreakUpRow();
                            salaryBreakUpRow.RowID = rowindex;
                            salaryBreakUpRow.ColumnA = LoadProps(rowindex, 1, worksheet, ref formulasList, "A");
                            salaryBreakUpRow.ColumnB = LoadProps(rowindex, 2, worksheet, ref formulasList, "B");
                            salaryBreakUpRow.ColumnC = LoadProps(rowindex, 3, worksheet, ref formulasList, "C");
                            salaryBreakModel.Rows.Add(salaryBreakUpRow);
                        }

                    }

                    string TableJson = JsonConvert.SerializeObject(salaryBreakModel);
                    string FormulasJson = JsonConvert.SerializeObject(formulasList);
                    result[0] = TableJson;
                    result[1] = TableJson;
                }
            }
            catch (Exception ex)
            {
                result[0] = string.Empty;
                result[1] = string.Empty;
            }

            return result;

        }

        static SalaryBreakUpColumnProperties LoadProps(int rowindex, int colindex, ExcelWorksheet worksheet, ref Dictionary<string, string> formulasList, string Alphabet)
        {

            decimal i = 0;
            SalaryBreakUpColumnProperties props = new SalaryBreakUpColumnProperties();
            if (worksheet.Cells[rowindex, colindex].Style.Font.Bold)
            {
                props.IsBold = true;
            }
            if (!string.IsNullOrEmpty(worksheet.Cells[rowindex, colindex].Formula))
            {
                props.Formula = worksheet.Cells[rowindex, colindex].Formula;
                props.HasFormula = true;
                formulasList.Add(Alphabet + rowindex.ToString(), worksheet.Cells[rowindex, colindex].Formula);

            }
            props.Value = worksheet.Cells[rowindex, colindex].Text;
            props.IsNumeric = false;
            if (decimal.TryParse(props.Value, out i))
            {
                props.IsNumeric = true;
            }
            return props;
        }

        public static string ImportFiletoFolder(IFormFile file)
        {
            if (file.Length > 0)
            {
                var folderName = Path.Combine("Resources", "Salary");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return fullPath;
            }
            else
            {
                return "0";
            }
        }

        #endregion
    }
}
