using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementDataAccess.ModelExtensions
{
    public class SalaryBreakModel
    {
        public List<SalaryBreakUpRow> Rows { get; set; }

    }
    public class SalaryBreakUpRow
    {
        public int RowID { get; set; }
        public SalaryBreakUpColumnProperties ColumnA { get; set; }
        public SalaryBreakUpColumnProperties ColumnB { get; set; }
        public SalaryBreakUpColumnProperties ColumnC { get; set; }
    }
    public class SalaryBreakUpColumnProperties
    {
        public bool IsBold { get; set; }
        public string Value { get; set; }
        public bool IsNumeric { get; set; }
        public bool HasFormula { get; set; }
        public string Formula { get; set; }


    }

    public class ResumeUpload
    {
        public int? SourceId { get; set; }
        public string EmployeeEmailId { get; set; }
        public int? PortalId { get; set; }
        public int? VendorId { get; set; }
    }

}
