using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblRequisition
    {
        public TblRequisition()
        {
            TblApplicantRequisition = new HashSet<TblApplicantRequisition>();
            TblApplicantRequisitionClient = new HashSet<TblApplicantRequisitionClient>();
            TblRequisitionRecruiterMapping = new HashSet<TblRequisitionRecruiterMapping>();
            TblRequisitionSkillMapping = new HashSet<TblRequisitionSkillMapping>();
        }

        public long RequistionId { get; set; }
        public int DepartmentId { get; set; }
        public decimal? YearsofExperience { get; set; }
        public decimal? JoiningTenure { get; set; }
        public int? NoofPositions { get; set; }
        public string Location { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public long? ManagerEmployeeId { get; set; }
        public string ManagerComments { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DesignationId { get; set; }
        public string Title { get; set; }
        public string RecruiterComment { get; set; }
        public long? RecruiterLeadId { get; set; }
        public string CurrentOwner { get; set; }
        public string ApplicantWorkFlow { get; set; }
        public string CancelComments { get; set; }
        public int? ClientId { get; set; }
        public string PrimarySkills { get; set; }
        public string SecondarySkills { get; set; }

        public virtual TblClients Client { get; set; }
        public virtual TblEmployees CreatedByNavigation { get; set; }
        public virtual TblDepartmentMaster Department { get; set; }
        public virtual TblDesignationMaster Designation { get; set; }
        public virtual TblEmployees ManagerEmployee { get; set; }
        public virtual TblEmployees ModifiedByNavigation { get; set; }
        public virtual TblEmployees RecruiterLead { get; set; }
        public virtual ICollection<TblApplicantRequisition> TblApplicantRequisition { get; set; }
        public virtual ICollection<TblApplicantRequisitionClient> TblApplicantRequisitionClient { get; set; }
        public virtual ICollection<TblRequisitionRecruiterMapping> TblRequisitionRecruiterMapping { get; set; }
        public virtual ICollection<TblRequisitionSkillMapping> TblRequisitionSkillMapping { get; set; }
    }
}
