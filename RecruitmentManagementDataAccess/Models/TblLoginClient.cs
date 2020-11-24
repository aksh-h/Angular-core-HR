using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblLoginClient
    {
        public int LoginClientId { get; set; }
        public int ContactId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual TblClientsContactDetails Contact { get; set; }
    }
}
