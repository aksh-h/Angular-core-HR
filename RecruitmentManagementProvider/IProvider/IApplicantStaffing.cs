using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementProvider.IProvider
{
    public interface IApplicantStaffing
    {
        int CreateApplicants(TblApplicants tblApplicants);

        int DeleteApplicants(int key);

        Task<TblApplicants> GetApplicantsByCriteria(int key);

        int UpdateApplicants(TblApplicants tblApplicants);

        List<TblApplicants> GetAllApplicant();
        TblApplicantRequisitionStaffing GetRequisitionApplicantDetails(long key);
        List<TblEmployeePanelMapping> GetAllInterviewer();
        List<TblApplicantRequisitionStaffing> GetAllShortListedApplicant();

        List<TblApplicantRequisitionClient> GetAllShortListedApplicantClient();
        List<TblInterviewManagementStaffing> GetAllApplicantInterviewDetails(int key);
        int SaveInterviewDetails(TblInterviewManagementStaffing tblInterviewManagement);
        int UpdateInterview(TblInterviewManagementStaffing tblInterviewManagement);
        int UpdateStatusOnStaffingFeedback(TblInterviewManagementStaffing tblInterviewManagement);
        List<TblApplicantRequisitionStaffing> GetAllSelectedApplicant();
        List<TblApplicantRequisitionStaffing> GetAllRejectedApplicant();

        List<TblApplicantRequisitionStaffing> GetAllBlackListedApplicant();

        

       int CancelInterview(long id);

        List<TblInterviewManagementStaffing> GetAllInterviews(long applicantId);
        TblInterviewManagement GetInterviewById(int id);
        Task<TblSelectedApplicantsStaffing> GetApplicantJoiningDetails(int id);
        int CreateApplicantJoining(TblSelectedApplicantsStaffing tblSelectedApplicants);
        long UpdateApplicantJoining(TblSelectedApplicantsStaffing tblSelectedApplicants);
    }
}
