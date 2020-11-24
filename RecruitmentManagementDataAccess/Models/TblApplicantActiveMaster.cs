using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblApplicantActiveMaster
    {
        public int ApplicantActiveId { get; set; }
        public string ApplicantActive { get; set; }
        public bool? IsActive { get; set; }
    }
}
