﻿using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblInterviewEmployeeMappingStaffing
    {
        public int InterviewEmployeeMappingId { get; set; }
        public long? InterviewId { get; set; }
        public long? EmployeeId { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblEmployees Employee { get; set; }
        public virtual TblInterviewManagementStaffing Interview { get; set; }
    }
}