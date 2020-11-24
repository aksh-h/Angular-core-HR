using RecruitmentManagementDataAccess.DAL;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using System.Collections.Generic;
using RecruitmentManagementDataAccess.Masters;
using RecruitmentManagementProvider.Helpers;
using System.Text;
using RecruitmentManagementDataAccess;
using RecruitmentManagementDataAccess.Helpers;


namespace RecruitmentManagementProvider.Provider
{
    public class RequisitionStaffing : IRequisitionStaffing
    {
        RequisitionStaffingDAL RequisitionStaffingDAL = new RequisitionStaffingDAL();
        public int CreateRequisitionStaffing(TblRequisitionStaffing tblRequisition)
        {
            return RequisitionStaffingDAL.CreateRequisitionStaffing(tblRequisition);

        }

        public int DeleteRequisitionStaffing(int key)
        {
            return RequisitionStaffingDAL.DeleteRequisitionStaffing(key);
        }

        public List<TblRequisitionStaffing> GetAllRequisitionStaffing(bool IsCancelled = true)
        {
            var result = new List<TblRequisitionStaffing>();
            if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole || CurrentContext.RoleName == RoleConstants.StaffingRole)
            {
                result = RequisitionStaffingDAL.GetAllRequisitionForRecuitersStaffing(IsCancelled);
            }
            else
            {
                result = RequisitionStaffingDAL.GetAllRequisitionStaffing(IsCancelled);
            }
            return result;



        }
        //public List<TblRequisitionStaffing> GetAllRequisitionStaffing(bool IsCancelled = true)
        //{
        //    var result = new List<TblRequisitionStaffing>();

        //    result = RequisitionStaffingDAL.GetAllRequisitionStaffing(IsCancelled);

        //    return result;

        //}

        public int UpdateRequisitionStaffing(TblRequisitionStaffing tblRequisition)
        {
            return RequisitionStaffingDAL.UpdateRequisitionStaffing(tblRequisition);
        }

        public int CloseRequisitionStaffing(TblRequisitionStaffing tblRequisition)
        {
            return RequisitionStaffingDAL.CloseRequisitionStaffing(tblRequisition);
        }

        public TblRequisitionStaffing GetRequisitionStaffingByCriteria(int key)
        {
            return RequisitionStaffingDAL.GetRequisitionByCriteriaStaffing(key);
        }

        public int MapRequisitionStaffingToApplicant(long RequisitionID, long ApplicantID, bool IsInternal)
        {
            if (IsInternal)
                return RequisitionStaffingDAL.MapRequisitionStaffingToApplicant(RequisitionID, ApplicantID);
            else
                return RequisitionStaffingDAL.MapRequisitionStaffingToApplicantClient(RequisitionID, ApplicantID);
        }

        public int UpdateRequisitionStaffingToApplicant(TblApplicantRequisitionStaffing tblApplicantRequisition)
        {
            return RequisitionStaffingDAL.UpdateRequisitionToApplicantStaffing(tblApplicantRequisition);
        }

        public List<TblApplicantRequisitionStaffing> GetAllApplicantsForRequisitionStaffing(long RequisitionID)
        {
            return RequisitionStaffingDAL.GetAllApplicantsForRequisitionStaffing(RequisitionID);
        }
        public List<TblClientsContactDetails> GetClientsContactByCriteria(int id)
        {
            return RequisitionStaffingDAL.GetClientsContactByCriteria(id);
        }
        //public int ApplicantStatusChanging()
        //{
        //   return RequisitionStaffingDAL.ApplicantStatusChanging();
           
        //}
     
    }
}



