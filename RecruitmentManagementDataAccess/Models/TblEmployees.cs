using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblEmployees
    {
        public TblEmployees()
        {
            TblApplicantRequisitionClientCreatedByNavigation = new HashSet<TblApplicantRequisitionClient>();
            TblApplicantRequisitionClientModifiedByNavigation = new HashSet<TblApplicantRequisitionClient>();
            TblApplicantRequisitionCreatedByNavigation = new HashSet<TblApplicantRequisition>();
            TblApplicantRequisitionModifiedByNavigation = new HashSet<TblApplicantRequisition>();
            TblApplicantRequisitionStaffingCreatedByNavigation = new HashSet<TblApplicantRequisitionStaffing>();
            TblApplicantRequisitionStaffingModifiedByNavigation = new HashSet<TblApplicantRequisitionStaffing>();
            TblClientsCreatedByNavigation = new HashSet<TblClients>();
            TblClientsModifiedByNavigation = new HashSet<TblClients>();
            TblEmployeePanelMapping = new HashSet<TblEmployeePanelMapping>();
            TblInterviewEmployeeMapping = new HashSet<TblInterviewEmployeeMapping>();
            TblInterviewEmployeeMappingStaffing = new HashSet<TblInterviewEmployeeMappingStaffing>();
            TblInterviewManagementCreatedByNavigation = new HashSet<TblInterviewManagement>();
            TblInterviewManagementModifiedByNavigation = new HashSet<TblInterviewManagement>();
            TblInterviewManagementStaffingCreatedByNavigation = new HashSet<TblInterviewManagementStaffing>();
            TblInterviewManagementStaffingModifiedByNavigation = new HashSet<TblInterviewManagementStaffing>();
            TblLogin = new HashSet<TblLogin>();
            TblRequisitionCreatedByNavigation = new HashSet<TblRequisition>();
            TblRequisitionManagerEmployee = new HashSet<TblRequisition>();
            TblRequisitionModifiedByNavigation = new HashSet<TblRequisition>();
            TblRequisitionRecruiterLead = new HashSet<TblRequisition>();
            TblRequisitionRecruiterMapping = new HashSet<TblRequisitionRecruiterMapping>();
            TblRequisitionRecruiterMappingStaffing = new HashSet<TblRequisitionRecruiterMappingStaffing>();
            TblRequisitionStaffingCreatedByNavigation = new HashSet<TblRequisitionStaffing>();
            TblRequisitionStaffingManagerEmployee = new HashSet<TblRequisitionStaffing>();
            TblRequisitionStaffingModifiedByNavigation = new HashSet<TblRequisitionStaffing>();
            TblRequisitionStaffingRecruiterLead = new HashSet<TblRequisitionStaffing>();
            TblVendorsCreatedByNavigation = new HashSet<TblVendors>();
            TblVendorsModifiedByNavigation = new HashSet<TblVendors>();
        }

        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public int ReportingManagerId { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string EmailId { get; set; }

        public virtual TblDepartmentMaster Department { get; set; }
        public virtual TblDesignationMaster Designation { get; set; }
        public virtual TblRoleMaster Role { get; set; }
        public virtual ICollection<TblApplicantRequisitionClient> TblApplicantRequisitionClientCreatedByNavigation { get; set; }
        public virtual ICollection<TblApplicantRequisitionClient> TblApplicantRequisitionClientModifiedByNavigation { get; set; }
        public virtual ICollection<TblApplicantRequisition> TblApplicantRequisitionCreatedByNavigation { get; set; }
        public virtual ICollection<TblApplicantRequisition> TblApplicantRequisitionModifiedByNavigation { get; set; }
        public virtual ICollection<TblApplicantRequisitionStaffing> TblApplicantRequisitionStaffingCreatedByNavigation { get; set; }
        public virtual ICollection<TblApplicantRequisitionStaffing> TblApplicantRequisitionStaffingModifiedByNavigation { get; set; }
        public virtual ICollection<TblClients> TblClientsCreatedByNavigation { get; set; }
        public virtual ICollection<TblClients> TblClientsModifiedByNavigation { get; set; }
        public virtual ICollection<TblEmployeePanelMapping> TblEmployeePanelMapping { get; set; }
        public virtual ICollection<TblInterviewEmployeeMapping> TblInterviewEmployeeMapping { get; set; }
        public virtual ICollection<TblInterviewEmployeeMappingStaffing> TblInterviewEmployeeMappingStaffing { get; set; }
        public virtual ICollection<TblInterviewManagement> TblInterviewManagementCreatedByNavigation { get; set; }
        public virtual ICollection<TblInterviewManagement> TblInterviewManagementModifiedByNavigation { get; set; }
        public virtual ICollection<TblInterviewManagementStaffing> TblInterviewManagementStaffingCreatedByNavigation { get; set; }
        public virtual ICollection<TblInterviewManagementStaffing> TblInterviewManagementStaffingModifiedByNavigation { get; set; }
        public virtual ICollection<TblLogin> TblLogin { get; set; }
        public virtual ICollection<TblRequisition> TblRequisitionCreatedByNavigation { get; set; }
        public virtual ICollection<TblRequisition> TblRequisitionManagerEmployee { get; set; }
        public virtual ICollection<TblRequisition> TblRequisitionModifiedByNavigation { get; set; }
        public virtual ICollection<TblRequisition> TblRequisitionRecruiterLead { get; set; }
        public virtual ICollection<TblRequisitionRecruiterMapping> TblRequisitionRecruiterMapping { get; set; }
        public virtual ICollection<TblRequisitionRecruiterMappingStaffing> TblRequisitionRecruiterMappingStaffing { get; set; }
        public virtual ICollection<TblRequisitionStaffing> TblRequisitionStaffingCreatedByNavigation { get; set; }
        public virtual ICollection<TblRequisitionStaffing> TblRequisitionStaffingManagerEmployee { get; set; }
        public virtual ICollection<TblRequisitionStaffing> TblRequisitionStaffingModifiedByNavigation { get; set; }
        public virtual ICollection<TblRequisitionStaffing> TblRequisitionStaffingRecruiterLead { get; set; }
        public virtual ICollection<TblVendors> TblVendorsCreatedByNavigation { get; set; }
        public virtual ICollection<TblVendors> TblVendorsModifiedByNavigation { get; set; }
    }
}
