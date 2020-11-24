using RecruitmentManagementDataAccess.Employee;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementProvider.Provider
{
    public class Employee : IEmployee
    {
        EmployeeDAL EmployeeDAL = new EmployeeDAL();
        public List<TblInterviewManagement> GetAllInterviews()
        {
            return EmployeeDAL.GetAllInterviews();
        }

        public List<TblInterviewManagement> GetAllInterviewsHistory(long ApplicantRequisitionID)
        {
            return EmployeeDAL.GetAllInterviewsHistory(ApplicantRequisitionID);
        }
            public List<TblInterviewManagementStaffing> GetAllClientInterviews()
        {
            return EmployeeDAL.GetAllClientInterviews();
        }
    } 
}
