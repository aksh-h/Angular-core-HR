using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblApplicants
    {
        public TblApplicants()
        {
            TblApplicantRequisition = new HashSet<TblApplicantRequisition>();
            TblApplicantRequisitionClientApplicant = new HashSet<TblApplicantRequisitionClient>();
            TblApplicantRequisitionStaffing = new HashSet<TblApplicantRequisitionStaffing>();
            TblInterviewManagement = new HashSet<TblInterviewManagement>();
            TblInterviewManagementStaffing = new HashSet<TblInterviewManagementStaffing>();
        }

        public long ApplicantId { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailAddress { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string RelevantExperience { get; set; }
        public decimal? CurrentCtc { get; set; }
        public decimal? ExpectedCtc { get; set; }
        public string JoiningTime { get; set; }
        public string SkillsandProficiency { get; set; }
        public string LocationPreference { get; set; }
        public int? ReferedBy { get; set; }
        public string JobType { get; set; }
        public string Status { get; set; }
        public string NoticePeriod { get; set; }
        public string ApplicantActive { get; set; }
        public string ShortlistedBy { get; set; }
        public string Source { get; set; }
        public string PassportNo { get; set; }
        public string PanNo { get; set; }
        public string EmployeeEmailId { get; set; }
        public int? PortalId { get; set; }
        public int? VendorId { get; set; }
        public string ApplicantResume { get; set; }
        public string PhoneNumber { get; set; }

        public virtual TblApplicantRequisitionClient TblApplicantRequisitionClientApplicantRequisition { get; set; }
        public virtual ICollection<TblApplicantRequisition> TblApplicantRequisition { get; set; }
        public virtual ICollection<TblApplicantRequisitionClient> TblApplicantRequisitionClientApplicant { get; set; }
        public virtual ICollection<TblApplicantRequisitionStaffing> TblApplicantRequisitionStaffing { get; set; }
        public virtual ICollection<TblInterviewManagement> TblInterviewManagement { get; set; }
        public virtual ICollection<TblInterviewManagementStaffing> TblInterviewManagementStaffing { get; set; }
    }
}
