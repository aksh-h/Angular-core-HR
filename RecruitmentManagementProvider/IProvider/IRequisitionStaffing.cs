using RecruitmentManagementDataAccess.Masters;
using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementProvider.IProvider
{
    public interface IRequisitionStaffing
    {

        int CreateRequisitionStaffing(TblRequisitionStaffing requisition);

        int DeleteRequisitionStaffing(int key);

        List<TblRequisitionStaffing> GetAllRequisitionStaffing(bool IsCancelled = true);

        TblRequisitionStaffing GetRequisitionStaffingByCriteria(int key);

        int UpdateRequisitionStaffing(TblRequisitionStaffing tblRequisition);

        int CloseRequisitionStaffing(TblRequisitionStaffing tblRequisition);

        int MapRequisitionStaffingToApplicant(long RequisitionID, long ApplicantID, bool IsInternal);

        int UpdateRequisitionStaffingToApplicant(TblApplicantRequisitionStaffing tblApplicantRequisition);

        List<TblApplicantRequisitionStaffing> GetAllApplicantsForRequisitionStaffing(long RequisitionID);
        List<TblClientsContactDetails> GetClientsContactByCriteria(int id);
        //List<TblApplicantRequisitionClient> ApplicantStatusChanging();

        //int ApplicantStatusChanging();
    }
}


