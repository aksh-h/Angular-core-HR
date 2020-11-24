using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblWorkFlowMaster
    {
        public int WorkFlowId { get; set; }
        public string WorkFlowJson { get; set; }
        public bool? IsActive { get; set; }
        public string WorkFlowName { get; set; }
    }
}
