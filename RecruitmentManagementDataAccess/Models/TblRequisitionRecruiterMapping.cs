using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblRequisitionRecruiterMapping
    {
        public long RequisitionEmployeeAssigneeId { get; set; }
        public long EmployeeId { get; set; }
        public long RequisitionId { get; set; }

        public virtual TblEmployees Employee { get; set; }
        public virtual TblRequisition Requisition { get; set; }
    }
}
