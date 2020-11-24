using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblEmployeePanelMapping
    {
        public int EmployeePanelMappingId { get; set; }
        public long? EmployeeId { get; set; }
        public int PanelGroupId { get; set; }
        public bool IsActive { get; set; }

        public virtual TblEmployees Employee { get; set; }
        public virtual TblPanelGroupMaster PanelGroup { get; set; }
    }
}
