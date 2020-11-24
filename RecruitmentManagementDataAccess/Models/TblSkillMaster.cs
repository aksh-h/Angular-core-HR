using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblSkillMaster
    {
        public TblSkillMaster()
        {
            TblRequisitionSkillMapping = new HashSet<TblRequisitionSkillMapping>();
            TblRequisitionSkillMappingStaffing = new HashSet<TblRequisitionSkillMappingStaffing>();
        }

        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblRequisitionSkillMapping> TblRequisitionSkillMapping { get; set; }
        public virtual ICollection<TblRequisitionSkillMappingStaffing> TblRequisitionSkillMappingStaffing { get; set; }
    }
}
