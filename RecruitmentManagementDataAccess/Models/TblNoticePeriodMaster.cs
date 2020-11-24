using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblNoticePeriodMaster
    {
        public int NoticePeriodId { get; set; }
        public string NoticePeriod { get; set; }
        public bool? IsActive { get; set; }
    }
}
