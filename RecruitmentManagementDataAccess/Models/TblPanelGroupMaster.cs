using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblPanelGroupMaster
    {
        public TblPanelGroupMaster()
        {
            TblEmployeePanelMapping = new HashSet<TblEmployeePanelMapping>();
        }

        public int PanelGroupId { get; set; }
        public string PanelGroupName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblEmployeePanelMapping> TblEmployeePanelMapping { get; set; }
    }
}
