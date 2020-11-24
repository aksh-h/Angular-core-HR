using RecruitmentManagementDataAccess.ApplicantStaffing;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementProvider.Provider
{
    public class ApplicantStaffing : IApplicantStaffing
    {
        ApplicantStaffingDAL ApplicantStaffingDAL = new ApplicantStaffingDAL();
        public int CreateApplicants(TblApplicants tblApplicants)
        {
            return ApplicantStaffingDAL.CreateApplicants(tblApplicants);
        }

        public int DeleteApplicants(int key)
        {
            return ApplicantStaffingDAL.DeleteApplicants(key);
        }

        public int UpdateApplicants(TblApplicants tblApplicants)
        {
            return ApplicantStaffingDAL.UpdateApplicants(tblApplicants);
        }

        public async Task<TblApplicants> GetApplicantsByCriteria(int key)
        {
            return await ApplicantStaffingDAL.GetApplicantsByCriteria(key);
        }
        public List<TblApplicants> GetAllApplicant()
        {
            return ApplicantStaffingDAL.GetAllApplicant();
        }

        public TblApplicantRequisitionStaffing GetRequisitionApplicantDetails(long key)
        {
            return ApplicantStaffingDAL.GetRequisitionApplicantDetails(key);
        }

        public List<TblEmployeePanelMapping> GetAllInterviewer()
        {
            return ApplicantStaffingDAL.GetAllInterviewer();
        }

        public List<TblApplicantRequisitionStaffing> GetAllShortListedApplicant()
        {
            return ApplicantStaffingDAL.GetAllShortListedApplicant();
        }

        public List<TblApplicantRequisitionClient> GetAllShortListedApplicantClient()
        {
            return ApplicantStaffingDAL.GetAllShortListedApplicantClient();
        }

        public List<TblInterviewManagementStaffing> GetAllApplicantInterviewDetails(int key)
        {
            return ApplicantStaffingDAL.GetAllApplicantInterviewDetails(key);
        }

        public int SaveInterviewDetails(TblInterviewManagementStaffing tblInterviewManagement)
        {
            return ApplicantStaffingDAL.SaveInterviewDetails(tblInterviewManagement);
        }

        public int UpdateInterview(TblInterviewManagementStaffing tblInterviewManagement)
        {
            return ApplicantStaffingDAL.UpdateInterview(tblInterviewManagement);
        }

        public int UpdateStatusOnStaffingFeedback(TblInterviewManagementStaffing tblInterviewManagement)
        {
            return ApplicantStaffingDAL.UpdateStatusOnStaffingFeedback(tblInterviewManagement);
        }

        public List<TblApplicantRequisitionStaffing> GetAllSelectedApplicant()
        {
            return ApplicantStaffingDAL.GetAllSelectedApplicant();
        }

        public List<TblApplicantRequisitionStaffing> GetAllRejectedApplicant()
        {
            return ApplicantStaffingDAL.GetAllRejectedApplicant();
        }

        public List<TblApplicantRequisitionStaffing> GetAllBlackListedApplicant()
        {
            return ApplicantStaffingDAL.GetAllBlackListedApplicant();
        }

        public int CancelInterview(long id)
        {
            return ApplicantStaffingDAL.CancelInterview(id);
        }

        public List<TblInterviewManagementStaffing> GetAllInterviews(long applicantId)
        {
            return ApplicantStaffingDAL.GetAllInterviews(applicantId);
        }

        public TblInterviewManagement GetInterviewById(int id)
        {
            return ApplicantStaffingDAL.GetInterviewById(id);
        }

        public async Task<TblSelectedApplicantsStaffing> GetApplicantJoiningDetails(int id)
        {
            return await ApplicantStaffingDAL.GetApplicantJoiningDetails(id);
        }
        public int CreateApplicantJoining(TblSelectedApplicantsStaffing tblSelectedApplicants)
        {
            return ApplicantStaffingDAL.CreateApplicantJoining(tblSelectedApplicants);
        }
        public long UpdateApplicantJoining(TblSelectedApplicantsStaffing tblSelectedApplicants)
        {
            return ApplicantStaffingDAL.UpdateApplicantJoining(tblSelectedApplicants);
        }
    }
}
