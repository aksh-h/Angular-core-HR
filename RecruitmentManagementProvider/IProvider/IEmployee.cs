using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementProvider.IProvider
{
    public interface IEmployee
    {
        List<TblInterviewManagement> GetAllInterviews();
        List<TblInterviewManagementStaffing> GetAllClientInterviews();
        List<TblInterviewManagement> GetAllInterviewsHistory(long ApplicantRequisitionID);
    }
}
