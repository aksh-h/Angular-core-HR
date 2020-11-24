using Microsoft.AspNetCore.Http;
using RecruitmentManagementDataAccess.ModelExtensions;
using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementProvider.IProvider
{
    public interface IApplicant
    {
        int CreateApplicants(TblApplicants tblApplicants);

        int DeleteApplicants(int key);

        TblApplicants GetApplicantsByCriteria(int key);

        int UpdateApplicants(TblApplicants tblApplicants);
        List<TblApplicants> GetAllApplicant();
        List<TblApplicants> GetAllApplicantFromRequisition(int RequisitionId);
        TblApplicantRequisition GetRequisitionApplicantDetails(long key);
        List<TblEmployeePanelMapping> GetAllInterviewer();
        List<TblApplicantRequisition> GetAllShortListedApplicant();

        List<TblApplicantRequisitionClient> GetAllShortListedApplicantClient();
        List<TblInterviewManagement> GetAllApplicantInterviewDetails(int key);
        int SaveInterviewDetails(TblInterviewManagement tblInterviewManagement);
        int UpdateInterview(TblInterviewManagement tblInterviewManagement);
        List<TblApplicantRequisition> GetAllSelectedApplicant();
        List<TblApplicantRequisition> GetAllRejectedApplicant();

        List<TblApplicantRequisition> GetAllBlackListedApplicant();
        int CancelInterview(long id);

        List<TblInterviewManagement> GetAllInterviews(long applicantId);

        Task<TblSelectedApplicants> GetInternalApplicantJoiningDetails(int id);

        int CreateInternalApplicantJoining(TblSelectedApplicants tblSelectedApplicants);
        long UpdateInternalApplicantJoining(TblSelectedApplicants tblSelectedApplicants);

        string ImportExcel(IFormFile file);
        int ResumeImport(string path, ResumeUpload resumeUpload);
    }
}
