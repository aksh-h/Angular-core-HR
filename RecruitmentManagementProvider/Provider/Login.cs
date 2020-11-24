using RecruitmentManagementDataAccess;
using RecruitmentManagementDataAccess.DAL;
using RecruitmentManagementDataAccess.Helpers;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementProvider.Provider
{
    public class Login : ILogin
    {
        LoginDAL LoginDAL = new LoginDAL();
        public TblEmployees Authenticate(string UserName, string Password)
        {
            return LoginDAL.Authenticate(UserName, Password);
        }
        public TblClientsContactDetails AuthenticateClient(string UserName, string Password)
        {
            return LoginDAL.AuthenticateClient(UserName, Password);
        }
        public int ApplicantStatusChanging()
        {
            return LoginDAL.ApplicantStatusChanging();

        }

        public List<int> GetCompletedInterviewCount()
        {
            var result = new List<int>();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.GetCompletedInterviewCountStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.GetCompletedInterviewCountForEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.GetCompletedInterviewCountForRecruiter();
            }
            else if (CurrentContext.RoleName == RoleConstants.Client)
            {
                result = LoginDAL.GetCompletedInterviewCountForClient();
            }

            return result;
        }

        public List<int> GetPendingInterviewCount()
        {
            var result = new List<int>();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.GetPendingInterviewCountStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.GetPendingInterviewCountForEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.GetPendingInterviewCountForRecruiter();
            }
            else if (CurrentContext.RoleName == RoleConstants.Client)
            {
                result = LoginDAL.GetPendingInterviewCountForClient();
            }
            return result;
        }

        public int TotalInterviewCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.TotalInterviewCountStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.TotalInterviewCountEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.TotalInterviewCountRecruiter();
            }


            return result;
        }

        public int WeeklyInterviewCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.WeeklyInterviewCountStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.WeeklyInterviewCountEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.WeeklyInterviewCountRecruiter();
            }


            return result;
        }

        public int MonthlyInterviewCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.MonthlyInterviewCountStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.MonthlyInterviewCountEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.MonthlyInterviewCountRecruiter();
            }
            //else if (CurrentContext.RoleName == RoleConstants.Client)
            //{
            //    result = LoginDAL.GetCompletedInterviewCountForClient();
            //}

            return result;
        }

        public int DailyInterviewCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.DailyInterviewCountStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.DailyInterviewCountEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.DailyInterviewCountRecruiter();
            }
            else if (CurrentContext.RoleName == RoleConstants.Client)
            {
                result = LoginDAL.DailyInterviewCountEmployee();
            }
            return result;
        }

        public int PendingRequisitionCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.GetPendingRequisitionStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.GetPendingRequisitionEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.GetPendingRequisitionRecruiter();
            }
            return result;
        }

        public int ApprovedRequisitionCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.GetApprovedRequisitionStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.GetApprovedRequisitionEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.GetApprovedRequisitionRecruiter();
            }
            return result;
        }

        public int CompletedRequisitionCount()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = LoginDAL.GetCompletedRequisitionStaffing();
            }
            else if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                result = LoginDAL.GetCompletedRequisitionEmployee();
            }
            else if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = LoginDAL.GetCompletedRequisitionRecruiter();
            }
            return result;
        }

        public int StaffingRequisitionAssignedTo()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole)   // doubt (staffingrole ?)
            {
                result = LoginDAL.StaffingRequisitionAssignedTo();
            }
            return result;
        }

        public int RecruiterRequisitionAssignedTo()
        {
            var result = new int();
            if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole)   //
            {
                result = LoginDAL.RecruiterRequisitionAssignedTo();    
            }
            return result;
        }



    }
}


