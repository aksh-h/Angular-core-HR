using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblLocationMaster
    {
        public int LocationId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string LocationName { get; set; }
        public bool? IsActive { get; set; }
    }
}
