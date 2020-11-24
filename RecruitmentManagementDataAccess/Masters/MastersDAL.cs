using RecruitmentManagementDataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace RecruitmentManagementDataAccess.Masters
{
    public  class MastersDAL
    {
        private readonly  RecruitmentManagementContext dbContext;
        public MastersDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }

        #region Clients

        public  List<TblClients> GetAllClients()
        {
            return dbContext.TblClients.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        #endregion

        #region Department

        public  int CreateDepartment(TblDepartmentMaster tblDepartment)
        {
            if (dbContext.TblDepartmentMaster.Where(r => r.Department.ToUpper() == tblDepartment.Department.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblDepartment.IsActive = true;
            dbContext.Entry(tblDepartment).State = EntityState.Added;
            dbContext.TblDepartmentMaster.Add(tblDepartment);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblDepartment).State = EntityState.Detached;
            return result;
        }

        public  int DeleteDepartment(int key)
        {
            TblDepartmentMaster department = dbContext.TblDepartmentMaster.Where(x => x.DepartmentId == key).FirstOrDefault();
            department.IsActive = false;
            dbContext.TblDepartmentMaster.Update(department);
            return dbContext.SaveChanges();
        }

        public  List<TblDepartmentMaster> GetAllDepartment()
        {
            return dbContext.TblDepartmentMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateDepartment(TblDepartmentMaster tblDepartment)
        {
            if (dbContext.TblDepartmentMaster.Where(r => r.Department.ToUpper() == tblDepartment.Department.ToUpper() && r.DepartmentId != tblDepartment.DepartmentId && r.IsActive == true).Count() > 0)
                return -1;
            tblDepartment.IsActive = true;
            dbContext.Entry(tblDepartment).State = EntityState.Modified;
            dbContext.TblDepartmentMaster.Update(tblDepartment);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblDepartment).State = EntityState.Detached;
            return result;
        }

        public  TblDepartmentMaster GetDepartmentByCriteria(int key)
        {
            return dbContext.TblDepartmentMaster.Where(x => x.DepartmentId == key).FirstOrDefault();
        }

        #endregion

        #region Designation

        public  int CreateDesignation(TblDesignationMaster tblDesignation)
        {
            if (dbContext.TblDesignationMaster.Where(r => r.Designation.ToUpper() == tblDesignation.Designation.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblDesignation.IsActive = true;
            dbContext.Entry(tblDesignation).State = EntityState.Added;
            dbContext.TblDesignationMaster.Add(tblDesignation);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblDesignation).State = EntityState.Detached;
            return result;
        }

        public  int DeleteDesignation(int key)
        {
            TblDesignationMaster designation = dbContext.TblDesignationMaster.Where(x => x.DesignationId == key).FirstOrDefault();
            designation.IsActive = false;
            dbContext.TblDesignationMaster.Update(designation);
            return dbContext.SaveChanges();
        }

        public  List<TblDesignationMaster> GetAllDesignation()
        {
            return dbContext.TblDesignationMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateDesignation(TblDesignationMaster tblDesignation)
        {
            if (dbContext.TblDesignationMaster.Where(r => r.Designation.ToUpper() == tblDesignation.Designation.ToUpper() && r.DesignationId != tblDesignation.DesignationId && r.IsActive == true).Count() > 0)
                return -1;
            tblDesignation.IsActive = true;
            dbContext.Entry(tblDesignation).State = EntityState.Modified;
            dbContext.TblDesignationMaster.Update(tblDesignation);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblDesignation).State = EntityState.Detached;
            return result;
        }

        public  TblDesignationMaster GetDesignationByCriteria(int key)
        {
            return dbContext.TblDesignationMaster.Where(x => x.DesignationId == key).FirstOrDefault();
        }

        #endregion

        #region EmployeePanelMapping

        public  int CreateEmployeePanel(TblEmployeePanelMapping panelMapping)
        {
            if (dbContext.TblEmployeePanelMapping.Where(r => r.PanelGroupId == panelMapping.PanelGroupId && r.EmployeeId == panelMapping.EmployeeId && r.IsActive == panelMapping.IsActive).Count() > 0)
                return -1;
            panelMapping.IsActive = true;
            dbContext.Entry(panelMapping).State = EntityState.Added;
            dbContext.TblEmployeePanelMapping.Add(panelMapping);
            int result = dbContext.SaveChanges();
            dbContext.Entry(panelMapping).State = EntityState.Detached;
            return result;
        }

        public  int DeleteEmployeePanel(int key)
        {
            TblEmployeePanelMapping panelMapping = dbContext.TblEmployeePanelMapping.Where(x => x.EmployeePanelMappingId == key).FirstOrDefault();
            panelMapping.IsActive = false;
            dbContext.TblEmployeePanelMapping.Update(panelMapping);
            return dbContext.SaveChanges();
        }

        public  List<TblEmployeePanelMapping> GetAllEmployeePanel()
        {
            return dbContext.TblEmployeePanelMapping.AsNoTracking().Include(s => s.PanelGroup).Include(q => q.Employee).Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateEmployeePanel(TblEmployeePanelMapping panelMapping)
        {
            if (dbContext.TblEmployeePanelMapping.Where(r => r.PanelGroupId == panelMapping.PanelGroupId && r.EmployeeId == panelMapping.EmployeeId && r.EmployeePanelMappingId != panelMapping.EmployeePanelMappingId && r.IsActive == panelMapping.IsActive).Count() > 0)
                return -1;
            panelMapping.IsActive = true;
            dbContext.Entry(panelMapping).State = EntityState.Modified;
            dbContext.TblEmployeePanelMapping.Update(panelMapping);
            int result = dbContext.SaveChanges();
            dbContext.Entry(panelMapping).State = EntityState.Detached;
            return result;
        }

        public  TblEmployeePanelMapping GetEmployeePanelByCriteria(int key)
        {
            return dbContext.TblEmployeePanelMapping.Where(x => x.EmployeePanelMappingId == key).FirstOrDefault();
        }

        #endregion

        #region Employees

        public  int CreateEmployees(TblEmployees employees)
        {
            var Cond = dbContext.TblEmployees.Where(e => e.IsActive == true && employees.IsActive == true).Count() > 0;
            if (Cond)
            {
                dbContext.TblEmployees.Add(employees);
                return dbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public  int DeleteEmployees(int key)
        {
            TblEmployees employees = dbContext.TblEmployees.Where(x => x.EmployeeId == key).FirstOrDefault();
            employees.IsActive = false;
            dbContext.TblEmployees.Update(employees);
            return dbContext.SaveChanges();
        }

        public  List<TblEmployees> GetAllEmployees()
        {
            return dbContext.TblEmployees.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateEmployees(TblEmployees employees)
        {
            var Cond = dbContext.TblEmployees.Where(e => e.IsActive == true && employees.IsActive == true).Count() > 0;
            if (Cond)
            {
                dbContext.TblEmployees.Update(employees);
                return dbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public  List<TblEmployees> GetEmployeesByCriteria(long key)
        {
            return dbContext.TblEmployees.AsNoTracking().Where(x => x.ReportingManagerId == key && x.IsActive == true).ToList();
        }

        #endregion

        #region PanelGroup

        public  int CreatePanelGroup(TblPanelGroupMaster tblPanelGroup)
        {
            if (dbContext.TblPanelGroupMaster.Where(r => r.PanelGroupName.ToUpper() == tblPanelGroup.PanelGroupName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblPanelGroup.IsActive = true;
            dbContext.Entry(tblPanelGroup).State = EntityState.Added;
            dbContext.TblPanelGroupMaster.Add(tblPanelGroup);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblPanelGroup).State = EntityState.Detached;
            return result;
        }

        public  int DeletePanelGroup(int key)
        {
            TblPanelGroupMaster panelgroup = dbContext.TblPanelGroupMaster.Where(x => x.PanelGroupId == key).FirstOrDefault();
            panelgroup.IsActive = false;
            dbContext.TblPanelGroupMaster.Update(panelgroup);
            return dbContext.SaveChanges();
        }

        public  List<TblPanelGroupMaster> GetAllPanelGroup()
        {
            return dbContext.TblPanelGroupMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdatePanelGroup(TblPanelGroupMaster tblPanelGroup)
        {
            if (dbContext.TblPanelGroupMaster.Where(r => r.PanelGroupName.ToUpper() == tblPanelGroup.PanelGroupName.ToUpper() && r.PanelGroupId != tblPanelGroup.PanelGroupId && r.IsActive == true).Count() > 0)
                return -1;
            tblPanelGroup.IsActive = true;
            dbContext.Entry(tblPanelGroup).State = EntityState.Modified;
            dbContext.TblPanelGroupMaster.Update(tblPanelGroup);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblPanelGroup).State = EntityState.Detached;
            return result;
        }

        public  TblPanelGroupMaster GetPanelGroupByCriteria(int key)
        {
            return dbContext.TblPanelGroupMaster.Where(x => x.PanelGroupId == key).FirstOrDefault();
        }

        #endregion

        #region Role

        public  int CreateRole(TblRoleMaster tblRole)
        {
            if (dbContext.TblRoleMaster.Where(r => r.RoleName.ToUpper() == tblRole.RoleName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblRole.IsActive = true;
            dbContext.Entry(tblRole).State = EntityState.Added;
            dbContext.TblRoleMaster.Add(tblRole);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblRole).State = EntityState.Detached;
            return result;
        }

        public  int DeleteRole(int key)
        {
            TblRoleMaster role = dbContext.TblRoleMaster.Where(x => x.RoleId == key).FirstOrDefault();
            role.IsActive = false;
            dbContext.TblRoleMaster.Update(role);
            return dbContext.SaveChanges();
        }

        public  List<TblRoleMaster> GetAllRole()
        {
            return dbContext.TblRoleMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateRole(TblRoleMaster tblRole)
        {
            if (dbContext.TblRoleMaster.Where(r => r.RoleName.ToUpper() == tblRole.RoleName.ToUpper() && r.RoleId != tblRole.RoleId && r.IsActive == true).Count() > 0)
                return -1;
            tblRole.IsActive = true;
            dbContext.Entry(tblRole).State = EntityState.Modified;
            dbContext.TblRoleMaster.Update(tblRole);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblRole).State = EntityState.Detached;
            return result;
        }

        public  TblRoleMaster GetRoleByCriteria(int key)
        {
            return dbContext.TblRoleMaster.Where(x => x.RoleId == key).FirstOrDefault();
        }

        #endregion

        #region Skill

        public  int CreateSkill(TblSkillMaster tblSkill)
        {
            if (dbContext.TblSkillMaster.Where(r => r.SkillName.ToUpper() == tblSkill.SkillName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblSkill.IsActive = true;
            dbContext.Entry(tblSkill).State = EntityState.Added;
            dbContext.TblSkillMaster.Add(tblSkill);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblSkill).State = EntityState.Detached;
            return result;
        }

        public  int DeleteSkill(int key)
        {
            TblSkillMaster skill = dbContext.TblSkillMaster.Where(x => x.SkillId == key).FirstOrDefault();
            skill.IsActive = false;
            dbContext.TblSkillMaster.Update(skill);
            return dbContext.SaveChanges();
        }

        public  List<TblSkillMaster> GetAllSkill()
        {
            return dbContext.TblSkillMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateSkill(TblSkillMaster tblSkill)
        {
            if (dbContext.TblSkillMaster.Where(r => r.SkillName.ToUpper() == tblSkill.SkillName.ToUpper() && r.SkillId != tblSkill.SkillId && r.IsActive == true).Count() > 0)
                return -1;
            tblSkill.IsActive = true;
            dbContext.Entry(tblSkill).State = EntityState.Modified;
            dbContext.TblSkillMaster.Update(tblSkill);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblSkill).State = EntityState.Detached;
            return result;
        }

        public  TblSkillMaster GetSkillByCriteria(int key)
        {
            return dbContext.TblSkillMaster.Where(x => x.SkillId == key).FirstOrDefault();
        }

        #endregion

        #region CommonMasters

        public  List<TblQualificationMaster> GetAllQualifications()
        {
            return dbContext.TblQualificationMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  List<TblNoticePeriodMaster> GetAllNoticePeriod()
        {
            return dbContext.TblNoticePeriodMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  List<TblJobTypeMaster> GetAllJobTypes()
        {
            return dbContext.TblJobTypeMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  List<TblApplicantActiveMaster> GetAllApplicantActiveFrom()
        {
            return dbContext.TblApplicantActiveMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  List<TblLocationMaster> GetAllLocations()
        {
            return dbContext.TblLocationMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  List<TblWorkFlowMaster> GetAllWorkFlowInternal()
        {
            return dbContext.TblWorkFlowMaster.AsNoTracking().Where(x => x.IsActive == true && x.WorkFlowName != "Client").ToList();
        }
        public  List<TblWorkFlowMaster> GetAllWorkFlowClient()
        {
            return dbContext.TblWorkFlowMaster.AsNoTracking().Where(x => x.IsActive == true && x.WorkFlowName == "Client").ToList();
        }
        public  List<TblGmapMaster> GetMapAddress()
        {
            return dbContext.TblGmapMaster.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        #endregion

        #region Vendors
        public  int CreateVendors(TblVendors tblVendors)
        {
            if (dbContext.TblVendors.Where(r => r.VendorName.ToUpper() == tblVendors.VendorName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblVendors.IsActive = true;
            tblVendors.CreatedBy = CurrentContext.EmployeeID;
            tblVendors.CreatedDate = DateTime.Now;
            dbContext.Entry(tblVendors).State = EntityState.Added;
            dbContext.TblVendors.Add(tblVendors);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblVendors).State = EntityState.Detached;
            return result;
        }

        public  int DeleteVendors(int key)
        {
            TblVendors vendors = dbContext.TblVendors.Where(x => x.VendorId == key).FirstOrDefault();
            vendors.IsActive = false;
            dbContext.TblVendors.Update(vendors);
            return dbContext.SaveChanges();
        }

        public  List<TblVendors> GetAllVendors()
        {

            return dbContext.TblVendors.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateVendors(TblVendors tblVendors)
        {
            if (dbContext.TblVendors.Where(r => r.VendorName.ToUpper() == tblVendors.VendorName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblVendors.IsActive = true;
            tblVendors.ModifiedBy = CurrentContext.EmployeeID;
            tblVendors.ModifiedDate = DateTime.Now;
            dbContext.Entry(tblVendors).State = EntityState.Modified;
            dbContext.TblVendors.Update(tblVendors);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblVendors).State = EntityState.Detached;
            return result;
        }

        public  TblVendors GetVendorsByCriteria(int key)
        {
            return dbContext.TblVendors.Where(x => x.VendorId == key).FirstOrDefault();
        }
        public  List<TblCountries> GetAllCountries()
        {
            return dbContext.TblCountries.AsNoTracking().Where(x => x.Flag == 1).ToList();
        }
        public  List<TblStates> GetAllStates(int countryid)
        {
            return dbContext.TblStates.AsNoTracking().Where(x => x.Flag == 1 && x.CountryId == countryid).ToList();
        }
        public  List<TblCities> GetAllCities(int stateid)
        {
            return dbContext.TblCities.AsNoTracking().Where(x => x.Flag == 1 && x.StateId == stateid).ToList();
        }
        #endregion

        #region VendorContacts
        public  int CreateVendorContacts(TblVendorContacts tblVendorContacts)
        {
            if (dbContext.TblVendorContacts.Where(r => r.ContactPerson.ToUpper() == tblVendorContacts.ContactPerson.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblVendorContacts.IsActive = true;
            tblVendorContacts.ContactId = 0;
            dbContext.Entry(tblVendorContacts).State = EntityState.Added;
            dbContext.TblVendorContacts.Add(tblVendorContacts);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblVendorContacts).State = EntityState.Detached;
            return result;

        }

        public  int DeleteVendorContacts(int key)
        {
            TblVendorContacts vendorContacts = dbContext.TblVendorContacts.Where(x => x.ContactId == key).FirstOrDefault();
            vendorContacts.IsActive = false;
            dbContext.TblVendorContacts.Update(vendorContacts);
            return dbContext.SaveChanges();

        }

        public  List<TblVendorContacts> GetAllVendorContacts()
        {
            return dbContext.TblVendorContacts.AsNoTracking().Where(x => x.IsActive == true).ToList();

        }

        public  int UpdateVendorContacts(TblVendorContacts tblVendorContacts)
        {
            if (dbContext.TblVendorContacts.AsNoTracking().Where(r => r.ContactPerson.ToUpper() == tblVendorContacts.ContactPerson.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblVendorContacts.IsActive = true;
            dbContext.Entry(tblVendorContacts).State = EntityState.Modified;
            dbContext.TblVendorContacts.Update(tblVendorContacts);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblVendorContacts).State = EntityState.Detached;
            return result;

        }

        public  List<TblVendorContacts> GetVendorContactsByCriteria(int key)
        {
            return dbContext.TblVendorContacts.AsNoTracking().Where(x => x.VendorId == key).ToList();

        }

        #endregion

        #region Clients
        public  int CreateClient(TblClients client)
        {
            if (dbContext.TblClients.Where(r => r.ClientName.ToUpper() == client.ClientName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            client.IsActive = true;
            client.CreatedBy = CurrentContext.EmployeeID;
            client.CreatedDate = System.DateTime.Now;

            dbContext.Entry(client).State = EntityState.Added;
            dbContext.TblClients.Add(client);
            int result = dbContext.SaveChanges();
            dbContext.Entry(client).State = EntityState.Detached;
            return result;
        }

        public  int DeleteClient(int key)
        {
            TblClients client = dbContext.TblClients.Where(x => x.ClientId == key).FirstOrDefault();
            client.IsActive = false;
            dbContext.TblClients.Update(client);
            return dbContext.SaveChanges();
        }

        public  List<TblClients> GetAllClient()
        {
            return dbContext.TblClients.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateClient(TblClients client)
        {
            if (dbContext.TblClients.Where(r => r.ClientName.ToUpper() == client.ClientName.ToUpper() && r.ClientId != client.ClientId && r.IsActive == true).Count() > 0)
                return -1;
            client.IsActive = true;
            client.ModifiedBy = CurrentContext.EmployeeID;
            client.ModifiedDate = System.DateTime.Now;
            dbContext.Entry(client).State = EntityState.Modified;
            dbContext.TblClients.Update(client);
            int result = dbContext.SaveChanges();
            dbContext.Entry(client).State = EntityState.Detached;
            return result;
        }

        public  TblClients GetClientByCriteria(int key)
        {
            return dbContext.TblClients.Where(x => x.ClientId == key).FirstOrDefault();
        }

        //public  List<TblCountries> GetAllCountries()
        //{
        //    return dbContext.TblCountries.AsNoTracking().Where(x => x.Flag == 1).ToList();
        //}
        //public  List<TblStates> GetAllStates(int countryid)
        //{
        //    return dbContext.TblStates.AsNoTracking().Where(x => x.Flag == 1 && x.CountryId == countryid).ToList();
        //}
        //public  List<TblCities> GetAllCities(int stateid)
        //{
        //    return dbContext.TblCities.AsNoTracking().Where(x => x.Flag == 1 && x.StateId == stateid).ToList();
        //}


        #endregion

        #region ClientsContactDetails

        public  int CreateClientsContact(TblClientsContactDetails clientscontact)
        {
            if (dbContext.TblClientsContactDetails.Where(r => r.ContactPerson.ToUpper() == clientscontact.ContactPerson.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            clientscontact.IsActive = true;
            dbContext.Entry(clientscontact).State = EntityState.Added;
            dbContext.TblClientsContactDetails.Add(clientscontact);
            int result = dbContext.SaveChanges();
            dbContext.Entry(clientscontact).State = EntityState.Detached;
            return result;
        }

        public  int DeleteClientsContact(int key)
        {
            TblClientsContactDetails clientscontact = dbContext.TblClientsContactDetails.Where(x => x.ContactId == key).FirstOrDefault();
            clientscontact.IsActive = false;
            dbContext.TblClientsContactDetails.Update(clientscontact);
            return dbContext.SaveChanges();
        }

        public  List<TblClientsContactDetails> GetAllClientsContact()
        {
            return dbContext.TblClientsContactDetails.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public  int UpdateClientsContact(TblClientsContactDetails clientscontact)
        {
            if (dbContext.TblClientsContactDetails.Where(r => r.ContactPerson.ToUpper() == clientscontact.ContactPerson.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            dbContext.Entry(clientscontact).State = EntityState.Modified;
            dbContext.TblClientsContactDetails.Update(clientscontact);
            int result = dbContext.SaveChanges();
            dbContext.Entry(clientscontact).State = EntityState.Detached;
            return result;
        }

        public  TblClientsContactDetails GetClientsContactByCriteria(int key)
        {
            return dbContext.TblClientsContactDetails.Where(x => x.ContactId == key).FirstOrDefault();
        }

        #endregion

        #region JobPortal
        public int CreateJobPortal(TblJobPortal tblJobPortal)
        {
            if (dbContext.TblJobPortal.Where(r => r.PortalName.ToUpper() == tblJobPortal.PortalName.ToUpper() && r.IsActive == true).Count() > 0)
                return -1;
            tblJobPortal.IsActive = true;
            dbContext.Entry(tblJobPortal).State = EntityState.Added;
            dbContext.TblJobPortal.Add(tblJobPortal);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblJobPortal).State = EntityState.Detached;
            return result;
        }

        public int DeleteJobPortal(int key)
        {
            TblJobPortal tblJobPortal = dbContext.TblJobPortal.Where(x => x.PortalId == key).FirstOrDefault();
            tblJobPortal.IsActive = false;
            dbContext.TblJobPortal.Update(tblJobPortal);
            return dbContext.SaveChanges();
        }

        public List<TblJobPortal> GetAllJobPortal()
        {
            return dbContext.TblJobPortal.AsNoTracking().Where(x => x.IsActive == true).ToList();
        }

        public int UpdateJobPortal(TblJobPortal tblJobPortal)
        {
            if (dbContext.TblJobPortal.Where(r => r.PortalName.ToUpper() == tblJobPortal.PortalName.ToUpper() && r.PortalId != tblJobPortal.PortalId && r.IsActive == true).Count() > 0)
                return -1;
            tblJobPortal.IsActive = true;
            dbContext.Entry(tblJobPortal).State = EntityState.Modified;
            dbContext.TblJobPortal.Update(tblJobPortal);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblJobPortal).State = EntityState.Detached;
            return result;
        }

        public TblJobPortal GetJobPortalByCriteria(int key)
        {
            return dbContext.TblJobPortal.Where(x => x.PortalId == key).FirstOrDefault();
        }

        #endregion

        #region SalaryBreakUpTemplate

        public List<TblSalaryBreakupTemplates> GetAllSalaryTemplates()
        {
            return dbContext.TblSalaryBreakupTemplates.Where(r => r.IsAcitve == true).ToList();
        }

        public int CreateSalaryBreakUpTemplate(TblSalaryBreakupTemplates tblSalaryBreakup)
        {
           // return dbContext.TblSalaryBreakupTemplates(tblSalaryBreakup);
            if (dbContext.TblSalaryBreakupTemplates.Where(r => r.TemplateName.ToUpper() == tblSalaryBreakup.TemplateName.ToUpper() && r.IsAcitve == true).Count() > 0)
                return -1;
            tblSalaryBreakup.IsAcitve = true;
            dbContext.Entry(tblSalaryBreakup).State = EntityState.Added;
            dbContext.TblSalaryBreakupTemplates.Add(tblSalaryBreakup);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblSalaryBreakup).State = EntityState.Detached;
            return result;
        }

        public int UpdateSalaryBreakUpTemplate(TblSalaryBreakupTemplates tblSalaryBreakup)
        {
            if (dbContext.TblSalaryBreakupTemplates.Where(r => r.TemplateName.ToUpper() == tblSalaryBreakup.TemplateName.ToUpper() && r.IsAcitve == true).Count() > 0)
                return -1;
            tblSalaryBreakup.IsAcitve = true;
            dbContext.Entry(tblSalaryBreakup).State = EntityState.Added;
            dbContext.TblSalaryBreakupTemplates.Update(tblSalaryBreakup);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblSalaryBreakup).State = EntityState.Detached;
            return result;
        }

        public int DeleteSalaryBreakUpTemplate(int key)
        {
            TblSalaryBreakupTemplates tblSalaryBreakup = dbContext.TblSalaryBreakupTemplates.Where(x => x.SalarayTemplateId == key).FirstOrDefault();
            tblSalaryBreakup.IsAcitve = false;
            dbContext.TblSalaryBreakupTemplates.Update(tblSalaryBreakup);
            return dbContext.SaveChanges();
        }
        #endregion

    }
}
