using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementProvider.IProvider
{
    public interface ILogin
    {
        TblEmployees Authenticate(string UserName, string Password);
        TblClientsContactDetails AuthenticateClient(string UserName, string Password);
        int ApplicantStatusChanging();

        
        List<int> GetCompletedInterviewCount();
        List<int> GetPendingInterviewCount();

        int TotalInterviewCount();
        int WeeklyInterviewCount();
        int MonthlyInterviewCount();
        int DailyInterviewCount();

        int PendingRequisitionCount();
        int ApprovedRequisitionCount();
        int CompletedRequisitionCount();

        int StaffingRequisitionAssignedTo();
        int RecruiterRequisitionAssignedTo();


    }

}
