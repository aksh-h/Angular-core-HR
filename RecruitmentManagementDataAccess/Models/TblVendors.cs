using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblVendors
    {
        public TblVendors()
        {
            TblVendorContacts = new HashSet<TblVendorContacts>();
        }

        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime? NdastartDate { get; set; }
        public DateTime? NdaendDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public string TypeofBusiness { get; set; }
        public string Specialization { get; set; }
        public string Website { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public int? PinCode { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblEmployees CreatedByNavigation { get; set; }
        public virtual TblEmployees ModifiedByNavigation { get; set; }
        public virtual ICollection<TblVendorContacts> TblVendorContacts { get; set; }
    }
}
