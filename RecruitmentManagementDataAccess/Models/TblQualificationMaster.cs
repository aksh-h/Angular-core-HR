using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblQualificationMaster
    {
        public int QualificationId { get; set; }
        public string Qualification { get; set; }
        public bool? IsActive { get; set; }
    }
}
