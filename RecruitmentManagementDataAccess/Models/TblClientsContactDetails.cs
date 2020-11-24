using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblClientsContactDetails
    {
        public TblClientsContactDetails()
        {
            TblLoginClient = new HashSet<TblLoginClient>();
            TblRequisitionClientContactMappingStaffing = new HashSet<TblRequisitionClientContactMappingStaffing>();
        }

        public int ContactId { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNo { get; set; }
        public string ContactPersonEmailId { get; set; }
        public string ContactPersonDesignation { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }

        public virtual TblClients Client { get; set; }
        public virtual ICollection<TblLoginClient> TblLoginClient { get; set; }
        public virtual ICollection<TblRequisitionClientContactMappingStaffing> TblRequisitionClientContactMappingStaffing { get; set; }
    }
}
