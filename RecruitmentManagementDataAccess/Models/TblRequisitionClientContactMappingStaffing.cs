using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblRequisitionClientContactMappingStaffing
    {
        public int RequisitionContactId { get; set; }
        public long RequisitionId { get; set; }
        public int ContactId { get; set; }

        public virtual TblClientsContactDetails Contact { get; set; }
        public virtual TblRequisitionStaffing Requisition { get; set; }
    }
}
