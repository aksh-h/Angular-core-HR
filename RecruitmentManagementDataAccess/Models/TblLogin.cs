using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblLogin
    {
        public int LoginId { get; set; }
        public long EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual TblEmployees Employee { get; set; }
    }
}
