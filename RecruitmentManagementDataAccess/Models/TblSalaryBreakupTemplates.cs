using System;
using System.Collections.Generic;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class TblSalaryBreakupTemplates
    {
        public int SalarayTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateJson { get; set; }
        public bool? IsAcitve { get; set; }
        public string FormalsJson { get; set; }
    }
}
