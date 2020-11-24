using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblRequisitionSkillMappingStaffing
    {
        public long RequisitionSkillId { get; set; }
        public long RequisitionId { get; set; }
        public int? SkillId { get; set; }

        public virtual TblRequisitionStaffing Requisition { get; set; }
        public virtual TblSkillMaster Skill { get; set; }
    }
}
