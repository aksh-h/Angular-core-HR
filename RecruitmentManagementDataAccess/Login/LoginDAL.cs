using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagementDataAccess.Helpers;

namespace RecruitmentManagementDataAccess.DAL
{
    public  class LoginDAL
    {
        private readonly  RecruitmentManagementContext dbContext;
        public LoginDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }
        public  TblEmployees Authenticate(string UserName, string Password)
        {
            var obj = dbContext.TblLogin.Where(r => r.UserName == UserName && r.Password == Password);
            if (obj.Count() > 0)
            {
                return dbContext.TblEmployees.Where(r => r.EmployeeId == obj.FirstOrDefault().EmployeeId).Include(r => r.Role).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public  TblClientsContactDetails AuthenticateClient(string UserName, string Password)
        {
            var obj = dbContext.TblLoginClient.Where(r => r.UserName == UserName && r.Password == Password);
            if (obj.Count() > 0)
            {
                return dbContext.TblClientsContactDetails.Where(r => r.ContactId == obj.FirstOrDefault().ContactId).Include(r => r.Client).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public  int ApplicantStatusChanging()
        {
            var Id = dbContext.TblApplicantRequisitionStaffing.Where(r => r.Status == "Selected" && r.TentativeOnboardingDate < DateTime.Now).ToList();
            foreach (TblApplicantRequisitionStaffing interviewer in Id)
            {
                if (dbContext.TblSelectedApplicantsStaffing.Where(a => a.ApplicantRequisitionId == interviewer.ApplicantRequisitionId && a.IsOnboarded== "No").Count() < 1)
                {
                    interviewer.Status = ApplicantStatus.Available;
                    interviewer.ModifiedDate = DateTime.Now;
                    interviewer.ModifiedBy = CurrentContext.EmployeeID;
                }

            }

            return 1;

        }

        #region InterviewCount

        public List<int> GetCompletedInterviewCountStaffing()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            //var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true).ToList();
            var dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true).ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                //int interviewCount = data.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(StaffingInterviewCount);
            }
            return result;
        }

        public  List<int> GetPendingInterviewCountStaffing()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            //var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == false).ToList();
            var dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == false).ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                //int interviewCount = data.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add( StaffingInterviewCount);
            }
            return result;
        }

        private  Dictionary<DateTime, DateTime> GetWeekStartDate()
        {
            Dictionary<DateTime, DateTime> result = new Dictionary<DateTime, DateTime>();
            string week = DateTime.Now.DayOfWeek.ToString();
            int reducedate = 0;
            int increasedate = 0;
            switch (week)
            {
                case "Sunday":
                    reducedate = 6; increasedate = 0;
                    break;
                case "Saturday":
                    reducedate = 5; increasedate = 1;
                    break;
                case "Friday":
                    reducedate = 4; increasedate = 2;
                    break;
                case "Thursday":
                    reducedate = 3; increasedate = 3;
                    break;
                case "Wednesday":
                    reducedate = 2; increasedate = 4;
                    break;
                case "Tuesday":
                    reducedate = 1; increasedate = 5;
                    break;
                case "Monday":
                    reducedate = 0; increasedate = 6;
                    break;
            }
            DateTime startdate = DateTime.Now.AddDays(-reducedate);
            DateTime enddate = DateTime.Now.AddDays(increasedate);
            result.Add(startdate, enddate);
            return result;
        }

        public  List<int> GetCompletedInterviewCountForEmployee()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).ToList();
            var dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMappingStaffing.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                int interviewCount = data.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(interviewCount + StaffingInterviewCount);
            }
            return result;
        }

        public  List<int> GetPendingInterviewCountForEmployee()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == false && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).ToList();
            var dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == false && r.TblInterviewEmployeeMappingStaffing.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                int interviewCount = data.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(StaffingInterviewCount);
            }
            return result;
        }

        public  List<int> GetCompletedInterviewCountForRecruiter()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true).ToList();
            //var dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true).ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                int interviewCount = data.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                //int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(interviewCount);
            }
            return result;
        }

        public  List<int> GetPendingInterviewCountForRecruiter()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == false).ToList();
            //var dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true).ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                int interviewCount = data.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                //int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(interviewCount);
            }
            return result;
        }

        public  List<int> GetCompletedInterviewCountForClient()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            var dataStaffing = dbContext.TblInterviewManagementStaffing.AsNoTracking().Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.RoundName == "Client" && r.ApplicantRequisition.Requisition.TblRequisitionClientContactMappingStaffing.Any(u => u.ContactId == CurrentContext.EmployeeID)).AsNoTracking().ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(StaffingInterviewCount);
            }
            return result;
        }

        public  List<int> GetPendingInterviewCountForClient()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            List<int> result = new List<int>();
            var dataStaffing = dbContext.TblInterviewManagementStaffing.AsNoTracking().Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == false && r.RoundName == "Client" && r.ApplicantRequisition.Requisition.TblRequisitionClientContactMappingStaffing.Any(u => u.ContactId == CurrentContext.EmployeeID)).AsNoTracking().ToList();
            for (int i = 0; 7 > i; i++)
            {
                DateTime qryDate = fromDate.AddDays(i);
                int StaffingInterviewCount = dataStaffing.Where(r => r.InterviewDate.Value.ToShortDateString() == qryDate.ToShortDateString()).Count();
                result.Add(StaffingInterviewCount);
            }
            return result;
        }

        
        public int TotalInterviewCountStaffing()
        {
            return dbContext.TblInterviewManagementStaffing.Count();
        }
        public  int TotalInterviewCountRecruiter()
        {
           return dbContext.TblInterviewManagement.Count();
        }
        public  int TotalInterviewCountEmployee()
        {
            
           
            int data = dbContext.TblInterviewManagement.Where(r =>  r.IsCompleted == true && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).Count();
            int dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r =>  r.IsCompleted == true && r.TblInterviewEmployeeMappingStaffing.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).Count();
            return data + dataStaffing;

        }

        public  int WeeklyInterviewCountStaffing()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            //var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).ToList();
            int dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMappingStaffing.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).Count();
            return dataStaffing;
        }
        public int WeeklyInterviewCountRecruiter()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
            //var data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).ToList();
            int data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).Count();
            return data;
        }
        public  int WeeklyInterviewCountEmployee()
        {
            Dictionary<DateTime, DateTime> keyValuePairs = GetWeekStartDate();
            DateTime fromDate = keyValuePairs.ElementAt(0).Key, ToDate = keyValuePairs.ElementAt(0).Value;
           
            int data = dbContext.TblInterviewManagement.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).Count();
            int dataStaffing = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewDate >= fromDate && r.InterviewDate <= ToDate && r.IsCompleted == true && r.TblInterviewEmployeeMappingStaffing.Any(k => k.EmployeeId == CurrentContext.EmployeeID)).Count();
            return data + dataStaffing;
        }

        public int MonthlyInterviewCountStaffing()
        {
            //List<int> result = new List<int>();
            //var data = dbContext.TblInterviewManagement.Where(x => x.InterviewDate.Value.Month == DateTime.Now.Month).Count();
            int staffingdata = dbContext.TblInterviewManagementStaffing.Where(x => x.InterviewDate.Value.Month == DateTime.Now.Month).Count();
            //result.Add(staffingdata);
            //return result;
            return staffingdata;
        }
        public  int MonthlyInterviewCountRecruiter()
        {
            //List<int> result = new List<int>();
            int data = dbContext.TblInterviewManagement.Where(x => x.InterviewDate.Value.Month == DateTime.Now.Month).Count();
            //var staffingdata = dbContext.TblInterviewManagementStaffing.Where(x => x.InterviewDate.Value.Month == DateTime.Now.Month).Count();
            return data;
        }
        public  int MonthlyInterviewCountEmployee()
        {
            
            int staffingdata = dbContext.TblInterviewManagementStaffing.Where(x => x.InterviewDate.Value.Month == DateTime.Now.Month).Count();
            int data = dbContext.TblInterviewManagement.Where(x => x.InterviewDate.Value.Month == DateTime.Now.Month).Count();
            return staffingdata + data;
        }

        public int DailyInterviewCountStaffing()
        {
            int staffingdata = dbContext.TblInterviewManagementStaffing.Where(x => x.InterviewDate.Value.Day == DateTime.Now.Day).Count();
            return staffingdata;
        }

        public int DailyInterviewCountRecruiter()
        {
            int data = dbContext.TblInterviewManagement.Where(x => x.InterviewDate.Value.Day == DateTime.Now.Day).Count();
            return data;
        }

        public int DailyInterviewCountEmployee()
        {

            int staffingdata = dbContext.TblInterviewManagementStaffing.Where(x => x.InterviewDate.Value.Day == DateTime.Now.Day).Count();
            int data = dbContext.TblInterviewManagement.Where(x => x.InterviewDate.Value.Day == DateTime.Now.Day).Count();
            return staffingdata + data;
        }

        #endregion

        #region Requisition Count
        public int GetPendingRequisitionStaffing()
        {
            //List<int> result = new List<int>();
            int dataStaffing = dbContext.TblRequisitionStaffing.Where(r => r.Status == "Inprogress").Count();
            return dataStaffing;
        }

        public int GetPendingRequisitionRecruiter()
        {
            int data = dbContext.TblRequisition.Where(r => r.Status == "Open").Count();
            return data;
        }

        public int GetPendingRequisitionEmployee()
        {
            //List<int> result = new List<int>();
            int data = dbContext.TblApplicantRequisition.Where(r => r.Status == "NotAssigned").Count();

            return data;
        }

        public int GetApprovedRequisitionStaffing()
        {

            int dataStaffing = dbContext.TblRequisitionStaffing.Where(r => r.Status == "Approved").Count();

            return dataStaffing;
        }

        public int GetApprovedRequisitionRecruiter()
        {

            int data = dbContext.TblRequisition.Where(r => r.Status == "Approved").Count();

            return data;
        }

        public int GetApprovedRequisitionEmployee()
        {

            int data = dbContext.TblApplicantRequisition.Where(r => r.Status == " ").Count(); //no matching status

            return data;
        }

        public int GetCompletedRequisitionStaffing()
        {
            //List<int> result = new List<int>();
            int dataStaffing = dbContext.TblRequisitionStaffing.Where(r => r.Status == " ").Count();  //status issue
            return dataStaffing;
        }

        public int GetCompletedRequisitionRecruiter()
        {
            //List<int> result = new List<int>();
            int data = dbContext.TblRequisition.Where(r => r.Status == "Closed ").Count();
            return data;
        }

        public int GetCompletedRequisitionEmployee()
        {
            int data = dbContext.TblApplicantRequisition.Where(r => r.Status == " ").Count(); //no matching status
            return data;
        }


        #endregion

        #region Requisition Assigned

        public int StaffingRequisitionAssignedTo()
        {
            int staffingdata=dbContext.TblRequisitionRecruiterMapping.Where(r => r.RequisitionEmployeeAssigneeId == CurrentContext.EmployeeID).Count();
            return staffingdata;
        }

        public int RecruiterRequisitionAssignedTo()
        {
            int data = dbContext.TblRequisitionRecruiterMappingStaffing.Where(r => r.RequisitionEmployeeAssigneeId == CurrentContext.EmployeeID).Count();
            return data;
        }


        #endregion


    }
}
