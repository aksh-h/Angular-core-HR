using RecruitmentManagementDataAccess.Masters;
using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementProvider.IProvider
{
    public interface IRequisition
    {
        int CreateRequisition(TblRequisition requisition);
        
        int DeleteRequisition(int key);

        List<TblRequisition> GetAllRequisition(bool IsCancelled = true);

        TblRequisition GetRequisitionByCriteria(int key);

        int UpdateRequisition(TblRequisition tblRequisition);

        int CloseRequisition(TblRequisition tblRequisition);

        int MapRequisitionToApplicant(long RequisitionID , long ApplicantID , bool IsInternal);

        int UpdateRequisitionToApplicant(TblApplicantRequisition tblApplicantRequisition);

        List<TblApplicantRequisition> GetAllApplicantsForRequisition(long RequisitionID);
    }
}
