using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblDepartmentMaster
    {
        public TblDepartmentMaster()
        {
            TblEmployees = new HashSet<TblEmployees>();
            TblRequisition = new HashSet<TblRequisition>();
            TblRequisitionStaffing = new HashSet<TblRequisitionStaffing>();
        }

        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblEmployees> TblEmployees { get; set; }
        public virtual ICollection<TblRequisition> TblRequisition { get; set; }
        public virtual ICollection<TblRequisitionStaffing> TblRequisitionStaffing { get; set; }
    }
}
