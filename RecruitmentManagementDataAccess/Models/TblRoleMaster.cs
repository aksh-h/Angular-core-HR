using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblRoleMaster
    {
        public TblRoleMaster()
        {
            TblEmployees = new HashSet<TblEmployees>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblEmployees> TblEmployees { get; set; }
    }
}
