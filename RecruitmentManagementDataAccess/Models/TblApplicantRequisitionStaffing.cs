using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblApplicantRequisitionStaffing
    {
        public TblApplicantRequisitionStaffing()
        {
            TblInterviewManagementStaffing = new HashSet<TblInterviewManagementStaffing>();
            TblSelectedApplicantsStaffing = new HashSet<TblSelectedApplicantsStaffing>();
        }

        public long ApplicantRequisitionId { get; set; }
        public long ApplicantId { get; set; }
        public long RequisitionId { get; set; }
        public string Status { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string RecruiterComment { get; set; }
        public decimal? CurrentCtc { get; set; }
        public decimal? ExpectedCtc { get; set; }
        public decimal? NegotiatedCtc { get; set; }
        public DateTime? TentativeJoiningDate { get; set; }
        public DateTime? TentativeOnboardingDate { get; set; }

        public virtual TblApplicants Applicant { get; set; }
        public virtual TblEmployees CreatedByNavigation { get; set; }
        public virtual TblEmployees ModifiedByNavigation { get; set; }
        public virtual TblRequisitionStaffing Requisition { get; set; }
        public virtual ICollection<TblInterviewManagementStaffing> TblInterviewManagementStaffing { get; set; }
        public virtual ICollection<TblSelectedApplicantsStaffing> TblSelectedApplicantsStaffing { get; set; }
    }
}
