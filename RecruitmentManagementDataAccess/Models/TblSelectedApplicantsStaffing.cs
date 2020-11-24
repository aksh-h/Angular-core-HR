using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblSelectedApplicantsStaffing
    {
        public long SelectedApplicantsId { get; set; }
        public long ApplicantRequisitionId { get; set; }
        public string ApplicantJoining { get; set; }
        public string IsOnboarded { get; set; }
        public string JoinedDate { get; set; }
        public string ClientOnboardingDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual TblApplicantRequisitionStaffing ApplicantRequisition { get; set; }
    }
}
