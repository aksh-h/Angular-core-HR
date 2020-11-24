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
    public class Requisition : IRequisition
    {
        RequisitionDAL RequisitionDAL = new RequisitionDAL();
        public int CreateRequisition(TblRequisition tblRequisition)
        {
            return RequisitionDAL.CreateRequisition(tblRequisition);
           
        }

        public int DeleteRequisition(int key)
        {
            return RequisitionDAL.DeleteRequisition(key);
        }

        public List<TblRequisition> GetAllRequisition(bool IsCancelled = true)
        {
            var result = new List<TblRequisition>();
            if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole || CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                result = RequisitionDAL.GetAllRequisitionForRecuiters(IsCancelled);
            }
            else
            {
                result = RequisitionDAL.GetAllRequisition(IsCancelled);
            }
            return result;
            
        }

        public int UpdateRequisition(TblRequisition tblRequisition)
        {
            return RequisitionDAL.UpdateRequisition(tblRequisition);
        }

        public int CloseRequisition(TblRequisition tblRequisition)
        {
            return RequisitionDAL.CloseRequisition(tblRequisition);
        }

        public TblRequisition GetRequisitionByCriteria(int key)
        {
            return RequisitionDAL.GetRequisitionByCriteria(key);
        }

        public int MapRequisitionToApplicant(long RequisitionID, long ApplicantID, bool IsInternal)
        {
            if (IsInternal)
                return RequisitionDAL.MapRequisitionToApplicant(RequisitionID, ApplicantID);
            else
                return RequisitionDAL.MapRequisitionToApplicantClient(RequisitionID, ApplicantID);
        }

        public int UpdateRequisitionToApplicant(TblApplicantRequisition tblApplicantRequisition)
        {
            return RequisitionDAL.UpdateRequisitionToApplicant(tblApplicantRequisition);
        }

        public List<TblApplicantRequisition> GetAllApplicantsForRequisition(long RequisitionID)
        {
            return RequisitionDAL.GetAllApplicantsForRequisition(RequisitionID);
        }

    }
}
