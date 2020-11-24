using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementDataAccess
{
    public static class CurrentContext
    {
        public static long EmployeeID { get; set; }

        public static long RoleID { get; set; }

        public static string RoleName { get; set; }
    }
}
