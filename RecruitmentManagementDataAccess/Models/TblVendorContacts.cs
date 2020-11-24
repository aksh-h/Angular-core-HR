using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblVendorContacts
    {
        public int ContactId { get; set; }
        public int VendorId { get; set; }
        public string ContactphoneNo { get; set; }
        public string ContactPersonEmailId { get; set; }
        public string Designation { get; set; }
        public bool? IsActive { get; set; }
        public string ContactPerson { get; set; }

        public virtual TblVendors Vendor { get; set; }
    }
}
