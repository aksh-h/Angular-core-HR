using RecruitmentManagementDataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using RecruitmentManagementProvider.Helpers;
using System.Text;
using System.Reflection.Metadata;
using RecruitmentManagementDataAccess.Helpers;

namespace RecruitmentManagementDataAccess.Masters
{
    public  class RequisitionDAL
    {
        ApplicantDAL ApplicantDAL = new ApplicantDAL();
        private readonly  RecruitmentManagementContext dbContext;
        public RequisitionDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }

        #region Requisition

        public  int CreateRequisition(TblRequisition tblRequisition)
        {
            tblRequisition.IsActive = true;
            tblRequisition.CreatedDate = DateTime.Now;
            tblRequisition.CreatedBy = CurrentContext.EmployeeID;
            tblRequisition.Status = RequisitionStatus.Open;
            tblRequisition.ManagerEmployeeId = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == CurrentContext.EmployeeID).FirstOrDefault().ReportingManagerId;
            tblRequisition.CurrentOwner = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.ManagerEmployeeId).FirstOrDefault().EmployeeName;
            dbContext.Entry(tblRequisition).State = EntityState.Added;
            dbContext.TblRequisition.Add(tblRequisition);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblRequisition).State = EntityState.Detached;
            foreach (TblRequisitionSkillMapping mapping in tblRequisition.TblRequisitionSkillMapping)
            {
                mapping.RequisitionId = tblRequisition.RequistionId;
                dbContext.TblRequisitionSkillMapping.Add(mapping);
            }
            dbContext.SaveChanges();
            var obj = dbContext.TblRequisition.AsNoTracking().Where(r => r.RequistionId == tblRequisition.RequistionId).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMapping).FirstOrDefault();
            if (result > 0)
            {
                SendRequisitionRaisedMail(obj);
            }
            dbContext.Entry(obj).State = EntityState.Detached;
            return result;
        }

        public  int DeleteRequisition(int key)
        {
            TblRequisition tblRequisition = dbContext.TblRequisition.Where(e => e.RequistionId == key).FirstOrDefault();
            tblRequisition.IsActive = false;
            dbContext.Entry(tblRequisition).State = EntityState.Modified;
            dbContext.TblRequisition.Update(tblRequisition);
            dbContext.SaveChanges();
            dbContext.Entry(tblRequisition).State = EntityState.Detached;
            dbContext.TblRequisitionSkillMapping.RemoveRange(dbContext.TblRequisitionSkillMapping.Where(r => r.RequisitionId == tblRequisition.RequistionId));
            dbContext.SaveChanges();
            return 1;
        }

        public  List<TblRequisition> GetAllRequisition(bool IsCancelled = true)
        {
            List<TblRequisition> RaisedRequisition = new List<TblRequisition>();
            if (IsCancelled)
                RaisedRequisition = dbContext.TblRequisition.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == CurrentContext.EmployeeID).Include(r => r.Department).Include(r => r.Designation).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
            else
                RaisedRequisition = dbContext.TblRequisition.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == CurrentContext.EmployeeID && r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).Include(r => r.Department).Include(r => r.Designation).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
            if (CurrentContext.RoleName == RoleConstants.ManagerRole)
            {
                long[] EmployeeIDs = dbContext.TblEmployees.AsNoTracking().Where(r => r.ReportingManagerId == CurrentContext.EmployeeID).Select(e => e.EmployeeId).ToArray();
                for (int i = 0; EmployeeIDs.Length > i; i++)
                {
                    var Requisition = new List<TblRequisition>();
                    if (IsCancelled)
                        Requisition = dbContext.TblRequisition.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == EmployeeIDs[i]).Include(r => r.CreatedByNavigation.Role).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                    else
                        Requisition = dbContext.TblRequisition.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == EmployeeIDs[i] && r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).Include(r => r.CreatedByNavigation.Role).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                    if (Requisition.Count > 0)
                    {
                        for (int p = 0; Requisition.Count > p; p++)
                        {
                            RaisedRequisition.Add(Requisition[p]);
                        }
                    }

                }
            }
            return RaisedRequisition.OrderBy(r => r.RequistionId).ToList();
        }

        public  List<TblRequisition> GetAllRequisitionForRecuiters(bool IsCancelled = true)
        {
            List<TblRequisition> RaisedRequisition = new List<TblRequisition>();
            if (CurrentContext.RoleName == RoleConstants.RecruiterLeadRole)
            {
                if (IsCancelled)
                {
                    RaisedRequisition = dbContext.TblRequisition.AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Open && r.Status != RequisitionStatus.Declined).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                    var ClientRequisition = dbContext.TblRequisition.AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.ClientId != 0 && r.ClientId != null && r.CreatedBy == CurrentContext.EmployeeID).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                    foreach (TblRequisition requisition in ClientRequisition)
                    {
                        if (RaisedRequisition.Where(r => r.RequistionId == requisition.RequistionId).Count() < 1)
                            RaisedRequisition.Add(requisition);
                    }
                }
                else
                {
                    RaisedRequisition = dbContext.TblRequisition.Where(r => r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Open && r.Status != RequisitionStatus.Declined).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                    var ClientRequisition = dbContext.TblRequisition.Where(r => r.ClientId != 0 && r.ClientId != null && r.CreatedBy == CurrentContext.EmployeeID).AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Open && r.Status != RequisitionStatus.Declined).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                    foreach (TblRequisition requisition in ClientRequisition)
                    {
                        if (RaisedRequisition.Where(r => r.RequistionId == requisition.RequistionId).Count() < 1)
                            RaisedRequisition.Add(requisition);
                    }
                }
            }
            else
            {
                if (IsCancelled)
                    RaisedRequisition = dbContext.TblRequisition.AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Declined && r.TblRequisitionRecruiterMapping.Any(t => t.EmployeeId == CurrentContext.EmployeeID)).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();
                else
                    RaisedRequisition = dbContext.TblRequisition.Where(r => r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Declined && r.TblRequisitionRecruiterMapping.Any(t => t.EmployeeId == CurrentContext.EmployeeID)).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMapping).ThenInclude(u => u.Skill).AsNoTracking().ToList();

            }
            return RaisedRequisition.OrderBy(r => r.RequistionId).ToList();
        }

        public  int UpdateRequisition(TblRequisition tblRequisition)
        {
            tblRequisition.ModifiedDate = DateTime.Now;
            tblRequisition.IsActive = true;
            tblRequisition.ModifiedBy = CurrentContext.EmployeeID;
            tblRequisition.ManagerEmployeeId = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == CurrentContext.EmployeeID).AsNoTracking().FirstOrDefault().ReportingManagerId;
            foreach (TblRequisitionSkillMapping tblRequisitionSkillMapping in dbContext.TblRequisitionSkillMapping.Where(r => r.RequisitionId == tblRequisition.RequistionId))
            {
                dbContext.TblRequisitionSkillMapping.Remove(tblRequisitionSkillMapping);
            }
            dbContext.SaveChanges();
            tblRequisition.CreatedByNavigation = null;
            foreach (TblRequisitionRecruiterMapping mapping in tblRequisition.TblRequisitionRecruiterMapping)
            {
                mapping.RequisitionId = tblRequisition.RequistionId;
                dbContext.TblRequisitionRecruiterMapping.Add(mapping);
            }
            dbContext.SaveChanges();
            foreach (TblRequisitionSkillMapping mapping in tblRequisition.TblRequisitionSkillMapping)
            {
                TblRequisitionSkillMapping tblRequisitionSkillMapping = new TblRequisitionSkillMapping() { RequisitionId = tblRequisition.RequistionId, SkillId = mapping.SkillId };
                dbContext.TblRequisitionSkillMapping.Add(tblRequisitionSkillMapping);
            }
            dbContext.SaveChanges();
            if (tblRequisition.Status == RequisitionStatus.Approved)
            {
                tblRequisition.CurrentOwner = String.Join(",", dbContext.TblEmployees.AsNoTracking().Where(r => r.Role.RoleName == RoleConstants.RecruiterLeadRole).Select(t => t.EmployeeName).ToArray());

            }
            if (tblRequisition.Status == RequisitionStatus.AssignedToRecruiter)
            {
                List<long> EmpIds = tblRequisition.TblRequisitionRecruiterMapping.Select(r => r.EmployeeId).ToList();
                string EmpName = "";
                foreach (long EmpID in EmpIds)
                {
                    EmpName = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == EmpID).FirstOrDefault().EmployeeName + ",";
                }

                tblRequisition.CurrentOwner = EmpName.Substring(0, EmpName.Length - 1);

            }
            if (tblRequisition.Status == RequisitionStatus.Cancelled)
            {
                tblRequisition.CurrentOwner = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault().EmployeeName;

            }
            dbContext.Entry(tblRequisition).State = EntityState.Modified;
            dbContext.TblRequisition.Update(tblRequisition);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblRequisition).State = EntityState.Detached;
            var obj = dbContext.TblRequisition.AsNoTracking().Where(r => r.RequistionId == tblRequisition.RequistionId).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMapping).FirstOrDefault();
            if (result > 0)
            {
                if (tblRequisition.Status == RequisitionStatus.Approved)
                {
                    SendRequisitionApprovedMail(obj);
                }
                else if (tblRequisition.Status == RequisitionStatus.AssignedToRecruiter)
                {
                    SendRequisitionAssignedMail(obj);
                }
                else if (tblRequisition.Status == RequisitionStatus.CancelRequested)
                {
                    //SendRequisitionCancelRequestMail(obj);
                    SendRequisitionCancelRequestMail(tblRequisition);
                }
                else if (tblRequisition.Status == RequisitionStatus.Cancelled)
                {
                    //SendRequisitionCancellationMail(obj);
                    SendRequestCancellationMail(tblRequisition);
                    RevertInterviewProcess(tblRequisition.RequistionId);
                }
            }
            return result;


        }

        public  int CloseRequisition(TblRequisition tblRequisition)
        {
            tblRequisition.TblRequisitionRecruiterMapping = null;
            tblRequisition.TblRequisitionSkillMapping = null;
            tblRequisition.ModifiedDate = DateTime.Now;
            tblRequisition.ModifiedBy = CurrentContext.EmployeeID;
            tblRequisition.CurrentOwner = "N/A";
            dbContext.Entry(tblRequisition).State = EntityState.Modified;
            dbContext.TblRequisition.Update(tblRequisition);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblRequisition).State = EntityState.Detached;
            return result;


        }

        public  TblRequisition GetRequisitionByCriteria(int key)
        {
            return dbContext.TblRequisition.Where(x => x.RequistionId == key).FirstOrDefault();
        }

        public  int MapRequisitionToApplicant(long RequisitionID, long ApplicantID)
        {
            dbContext.Add(new TblApplicantRequisition() { ApplicantId = ApplicantID, RequisitionId = RequisitionID, Status = InterviewStatus.NotAssigned, CreatedBy = CurrentContext.EmployeeID, CreatedDate = DateTime.Now });
            dbContext.SaveChanges();
            TblApplicants tblApplicants = dbContext.TblApplicants.AsNoTracking().Where(r => r.ApplicantId == ApplicantID).FirstOrDefault();
            tblApplicants.Status = ApplicantStatus.Blocked;
            tblApplicants.ShortlistedBy = RequisitionStatus.Internal;
            dbContext.Entry(tblApplicants).State = EntityState.Modified;
            dbContext.TblApplicants.Update(tblApplicants);
            dbContext.SaveChanges();
            dbContext.Entry(tblApplicants).State = EntityState.Detached;
            return 1;
        }

        public int MapRequisitionToApplicantClient(long RequisitionID, long ApplicantID)
        {
            dbContext.Add(new TblApplicantRequisitionClient() { ApplicantId = ApplicantID, RequisitionId = RequisitionID, Status = InterviewStatus.NotAssigned, CreatedBy = CurrentContext.EmployeeID, CreatedDate = DateTime.Now });
            dbContext.SaveChanges();
            return 1;
        }
        public  int UpdateRequisitionToApplicant(TblApplicantRequisition tblApplicantRequisition)
        {
            tblApplicantRequisition.ModifiedBy = CurrentContext.EmployeeID;
            tblApplicantRequisition.ModifiedDate = DateTime.Now;
            tblApplicantRequisition.Applicant = null;
            dbContext.Entry(tblApplicantRequisition).State = EntityState.Modified;
            dbContext.TblApplicantRequisition.Update(tblApplicantRequisition);
            dbContext.SaveChanges();
            dbContext.Entry(tblApplicantRequisition).State = EntityState.Detached;
            TblApplicants tblApplicants = dbContext.TblApplicants.AsNoTracking().Where(r => r.ApplicantId == tblApplicantRequisition.ApplicantId).FirstOrDefault();
            if (tblApplicantRequisition.Status == InterviewStatus.BlackListed)
            {
                tblApplicants.Status = ApplicantStatus.BlackListed;
            }
            else if (tblApplicantRequisition.Status == InterviewStatus.Selected)
            {
                tblApplicants.Status = ApplicantStatus.Selected;
            }
            else
            {
                tblApplicants.Status = ApplicantStatus.Available;
            }
            dbContext.Entry(tblApplicants).State = EntityState.Modified;
            dbContext.TblApplicants.Update(tblApplicants);
            dbContext.SaveChanges();
            dbContext.Entry(tblApplicants).State = EntityState.Detached;
            return 1;
        }

        private  void RevertInterviewProcess(long RequisitionID)
        {
            if (dbContext.TblApplicantRequisition.Where(r => r.RequisitionId == RequisitionID).Count() > 0)
            {
                var applicants = dbContext.TblApplicantRequisition.Where(r => r.RequisitionId == RequisitionID).ToList();

                foreach (TblApplicantRequisition tblApplicant in applicants)
                {
                    //Release Applicant
                    if (tblApplicant.Status != ApplicantStatus.BlackListed)
                    {
                        var applicant = dbContext.TblApplicants.Where(r => r.ApplicantId == tblApplicant.ApplicantId).FirstOrDefault();
                        dbContext.Entry(applicant).State = EntityState.Modified;
                        applicant.Status = ApplicantStatus.Available;
                        dbContext.TblApplicants.Update(applicant);
                        dbContext.SaveChanges();
                        dbContext.Entry(applicant).State = EntityState.Detached;
                    }

                    //Update Applicant Requisition ID
                    dbContext.Entry(tblApplicant).State = EntityState.Modified;
                    tblApplicant.Status = RequisitionStatus.Cancelled;
                    tblApplicant.ModifiedBy = CurrentContext.EmployeeID;
                    tblApplicant.ModifiedDate = DateTime.Now;
                    dbContext.TblApplicantRequisition.Update(tblApplicant);
                    dbContext.SaveChanges();
                    dbContext.Entry(tblApplicant).State = EntityState.Detached;


                }

                if (dbContext.TblInterviewManagement.Where(r => r.ApplicantRequisition.RequisitionId == RequisitionID).Count() > 0)
                {
                    //Cancell Interviews
                    var Interviews = dbContext.TblInterviewManagement.Where(r => r.ApplicantRequisition.RequisitionId == RequisitionID && r.Status == InterviewStatus.InterviewScheduled).ToList();
                    foreach (TblInterviewManagement interview in Interviews)
                    {

                        interview.Status = InterviewStatus.Cancelled;
                        interview.ModifiedDate = DateTime.Now;
                        interview.ModifiedBy = CurrentContext.EmployeeID;
                        dbContext.Entry(interview).State = EntityState.Modified;
                        dbContext.TblInterviewManagement.Update(interview);
                        dbContext.SaveChanges();
                        dbContext.Entry(interview).State = EntityState.Detached;

                        //Send Cancellation Emails to Interviewer
                        var Interviewers = dbContext.TblInterviewEmployeeMapping.Where(r => r.InterviewId == interview.InterviewId).ToList();
                        foreach (TblInterviewEmployeeMapping interviewer in Interviewers)
                        {
                            interviewer.IsActive = false;
                            dbContext.Entry(interviewer).State = EntityState.Modified;
                            dbContext.TblInterviewEmployeeMapping.Update(interviewer);
                            dbContext.SaveChanges();
                            dbContext.Entry(interviewer).State = EntityState.Detached;
                            //SendEmailUsing this table

                        }
                        ApplicantDAL.SendInterviewCancelledMailToPanel(interview, Interviewers);
                    }
                }
            }
        }

        public  List<TblApplicantRequisition> GetAllApplicantsForRequisition(long RequisitionID)
        {
            return dbContext.TblApplicantRequisition.AsNoTracking().Where(r => r.RequisitionId == RequisitionID).Include(r => r.Applicant).Include(r => r.Requisition).ToList();
        }


        #endregion


        #region Email

        private  async void SendRequisitionRaisedMail(TblRequisition tblRequisition)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblEmployees tblEmployees = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault();
            stringBuilder.AppendLine(tblEmployees.EmployeeName + "- has raised Requisition");
            stringBuilder.AppendLine("Requisition Title : " + tblRequisition.Title);
            stringBuilder.AppendLine("Skills : " + string.Join(",", dbContext.TblSkillMaster.Where(q => q.SkillId == tblRequisition.RequistionId).Select(x => x.SkillName).ToArray()));
            stringBuilder.AppendLine("Department : " + tblRequisition.Department.Department);
            stringBuilder.AppendLine("Designation : " + tblRequisition.Designation.Designation);
            stringBuilder.AppendLine("Years Of Experience : " + tblRequisition.YearsofExperience);
            stringBuilder.AppendLine("Joining Tenure(Months) : " + tblRequisition.JoiningTenure);
            stringBuilder.AppendLine("No Of Positions : " + tblRequisition.NoofPositions);
            stringBuilder.AppendLine("Location : " + tblRequisition.Location);
            stringBuilder.AppendLine("Comments : " + tblRequisition.Comments);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Requisition Raised";
            string ToMailID = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.ManagerEmployeeId).FirstOrDefault().EmailId;
            emailTemplate.EmailFromDisplayName = tblEmployees.EmployeeName;
            emailTemplate.ToEmailAddress = new string[] { ToMailID };
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }

        private  async void SendRequisitionApprovedMail(TblRequisition tblRequisition)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblEmployees tblEmployees = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault();
            stringBuilder.AppendLine(tblEmployees.EmployeeName + "- has raised Requisition");
            stringBuilder.AppendLine("Requisition Title : " + tblRequisition.Title);
            stringBuilder.AppendLine("Skills : " + string.Join(",", dbContext.TblSkillMaster.Where(q => q.SkillId == tblRequisition.RequistionId).Select(x => x.SkillName).ToArray()));
            stringBuilder.AppendLine("Department : " + tblRequisition.Department.Department);
            stringBuilder.AppendLine("Designation : " + tblRequisition.Designation.Designation);
            stringBuilder.AppendLine("Years Of Experience : " + tblRequisition.YearsofExperience);
            stringBuilder.AppendLine("Joining Tenure(Months) : " + tblRequisition.JoiningTenure);
            stringBuilder.AppendLine("No Of Positions : " + tblRequisition.NoofPositions);
            stringBuilder.AppendLine("Location : " + tblRequisition.Location);
            stringBuilder.AppendLine("Comments : " + tblRequisition.Comments);
            stringBuilder.AppendLine("Manager Comments : " + tblRequisition.ManagerComments);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Requisition Approved";
            emailTemplate.EmailFromDisplayName = tblEmployees.EmployeeName;
            emailTemplate.ToEmailAddress = dbContext.TblEmployees.AsNoTracking().Where(r => r.Role.RoleName == RoleConstants.RecruiterLeadRole && r.EmployeeId == tblRequisition.CreatedBy).AsNoTracking().ToList().Select(r => r.EmailId).ToArray();
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);

        }

        private  async void SendRequisitionAssignedMail(TblRequisition tblRequisition)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblEmployees tblEmployees = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault();
            stringBuilder.AppendLine(tblEmployees.EmployeeName + "- has raised Requisition");
            stringBuilder.AppendLine("Requisition Title : " + tblRequisition.Title);
            stringBuilder.AppendLine("Skills : " + string.Join(",", dbContext.TblSkillMaster.Where(q => q.SkillId == tblRequisition.RequistionId).Select(x => x.SkillName).ToArray()));
            stringBuilder.AppendLine("Department : " + tblRequisition.Department.Department);
            stringBuilder.AppendLine("Designation : " + tblRequisition.Designation.Designation);
            stringBuilder.AppendLine("Years Of Experience : " + tblRequisition.YearsofExperience);
            stringBuilder.AppendLine("Joining Tenure(Months) : " + tblRequisition.JoiningTenure);
            stringBuilder.AppendLine("No Of Positions : " + tblRequisition.NoofPositions);
            stringBuilder.AppendLine("Location : " + tblRequisition.Location);
            stringBuilder.AppendLine("Comments : " + tblRequisition.Comments);
            stringBuilder.AppendLine("Manager Comments : " + tblRequisition.ManagerComments);
            stringBuilder.AppendLine("Recruiter Comments : " + tblRequisition.RecruiterComment);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Requisition has been Assigned to you";
            emailTemplate.EmailFromDisplayName = tblEmployees.EmployeeName;
            string[] MailIds = new string[tblRequisition.TblRequisitionRecruiterMapping.Count + 1];
            for (int i = 0; tblRequisition.TblRequisitionRecruiterMapping.Count > i; i++)
            {
                TblRequisitionRecruiterMapping recruiter = tblRequisition.TblRequisitionRecruiterMapping.ElementAt(i);
                MailIds[i] = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == recruiter.EmployeeId).FirstOrDefault().EmailId;
            }
            MailIds[MailIds.Length - 1] = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault().EmailId;
            emailTemplate.ToEmailAddress = MailIds;
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);

        }

        private  async void SendRequisitionCancelRequestMail(TblRequisition tblRequisition)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblEmployees tblEmployees = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault();
            stringBuilder.AppendLine(tblEmployees.EmployeeName + "- has requested for cancellation of requisition.");
            stringBuilder.AppendLine("Requisition Title : " + tblRequisition.Title);
            stringBuilder.AppendLine("Skills : " + string.Join(",", dbContext.TblSkillMaster.Where(q => q.SkillId == tblRequisition.RequistionId).Select(x => x.SkillName).ToArray()));
            stringBuilder.AppendLine("Department : " + tblRequisition.Department.Department);
            stringBuilder.AppendLine("Designation : " + tblRequisition.Designation.Designation);
            stringBuilder.AppendLine("Years Of Experience : " + tblRequisition.YearsofExperience);
            stringBuilder.AppendLine("Joining Tenure(Months) : " + tblRequisition.JoiningTenure);
            stringBuilder.AppendLine("No Of Positions : " + tblRequisition.NoofPositions);
            stringBuilder.AppendLine("Location : " + tblRequisition.Location);
            stringBuilder.AppendLine("Comments : " + tblRequisition.Comments);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Requisition Cancellation";
            string ToMailID = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.ManagerEmployeeId).FirstOrDefault().EmailId;
            emailTemplate.EmailFromDisplayName = tblEmployees.EmployeeName;
            emailTemplate.ToEmailAddress = new string[] { ToMailID };
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }

        private  async void SendManagerApprovedCancellationMail(TblRequisition tblRequisition)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblEmployees tblEmployees = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault();
            stringBuilder.AppendLine(tblEmployees.EmployeeName + "- has requested for cancellation of requisition and approved by manager.");
            stringBuilder.AppendLine("Requisition Title : " + tblRequisition.Title);
            stringBuilder.AppendLine("Skills : " + string.Join(",", dbContext.TblSkillMaster.Where(q => q.SkillId == tblRequisition.RequistionId).Select(x => x.SkillName).ToArray()));
            stringBuilder.AppendLine("Department : " + tblRequisition.Department.Department);
            stringBuilder.AppendLine("Designation : " + tblRequisition.Designation.Designation);
            stringBuilder.AppendLine("Years Of Experience : " + tblRequisition.YearsofExperience);
            stringBuilder.AppendLine("Joining Tenure(Months) : " + tblRequisition.JoiningTenure);
            stringBuilder.AppendLine("No Of Positions : " + tblRequisition.NoofPositions);
            stringBuilder.AppendLine("Location : " + tblRequisition.Location);
            stringBuilder.AppendLine("Comments : " + tblRequisition.Comments);
            stringBuilder.AppendLine("Manager Comments : " + tblRequisition.ManagerComments);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Requisition cancellation approved by Manager";
            emailTemplate.EmailFromDisplayName = tblEmployees.EmployeeName;
            emailTemplate.ToEmailAddress = dbContext.TblEmployees.AsNoTracking().Where(r => r.Role.RoleName == RoleConstants.RecruiterLeadRole && r.EmployeeId == tblRequisition.CreatedBy).AsNoTracking().ToList().Select(r => r.EmailId).ToArray();
            RevertInterviewProcess(tblRequisition.RequistionId);
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }

        private  async void SendInterviewCancellationMailToPanel(string EmailID, TblInterviewManagement tblInterviewManagement)
        {
            DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
            var interview = dbContext.TblInterviewManagement.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).AsNoTracking().Include(r => r.Applicant).Include(r => r.ApplicantRequisition.Requisition.Designation).FirstOrDefault();
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblInterviewEmployeeMapping tblInterviewEmployees = dbContext.TblInterviewEmployeeMapping.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
            stringBuilder.AppendLine("Hi " + tblInterviewEmployees.Employee.EmployeeName + ",");
            stringBuilder.AppendLine("Interview has been cancelled for the date '" + tblInterviewManagement.InterviewDate + "'.");
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Interview has been Cancelled";
            emailTemplate.InvitationStartTime = dt;
            emailTemplate.InvitationEndTime = dt.AddHours(1);
            emailTemplate.Location = tblInterviewManagement.Venue;
            emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
            emailTemplate.ToEmailAddress = new string[] { EmailID };
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }


        private  async void SendRequestCancellationMail(TblRequisition tblRequisition)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblEmployees tblEmployees = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault();
            stringBuilder.AppendLine(tblEmployees.EmployeeName + "- has requested for cancellation of requisition.");
            stringBuilder.AppendLine("Requisition Title : " + tblRequisition.Title);
            stringBuilder.AppendLine("Skills : " + string.Join(",", dbContext.TblSkillMaster.Where(q => q.SkillId == tblRequisition.RequistionId).Select(x => x.SkillName).ToArray()));
            stringBuilder.AppendLine("Department : " + tblRequisition.Department.Department);
            stringBuilder.AppendLine("Designation : " + tblRequisition.Designation.Designation);
            stringBuilder.AppendLine("Years Of Experience : " + tblRequisition.YearsofExperience);
            stringBuilder.AppendLine("Joining Tenure(Months) : " + tblRequisition.JoiningTenure);
            stringBuilder.AppendLine("No Of Positions : " + tblRequisition.NoofPositions);
            stringBuilder.AppendLine("Location : " + tblRequisition.Location);
            stringBuilder.AppendLine("Comments : " + tblRequisition.Comments);
            stringBuilder.AppendLine("Manager Comments : " + tblRequisition.ManagerComments);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Requisition cancellation Request";
            emailTemplate.EmailFromDisplayName = tblEmployees.EmployeeName;
            emailTemplate.ToEmailAddress = dbContext.TblEmployees.AsNoTracking().Where(r => r.Role.RoleName == RoleConstants.RecruiterLeadRole && r.EmployeeId == tblRequisition.CreatedBy).AsNoTracking().ToList().Select(r => r.EmailId).ToArray();
            RevertInterviewProcess(tblRequisition.RequistionId);
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }



        //private  async void SendInterviewCancelledMailToPanel(TblInterviewManagement tblInterviewManagement)
        //{
        //    DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
        //    EmailTemplate emailTemplate = new EmailTemplate();
        //    StringBuilder stringBuilder = new StringBuilder();
        //    TblInterviewEmployeeMapping tblInterviewEmployees = dbContext.TblInterviewEmployeeMapping.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
        //    stringBuilder.AppendLine("Hi " + tblInterviewEmployees.Employee.EmployeeName + ",");
        //    stringBuilder.AppendLine("Interview has been Cancelled for you on '" + tblInterviewManagement.InterviewDate + "'.");
        //    emailTemplate.Body = stringBuilder.ToString();
        //    emailTemplate.Subject = "Interview has been Cancelled";
        //    emailTemplate.InvitationStartTime = dt;
        //    emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
        //    emailTemplate.ToEmailAddress = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblInterviewManagement.CreatedBy).AsNoTracking().ToList().Select(r => r.EmailId).ToArray();
        //    await EmailHelper.SendCancelCalendarInvite(emailTemplate);
        //}

        #endregion



    }
}
