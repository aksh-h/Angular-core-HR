using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblRequisitionApplicantMapping
    {
        public int RequisitionApplicantId { get; set; }
        public long RequistionId { get; set; }
        public long ApplicantId { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblApplicants Applicant { get; set; }
        public virtual TblRequisition Requistion { get; set; }
    }
}
