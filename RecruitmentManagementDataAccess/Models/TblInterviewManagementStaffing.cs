using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblInterviewManagementStaffing
    {
        public TblInterviewManagementStaffing()
        {
            TblInterviewEmployeeMappingStaffing = new HashSet<TblInterviewEmployeeMappingStaffing>();
        }

        public long InterviewId { get; set; }
        public string InterviewPanel { get; set; }
        public long ApplicantId { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string Comments { get; set; }
        public bool? IsCompleted { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long ApplicantRequisitionId { get; set; }
        public string Status { get; set; }
        public string Venue { get; set; }
        public string FeedBack { get; set; }
        public Guid? EmailGuid { get; set; }
        public int? Communication { get; set; }
        public int? Attitude { get; set; }
        public string SkillRatings { get; set; }
        public string RoundName { get; set; }
        public string ModeOfInterview { get; set; }
        public bool? IsClient { get; set; }
        public bool? SendFeedbacktoClient { get; set; }
        public string RecruitersFeedBack { get; set; }

        public virtual TblApplicants Applicant { get; set; }
        public virtual TblApplicantRequisitionStaffing ApplicantRequisition { get; set; }
        public virtual TblEmployees CreatedByNavigation { get; set; }
        public virtual TblEmployees ModifiedByNavigation { get; set; }
        public virtual ICollection<TblInterviewEmployeeMappingStaffing> TblInterviewEmployeeMappingStaffing { get; set; }
    }
}
