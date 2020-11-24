using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblJobTypeMaster
    {
        public int JobTypeId { get; set; }
        public string JobType { get; set; }
        public bool? IsActive { get; set; }
    }
}
