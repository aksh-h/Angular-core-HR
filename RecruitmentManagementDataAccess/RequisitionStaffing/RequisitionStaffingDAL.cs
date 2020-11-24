using RecruitmentManagementDataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using RecruitmentManagementProvider.Helpers;
using System.Text;
using System.Reflection.Metadata;
using RecruitmentManagementDataAccess.Helpers;
using RecruitmentManagementDataAccess.ApplicantStaffing;

namespace RecruitmentManagementDataAccess.Masters
{
        public class RequisitionStaffingDAL
        {
            ApplicantStaffingDAL ApplicantStaffingDAL = new ApplicantStaffingDAL();

            private readonly  RecruitmentManagementContext dbContext;
            public RequisitionStaffingDAL()
            {
                dbContext = new RecruitmentManagementContext();
            }

            #region RequisitionStaffing

            public  int CreateRequisitionStaffing(TblRequisitionStaffing tblRequisitionstaffing)
            {
                tblRequisitionstaffing.IsActive = true;
                tblRequisitionstaffing.CreatedDate = DateTime.Now;
                tblRequisitionstaffing.CreatedBy = CurrentContext.EmployeeID;
                tblRequisitionstaffing.Status = RequisitionStatus.Open;
                tblRequisitionstaffing.ManagerEmployeeId = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == CurrentContext.EmployeeID).FirstOrDefault().ReportingManagerId;
                tblRequisitionstaffing.CurrentOwner = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisitionstaffing.ManagerEmployeeId).FirstOrDefault().EmployeeName;
                dbContext.Entry(tblRequisitionstaffing).State = EntityState.Added;
                dbContext.TblRequisitionStaffing.Add(tblRequisitionstaffing);
                int result = dbContext.SaveChanges();
                dbContext.Entry(tblRequisitionstaffing).State = EntityState.Detached;
                foreach (TblRequisitionSkillMappingStaffing mapping in tblRequisitionstaffing.TblRequisitionSkillMappingStaffing)
                {
                    mapping.RequisitionId = tblRequisitionstaffing.RequistionId;
                    dbContext.TblRequisitionSkillMappingStaffing.Add(mapping);
                }
                dbContext.SaveChanges();
                foreach (TblRequisitionClientContactMappingStaffing mapping in tblRequisitionstaffing.TblRequisitionClientContactMappingStaffing)
                {
                    mapping.RequisitionId = tblRequisitionstaffing.RequistionId;
                    dbContext.TblRequisitionClientContactMappingStaffing.Add(mapping);
                }
                dbContext.SaveChanges();

