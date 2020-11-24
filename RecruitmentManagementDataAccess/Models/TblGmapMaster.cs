using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblGmapMaster
    {
        public int GmapId { get; set; }
        public int AddressId { get; set; }
        public string Lattiude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
