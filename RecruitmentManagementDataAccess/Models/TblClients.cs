using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblClients
    {
        public TblClients()
        {
            TblClientsContactDetails = new HashSet<TblClientsContactDetails>();
            TblRequisition = new HashSet<TblRequisition>();
            TblRequisitionStaffing = new HashSet<TblRequisitionStaffing>();
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public bool? IsActive { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public int? Pincode { get; set; }
        public DateTime? NdastartDate { get; set; }
        public DateTime? NdaendDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string TypeOfBusiness { get; set; }
        public string Specialization { get; set; }
        public decimal? AnnualRevenue { get; set; }
        public string JobBoardUrl { get; set; }
        public string JobBoardUserId { get; set; }
        public string JobBoardPassword { get; set; }
        public string Website { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TblEmployees CreatedByNavigation { get; set; }
        public virtual TblEmployees ModifiedByNavigation { get; set; }
        public virtual ICollection<TblClientsContactDetails> TblClientsContactDetails { get; set; }
        public virtual ICollection<TblRequisition> TblRequisition { get; set; }
        public virtual ICollection<TblRequisitionStaffing> TblRequisitionStaffing { get; set; }
    }
}