                var obj = dbContext.TblRequisitionStaffing.AsNoTracking().Where(r => r.RequistionId == tblRequisitionstaffing.RequistionId).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMappingStaffing).FirstOrDefault();
                if (result > 0)
                {
                    SendRequisitionRaisedMail(obj);
                }
                dbContext.Entry(obj).State = EntityState.Detached;
                return result;
            }

            public  int DeleteRequisitionStaffing(int key)
            {
                TblRequisitionStaffing tblRequisition = dbContext.TblRequisitionStaffing.Where(e => e.RequistionId == key).FirstOrDefault();
                tblRequisition.IsActive = false;
                dbContext.Entry(tblRequisition).State = EntityState.Modified;
                dbContext.TblRequisitionStaffing.Update(tblRequisition);
                dbContext.SaveChanges();
                dbContext.Entry(tblRequisition).State = EntityState.Detached;
                dbContext.TblRequisitionSkillMappingStaffing.RemoveRange(dbContext.TblRequisitionSkillMappingStaffing.Where(r => r.RequisitionId == tblRequisition.RequistionId));
                dbContext.SaveChanges();
                return 1;
            }

            public  List<TblRequisitionStaffing> GetAllRequisitionStaffing(bool IsCancelled = true)
            {
                List<TblRequisitionStaffing> RaisedRequisition = new List<TblRequisitionStaffing>();
                if (IsCancelled)
                    RaisedRequisition = dbContext.TblRequisitionStaffing.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == CurrentContext.EmployeeID).Include(r => r.Department).Include(r => r.Designation).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                else
                    RaisedRequisition = dbContext.TblRequisitionStaffing.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == CurrentContext.EmployeeID && r.Status == RequisitionStatus.Approved && r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).Include(r => r.Department).Include(r => r.Designation).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                if (CurrentContext.RoleName == RoleConstants.SalesHead)
                {
                    long[] EmployeeIDs = dbContext.TblEmployees.AsNoTracking().Where(r => r.ReportingManagerId == CurrentContext.EmployeeID).Select(e => e.EmployeeId).ToArray();
                    for (int i = 0; EmployeeIDs.Length > i; i++)
                    {
                        var Requisition = new List<TblRequisitionStaffing>();
                        if (IsCancelled)
                            Requisition = dbContext.TblRequisitionStaffing.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == EmployeeIDs[i]).Include(r => r.CreatedByNavigation.Role).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                        else
                            Requisition = dbContext.TblRequisitionStaffing.AsNoTracking().Where(r => r.IsActive == true && r.CreatedBy == EmployeeIDs[i] && r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).Include(r => r.CreatedByNavigation.Role).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
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

            public  List<TblRequisitionStaffing> GetAllRequisitionForRecuitersStaffing(bool IsCancelled = true)
            {
                List<TblRequisitionStaffing> RaisedRequisition = new List<TblRequisitionStaffing>();
                if (CurrentContext.RoleName == RoleConstants.StaffingLeadRole)//RecruiterLeadRole
                {
                    if (IsCancelled)
                    {
                        RaisedRequisition = dbContext.TblRequisitionStaffing.AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Open && r.Status != RequisitionStatus.Declined).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                        var ClientRequisition = dbContext.TblRequisitionStaffing.AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.ClientId != 0 && r.ClientId != null && r.CreatedBy == CurrentContext.EmployeeID).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                        foreach (TblRequisitionStaffing requisition in ClientRequisition)
                        {
                            if (RaisedRequisition.Where(r => r.RequistionId == requisition.RequistionId).Count() < 1)
                                RaisedRequisition.Add(requisition);
                        }
                    }
                    else
                    {
                        RaisedRequisition = dbContext.TblRequisitionStaffing.Where(r => r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Open && r.Status != RequisitionStatus.Declined).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                        var ClientRequisition = dbContext.TblRequisitionStaffing.Where(r => r.ClientId != 0 && r.ClientId != null && r.CreatedBy == CurrentContext.EmployeeID).AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Open && r.Status != RequisitionStatus.Declined).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                        foreach (TblRequisitionStaffing requisition in ClientRequisition)
                        {
                            if (RaisedRequisition.Where(r => r.RequistionId == requisition.RequistionId).Count() < 1)
                                RaisedRequisition.Add(requisition);
                        }
                    }
                }
                else
                {
                    if (IsCancelled)
                        RaisedRequisition = dbContext.TblRequisitionStaffing.AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Declined && r.TblRequisitionRecruiterMappingStaffing.Any(t => t.EmployeeId == CurrentContext.EmployeeID)).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();
                    else
                        RaisedRequisition = dbContext.TblRequisitionStaffing.Where(r => r.Status != RequisitionStatus.Cancelled && r.Status != RequisitionStatus.Closed).AsNoTracking().Include(r => r.Department).Include(r => r.Designation).Where(r => r.IsActive == true && r.Status != RequisitionStatus.Declined && r.TblRequisitionRecruiterMappingStaffing.Any(t => t.EmployeeId == CurrentContext.EmployeeID)).Include(r => r.CreatedByNavigation.Role).AsNoTracking().Include(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(u => u.Skill).AsNoTracking().Include(r => r.TblRequisitionClientContactMappingStaffing).AsNoTracking().ToList();

                }
                return RaisedRequisition.OrderBy(r => r.RequistionId).ToList();
            }

            public  int UpdateRequisitionStaffing(TblRequisitionStaffing tblRequisition)
            {
                tblRequisition.ModifiedDate = DateTime.Now;
                tblRequisition.IsActive = true;
                tblRequisition.ModifiedBy = CurrentContext.EmployeeID;
                tblRequisition.ManagerEmployeeId = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == CurrentContext.EmployeeID).AsNoTracking().FirstOrDefault().ReportingManagerId;
                foreach (TblRequisitionSkillMappingStaffing TblRequisitionSkillMappingStaffing in dbContext.TblRequisitionSkillMappingStaffing.Where(r => r.RequisitionId == tblRequisition.RequistionId))
                {
                    dbContext.TblRequisitionSkillMappingStaffing.Remove(TblRequisitionSkillMappingStaffing);
                }
                dbContext.SaveChanges();

                foreach (TblRequisitionClientContactMappingStaffing tblRequisitionClientContactMappingStaffing in dbContext.TblRequisitionClientContactMappingStaffing.Where(r => r.RequisitionId == tblRequisition.RequistionId))
                {
                    dbContext.TblRequisitionClientContactMappingStaffing.Remove(tblRequisitionClientContactMappingStaffing);
                }
                dbContext.SaveChanges();

                tblRequisition.CreatedByNavigation = null;
                foreach (TblRequisitionRecruiterMappingStaffing mapping in tblRequisition.TblRequisitionRecruiterMappingStaffing)
                {
                    mapping.RequisitionId = tblRequisition.RequistionId;
                    dbContext.TblRequisitionRecruiterMappingStaffing.Add(mapping);
                }
                dbContext.SaveChanges();
                foreach (TblRequisitionSkillMappingStaffing mapping in tblRequisition.TblRequisitionSkillMappingStaffing)
                {
                    TblRequisitionSkillMappingStaffing tblRequisitionSkillMapping = new TblRequisitionSkillMappingStaffing() { RequisitionId = tblRequisition.RequistionId, SkillId = mapping.SkillId };
                    dbContext.TblRequisitionSkillMappingStaffing.Add(tblRequisitionSkillMapping);
                }
                dbContext.SaveChanges();
                foreach (TblRequisitionClientContactMappingStaffing mapping in tblRequisition.TblRequisitionClientContactMappingStaffing)
                {
                    TblRequisitionClientContactMappingStaffing tblRequisitionClientContactMapping = new TblRequisitionClientContactMappingStaffing() { RequisitionId = tblRequisition.RequistionId, ContactId = mapping.ContactId };
                    dbContext.TblRequisitionClientContactMappingStaffing.Add(tblRequisitionClientContactMapping);
                }
                dbContext.SaveChanges();

                if (tblRequisition.Status == RequisitionStatus.Approved)
                {
                    tblRequisition.CurrentOwner = String.Join(",", dbContext.TblEmployees.AsNoTracking().Where(r => r.Role.RoleName == RoleConstants.StaffingLeadRole).Select(t => t.EmployeeName).ToArray());

                }
                if (tblRequisition.Status == RequisitionStatus.AssignedToStaffing)
                {
                    List<long> EmpIds = tblRequisition.TblRequisitionRecruiterMappingStaffing.Select(r => r.EmployeeId).ToList();
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
                dbContext.TblRequisitionStaffing.Update(tblRequisition);
                int result = dbContext.SaveChanges();
                dbContext.Entry(tblRequisition).State = EntityState.Detached;
                var obj = dbContext.TblRequisitionStaffing.AsNoTracking().Where(r => r.RequistionId == tblRequisition.RequistionId).Include(r => r.Department).Include(r => r.Designation).Include(r => r.TblRequisitionSkillMappingStaffing).FirstOrDefault();
                if (result > 0)
                {
                    if (tblRequisition.Status == RequisitionStatus.Approved)
                    {
                        SendRequisitionApprovedMail(obj);
                    }
                    else if (tblRequisition.Status == RequisitionStatus.AssignedToStaffing)
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

            public  int CloseRequisitionStaffing(TblRequisitionStaffing tblRequisition)
            {
                tblRequisition.TblRequisitionRecruiterMappingStaffing = null;
                tblRequisition.TblRequisitionSkillMappingStaffing = null;
                tblRequisition.ModifiedDate = DateTime.Now;
                tblRequisition.ModifiedBy = CurrentContext.EmployeeID;
                tblRequisition.CurrentOwner = "N/A";
                dbContext.Entry(tblRequisition).State = EntityState.Modified;
                dbContext.TblRequisitionStaffing.Update(tblRequisition);
                int result = dbContext.SaveChanges();
                dbContext.Entry(tblRequisition).State = EntityState.Detached;
                return result;


            }

            public  TblRequisitionStaffing GetRequisitionByCriteriaStaffing(int key)
            {
                return dbContext.TblRequisitionStaffing.Where(x => x.RequistionId == key).FirstOrDefault();
            }

            public  int MapRequisitionStaffingToApplicant(long RequisitionID, long ApplicantID)
            {
                dbContext.Add(new TblApplicantRequisitionStaffing() { ApplicantId = ApplicantID, RequisitionId = RequisitionID, Status = InterviewStatus.NotAssigned, CreatedBy = CurrentContext.EmployeeID, CreatedDate = DateTime.Now });
                dbContext.SaveChanges();
                TblApplicants tblApplicants = dbContext.TblApplicants.AsNoTracking().Where(r => r.ApplicantId == ApplicantID).FirstOrDefault();
                tblApplicants.Status = ApplicantStatus.Blocked;
                tblApplicants.ShortlistedBy = RequisitionStatus.Client;
                dbContext.Entry(tblApplicants).State = EntityState.Modified;
                dbContext.TblApplicants.Update(tblApplicants);
                dbContext.SaveChanges();
                dbContext.Entry(tblApplicants).State = EntityState.Detached;
                return 1;
            }

            //public  int MapRequisitionStaffingToApplicantClient(long RequisitionID, long ApplicantID)
            //{
            //    dbContext.Add(new TblApplicantRequisitionClient() { ApplicantId = ApplicantID, RequisitionId = RequisitionID, Status = InterviewStatus.NotAssigned, CreatedBy = CurrentContext.EmployeeID, CreatedDate = DateTime.Now });
            //    dbContext.SaveChanges();
            //    return 1;
            //}

            public  int MapRequisitionStaffingToApplicantClient(long RequisitionID, long ApplicantID)
            {
                if (dbContext.TblApplicantRequisitionStaffing.Where(a => a.RequisitionId == RequisitionID && a.ApplicantId == ApplicantID).Count() < 1)
                {
                    dbContext.Add(new TblApplicantRequisitionStaffing() { ApplicantId = ApplicantID, RequisitionId = RequisitionID, Status = InterviewStatus.NotAssigned, CreatedBy = CurrentContext.EmployeeID, CreatedDate = DateTime.Now });
                    dbContext.SaveChanges();
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            public  int UpdateRequisitionToApplicantStaffing(TblApplicantRequisitionStaffing tblApplicantRequisition)
            {
                tblApplicantRequisition.ModifiedBy = CurrentContext.EmployeeID;
                tblApplicantRequisition.ModifiedDate = DateTime.Now;
                tblApplicantRequisition.Applicant = null;
                dbContext.Entry(tblApplicantRequisition).State = EntityState.Modified;
                dbContext.TblApplicantRequisitionStaffing.Update(tblApplicantRequisition);
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
                if (dbContext.TblApplicantRequisitionStaffing.Where(r => r.RequisitionId == RequisitionID).Count() > 0)
                {
                    var applicants = dbContext.TblApplicantRequisitionStaffing.Where(r => r.RequisitionId == RequisitionID).ToList();

                    foreach (TblApplicantRequisitionStaffing tblApplicant in applicants)
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
                        dbContext.TblApplicantRequisitionStaffing.Update(tblApplicant);
                        dbContext.SaveChanges();
                        dbContext.Entry(tblApplicant).State = EntityState.Detached;


                    }

                    if (dbContext.TblInterviewManagementStaffing.Where(r => r.ApplicantRequisition.RequisitionId == RequisitionID).Count() > 0)
                    {
                        //Cancell Interviews
                        var Interviews = dbContext.TblInterviewManagementStaffing.Where(r => r.ApplicantRequisition.RequisitionId == RequisitionID && r.Status == InterviewStatus.InterviewScheduled).ToList();
                        foreach (TblInterviewManagementStaffing interview in Interviews)
                        {

                            interview.Status = InterviewStatus.Cancelled;
                            interview.ModifiedDate = DateTime.Now;
                            interview.ModifiedBy = CurrentContext.EmployeeID;
                            dbContext.Entry(interview).State = EntityState.Modified;
                            dbContext.TblInterviewManagementStaffing.Update(interview);
                            dbContext.SaveChanges();
                            dbContext.Entry(interview).State = EntityState.Detached;

                            //Send Cancellation Emails to Interviewer
                            var Interviewers = dbContext.TblInterviewEmployeeMappingStaffing.Where(r => r.InterviewId == interview.InterviewId).ToList();
                            foreach (TblInterviewEmployeeMappingStaffing interviewer in Interviewers)
                            {
                                interviewer.IsActive = false;
                                dbContext.Entry(interviewer).State = EntityState.Modified;
                                dbContext.TblInterviewEmployeeMappingStaffing.Update(interviewer);
                                dbContext.SaveChanges();
                                dbContext.Entry(interviewer).State = EntityState.Detached;
                                //SendEmailUsing this table

                            }
                            ApplicantStaffingDAL.SendInterviewCancelledMailToPanel(interview, Interviewers);
                        }
                    }
                }
            }

            public  List<TblApplicantRequisitionStaffing> GetAllApplicantsForRequisitionStaffing(long RequisitionID)
            {
                return dbContext.TblApplicantRequisitionStaffing.AsNoTracking().Where(r => r.RequisitionId == RequisitionID).Include(r => r.Applicant).Include(r => r.Requisition).ToList();
            }


            public  List<TblClientsContactDetails> GetClientsContactByCriteria(int id)
            {
                return dbContext.TblClientsContactDetails.Where(x => x.IsActive == true && x.ClientId == id).ToList();

            }

           
            #endregion


            #region Email

            private  async void SendRequisitionRaisedMail(TblRequisitionStaffing tblRequisition)
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

            private  async void SendRequisitionApprovedMail(TblRequisitionStaffing tblRequisition)
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

            private  async void SendRequisitionAssignedMail(TblRequisitionStaffing tblRequisition)
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
                string[] MailIds = new string[tblRequisition.TblRequisitionRecruiterMappingStaffing.Count + 1];
                for (int i = 0; tblRequisition.TblRequisitionRecruiterMappingStaffing.Count > i; i++)
                {
                    TblRequisitionRecruiterMappingStaffing recruiter = tblRequisition.TblRequisitionRecruiterMappingStaffing.ElementAt(i);
                    MailIds[i] = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == recruiter.EmployeeId).FirstOrDefault().EmailId;
                }
                MailIds[MailIds.Length - 1] = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblRequisition.CreatedBy).FirstOrDefault().EmailId;
                emailTemplate.ToEmailAddress = MailIds;
                await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);

            }

            private  async void SendRequisitionCancelRequestMail(TblRequisitionStaffing tblRequisition)
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

            private  async void SendManagerApprovedCancellationMail(TblRequisitionStaffing tblRequisition)
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

            private  async void SendInterviewCancellationMailToPanel(string EmailID, TblInterviewManagementStaffing tblInterviewManagement)
            {
                DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
                var interview = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).AsNoTracking().Include(r => r.Applicant).Include(r => r.ApplicantRequisition.Requisition.Designation).FirstOrDefault();
                EmailTemplate emailTemplate = new EmailTemplate();
                StringBuilder stringBuilder = new StringBuilder();
                TblInterviewEmployeeMappingStaffing tblInterviewEmployees = dbContext.TblInterviewEmployeeMappingStaffing.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
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


            private  async void SendRequestCancellationMail(TblRequisitionStaffing tblRequisition)
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

