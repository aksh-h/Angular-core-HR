using RecruitmentManagementDataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using RecruitmentManagementProvider.Helpers;
using System.Text;
using System.Reflection.Metadata;
using RecruitmentManagementDataAccess.Helpers;
using System.Threading.Tasks;
namespace RecruitmentManagementDataAccess.Masters
{
    public class ApplicantDAL
    {
        private readonly RecruitmentManagementContext dbContext;
        public ApplicantDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }
        #region Applicant
        #region Applicants
        public int CreateApplicants(TblApplicants tblApplicants)
        {
            if (dbContext.TblApplicants.Where(r => r.PanNo.ToUpper() == tblApplicants.PanNo.ToUpper() && r.EmailAddress.ToUpper() == tblApplicants.EmailAddress.ToUpper() && r.Dob == tblApplicants.Dob).Count() > 0)
                return -1;
            dbContext.Entry(tblApplicants).State = EntityState.Added;
            dbContext.TblApplicants.Add(tblApplicants);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblApplicants).State = EntityState.Detached;
            return result;
        }
        public int DeleteApplicants(int key)
        {
            TblApplicants applicants = dbContext.TblApplicants.Where(x => x.ApplicantId == key).FirstOrDefault();
            dbContext.TblApplicants.Remove(applicants);
            return dbContext.SaveChanges();
        }
        public int UpdateApplicants(TblApplicants tblApplicants)
        {
            if (dbContext.TblApplicants.Where(r => r.PanNo.ToUpper() == tblApplicants.PanNo.ToUpper() && r.EmailAddress.ToUpper() == tblApplicants.EmailAddress.ToUpper() && r.Dob==tblApplicants.Dob).Count() > 0)
                return -1;
            dbContext.Entry(tblApplicants).State = EntityState.Modified;
            dbContext.TblApplicants.Update(tblApplicants);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblApplicants).State = EntityState.Detached;
            return result;
        }
        public TblApplicants GetApplicantsByCriteria(int key)
        {
            return dbContext.TblApplicants.Where(x => x.ApplicantId == key).FirstOrDefault();
        }
        #endregion
        public List<TblApplicants> GetAllApplicant()
        {
            return dbContext.TblApplicants.AsNoTracking().ToList();
        }

        public List<TblRequisitionSkillMapping> GetAllSkills(int RequisitionId)
        {
            return dbContext.TblRequisitionSkillMapping.AsNoTracking().Where(x => x.RequisitionId == RequisitionId).ToList();
        }

        public List<TblApplicantRequisition> GetAllShortListedApplicant()
        {
            return dbContext.TblApplicantRequisition.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Applicant.Status == ApplicantStatus.Blocked && r.Applicant.Status != ApplicantStatus.Selected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }
        public List<TblApplicantRequisitionClient> GetAllShortListedApplicantClient()
        {
            return dbContext.TblApplicantRequisitionClient.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Applicant.Status == ApplicantStatus.Blocked && r.Applicant.Status != ApplicantStatus.Selected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();

        }
        public List<TblInterviewManagement> GetAllApplicantInterviewDetails(int key)
        {
            var result = dbContext.TblInterviewManagement.AsNoTracking().Where(r => r.ApplicantRequisitionId == key).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.TblInterviewEmployeeMapping).ThenInclude(r => r.Employee).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMapping).ThenInclude(r => r.Skill).ToList();
            return result;
        }
        public List<TblApplicantRequisition> GetAllSelectedApplicant()
        {
            return dbContext.TblApplicantRequisition.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Status == InterviewStatus.Selected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }
        public List<TblApplicantRequisition> GetAllRejectedApplicant()
        {
            return dbContext.TblApplicantRequisition.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Status == InterviewStatus.Rejected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }
        public List<TblApplicantRequisition> GetAllBlackListedApplicant()
        {
            return dbContext.TblApplicantRequisition.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Status == InterviewStatus.BlackListed).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }
        private int SaveApplicants(TblApplicants tblApplicants)
        {
            dbContext.TblApplicants.Add(tblApplicants);
            return dbContext.SaveChanges();

        }
        public async Task<TblSelectedApplicants> GetInternalApplicantJoiningDetails(int id)
        {
            var result = dbContext.TblSelectedApplicants.AsNoTracking().Where(r => r.ApplicantRequisitionId == id).AsNoTracking().FirstOrDefault();
            if (result == null)
            {
                result = new TblSelectedApplicants();
            }
            return result;
        }
        public int CreateInternalApplicantJoining(TblSelectedApplicants tblSelectedApplicants)
        {
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Added;
            dbContext.TblSelectedApplicants.Add(tblSelectedApplicants);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Detached;
            return result;
        }
        public long UpdateInternalApplicantJoining(TblSelectedApplicants tblSelectedApplicants)
        {
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Added;
            dbContext.TblSelectedApplicants.Add(tblSelectedApplicants);
            long result = dbContext.SaveChanges();
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Detached;
            result = tblSelectedApplicants.SelectedApplicantsId;
            return result;
        }
        #endregion
        #region Interview
        public TblApplicantRequisition GetRequisitionApplicantDetails(long key)
        {
            return dbContext.TblApplicantRequisition.Where(r => r.ApplicantRequisitionId == key).Include(r => r.Requisition).Include(r => r.Applicant).FirstOrDefault();
        }
        public List<TblEmployeePanelMapping> GetAllInterviewer()
        {
            return dbContext.TblEmployeePanelMapping.AsNoTracking().Where(r => r.IsActive == true).Include(r => r.Employee).ToList();
        }
        public int SaveInterviewDetails(TblInterviewManagement tblInterviewManagement)
        {
            dbContext.Entry(tblInterviewManagement).State = EntityState.Added;
            tblInterviewManagement.CreatedBy = CurrentContext.EmployeeID;
            tblInterviewManagement.CreatedDate = DateTime.Now;
            tblInterviewManagement.IsCompleted = false;
            tblInterviewManagement.EmailGuid = new Guid();
            string Round = tblInterviewManagement.Status;
            tblInterviewManagement.Status = InterviewStatus.InterviewScheduled;
            dbContext.TblInterviewManagement.Add(tblInterviewManagement);
            dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;
            for (int i = 0; tblInterviewManagement.TblInterviewEmployeeMapping.Count > i; i++)
            {
                tblInterviewManagement.TblInterviewEmployeeMapping.ElementAt(i).InterviewId = tblInterviewManagement.InterviewId;
                tblInterviewManagement.TblInterviewEmployeeMapping.ElementAt(i).IsActive = true;
                dbContext.TblInterviewEmployeeMapping.Add(tblInterviewManagement.TblInterviewEmployeeMapping.ElementAt(i));
            }
            dbContext.SaveChanges();
            //Requisition
            var requisition = dbContext.TblApplicantRequisition.Where(r => r.ApplicantRequisitionId == tblInterviewManagement.ApplicantRequisitionId).FirstOrDefault();
            requisition.Status = Round + "-" + InterviewStatus.InterviewScheduled;
            requisition.ModifiedBy = CurrentContext.EmployeeID;
            requisition.ModifiedDate = DateTime.Now;
            dbContext.Entry(requisition).State = EntityState.Modified;
            dbContext.TblApplicantRequisition.Update(requisition);
            dbContext.SaveChanges();
            dbContext.Entry(requisition).State = EntityState.Detached;
            SendInterviewScheduledMailToApplicant(tblInterviewManagement);
            SendInterviewScheduledMailToPanel(tblInterviewManagement, tblInterviewManagement.TblInterviewEmployeeMapping);
            return 1;
        }
        public int UpdateInterview(TblInterviewManagement tblInterviewManagement)
        {
            if (CurrentContext.RoleName != RoleConstants.EmployeeRole && CurrentContext.RoleName != RoleConstants.ManagerRole)
            {
                var interviewers = dbContext.TblInterviewEmployeeMapping.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).ToList();
                SendInterviewCancelledMailToPanel(tblInterviewManagement, interviewers);
                foreach (TblInterviewEmployeeMapping interviewer in interviewers)
                {
                    dbContext.TblInterviewEmployeeMapping.Remove(interviewer);
                }
                dbContext.SaveChanges();
                for (int i = 0; tblInterviewManagement.TblInterviewEmployeeMapping.Count > i; i++)
                {
                    TblInterviewEmployeeMapping tblInterviewEmployeeMapping = new TblInterviewEmployeeMapping();
                    tblInterviewEmployeeMapping.InterviewId = tblInterviewManagement.InterviewId;
                    tblInterviewEmployeeMapping.IsActive = true;
                    tblInterviewEmployeeMapping.EmployeeId = tblInterviewManagement.TblInterviewEmployeeMapping.ElementAt(i).EmployeeId;
                    dbContext.TblInterviewEmployeeMapping.Add(tblInterviewEmployeeMapping);
                }
                dbContext.SaveChanges();
                SendInterviewScheduledMailToPanel(tblInterviewManagement, tblInterviewManagement.TblInterviewEmployeeMapping);
            }
            dbContext.Entry(tblInterviewManagement).State = EntityState.Modified;
            tblInterviewManagement.ModifiedBy = CurrentContext.EmployeeID;
            tblInterviewManagement.ModifiedDate = DateTime.Now;
            string Round = tblInterviewManagement.Status;
            if (CurrentContext.RoleName != RoleConstants.EmployeeRole && CurrentContext.RoleName != RoleConstants.ManagerRole)
            {
                tblInterviewManagement.Status = InterviewStatus.InterviewScheduled;
            }
            dbContext.TblInterviewManagement.Update(tblInterviewManagement);
            dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;
            //Requisition           
            var requisition = dbContext.TblApplicantRequisition.Where(r => r.ApplicantRequisitionId == tblInterviewManagement.ApplicantRequisitionId).FirstOrDefault();
            if (CurrentContext.RoleName != RoleConstants.EmployeeRole && CurrentContext.RoleName != RoleConstants.ManagerRole)
            {
                requisition.Status = Round + "-" + tblInterviewManagement.Status;
            }
            else
            {
                requisition.Status = requisition.Status.Split("-")[0] + "-" + tblInterviewManagement.Status;
            }
            requisition.ModifiedBy = CurrentContext.EmployeeID;
            requisition.ModifiedDate = DateTime.Now;
            dbContext.Entry(requisition).State = EntityState.Modified;
            dbContext.TblApplicantRequisition.Update(requisition);
            dbContext.SaveChanges();
            dbContext.Entry(requisition).State = EntityState.Detached;
            SendFeedbackMailToRecruiter(tblInterviewManagement);
            return 1;
        }
        public int CancelInterview(long id)
        {
            TblInterviewManagement tblInterviewManagement = dbContext.TblInterviewManagement.Where(r => r.InterviewId == id).FirstOrDefault();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Modified;
            tblInterviewManagement.ModifiedBy = CurrentContext.EmployeeID;
            tblInterviewManagement.ModifiedDate = DateTime.Now;
            tblInterviewManagement.Status = InterviewStatus.Cancelled;
            dbContext.TblInterviewManagement.Update(tblInterviewManagement);
            dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;
            //Requisition           
            var requisition = dbContext.TblApplicantRequisition.Where(r => r.ApplicantRequisitionId == tblInterviewManagement.ApplicantRequisitionId).FirstOrDefault();
            requisition.Status = requisition.Status.Split("-")[0] + "-" + tblInterviewManagement.Status;
            requisition.ModifiedBy = CurrentContext.EmployeeID;
            requisition.ModifiedDate = DateTime.Now;
            dbContext.Entry(requisition).State = EntityState.Modified;
            dbContext.TblApplicantRequisition.Update(requisition);
            dbContext.SaveChanges();
            dbContext.Entry(requisition).State = EntityState.Detached;
            if (CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {
                var interviewers = dbContext.TblInterviewEmployeeMapping.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).ToList();
                foreach (TblInterviewEmployeeMapping interviewer in interviewers)
                {
                    //Send Email Cancelled Interview
                    dbContext.Entry(interviewer).State = EntityState.Modified;
                    interviewer.IsActive = false;
                    dbContext.TblInterviewEmployeeMapping.Update(interviewer);
                    dbContext.SaveChanges();
                    dbContext.Entry(interviewer).State = EntityState.Detached;

                }
                SendInterviewCancelledMailToPanel(tblInterviewManagement, interviewers);
            }
            if (CurrentContext.RoleName == RoleConstants.EmployeeRole)
            {
                SendInterviewCancelledMailToRecruiter(tblInterviewManagement);
            }
            return 1;
        }
        public List<TblInterviewManagement> GetAllInterviews(long applicantId)
        {
            return dbContext.TblInterviewManagement.Where(r => r.ApplicantId == applicantId).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMapping).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMapping).ThenInclude(r => r.Employee).ToList();
        }
        #endregion
        #region Email       

        private async void SendInterviewScheduledMailToApplicant(TblInterviewManagement tblInterviewManagement)
        {
            DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
            var interview = dbContext.TblInterviewManagement.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).AsNoTracking().Include(r => r.Applicant).Include(r => r.ApplicantRequisition.Requisition.Designation).FirstOrDefault();
            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.ToEmailAddress = new string[] { dbContext.TblApplicants.Where(r => r.ApplicantId == tblInterviewManagement.ApplicantId).FirstOrDefault().EmailAddress };
            StringBuilder stringBuilder = new StringBuilder();
            TblInterviewEmployeeMapping tblInterviewEmployees = dbContext.TblInterviewEmployeeMapping.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
            stringBuilder.AppendLine("Dear " + interview.Applicant.Name + ",");
            stringBuilder.AppendLine("Your application for the '" + interview.ApplicantRequisition.Requisition.Designation.Designation + "' position stood out to us and we would like to invite you for an interview at our office[s] to get to know you a bit better.");
            stringBuilder.AppendLine("Your interview has been scheduled for '" + interview.InterviewDate + ", at " + tblInterviewManagement.Venue + "' and your Interview panel is '" + tblInterviewManagement.InterviewPanel + "'.");
            stringBuilder.AppendLine("Please call me at '1234567890' or email me at '" + emailTemplate.ToEmailAddress[0] + "', if you have any questions or need to reschedule.");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Sincerely,");
            stringBuilder.AppendLine(tblInterviewEmployees.Employee.EmployeeName);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Invitation to Interview";
            emailTemplate.InvitationStartTime = dt;
            emailTemplate.InvitationEndTime = dt.AddHours(1);
            emailTemplate.Location = tblInterviewManagement.Venue;
            emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
            emailTemplate.UniqueIdentifier = tblInterviewManagement.EmailGuid;
            await EmailHelper.SendMailWithCalendarInvite(emailTemplate);
        }
        private async void SendInterviewScheduledMailToPanel(TblInterviewManagement tblInterviewManagement, ICollection<TblInterviewEmployeeMapping> tblInterviewEmployeeMapping)
        {
            DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
            var interview = dbContext.TblInterviewManagement.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).AsNoTracking().Include(r => r.Applicant).Include(r => r.ApplicantRequisition.Requisition.Designation).FirstOrDefault();
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblInterviewEmployeeMapping tblInterviewEmployees = dbContext.TblInterviewEmployeeMapping.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
            stringBuilder.AppendLine("Hi " + tblInterviewEmployees.Employee.EmployeeName + ",");
            stringBuilder.AppendLine("Interview has been Raised for you on '" + tblInterviewManagement.InterviewDate + "' at '" + tblInterviewManagement.Venue + "'.");
            stringBuilder.AppendLine("Applicant Name - " + interview.Applicant.Name);
            stringBuilder.AppendLine("Comments about Candidate - '" + tblInterviewManagement.Comments + "'.");
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Interview has been Scheduled";
            emailTemplate.InvitationStartTime = dt;
            emailTemplate.InvitationEndTime = dt.AddHours(1);
            emailTemplate.Location = tblInterviewManagement.Venue;
            emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
            List<string> emails = new List<string>();
            foreach (TblInterviewEmployeeMapping interviewer in tblInterviewEmployeeMapping)
            {
                emails.Add(dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == interviewer.EmployeeId).FirstOrDefault().EmailId);
            }
            emailTemplate.ToEmailAddress = emails.ToArray();
            emailTemplate.UniqueIdentifier = tblInterviewManagement.EmailGuid;
            await EmailHelper.SendMailWithCalendarInvite(emailTemplate);
        }
        private async void SendFeedbackMailToRecruiter(TblInterviewManagement tblInterviewManagement)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Interviewer " + tblInterviewManagement.Applicant.Name);
            stringBuilder.AppendLine("Applicant Name " + tblInterviewManagement.Applicant.Name);
            stringBuilder.AppendLine("Feedback on Candidate: '" + tblInterviewManagement.FeedBack + "'.");
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Interviewer feedback";
            emailTemplate.EmailFromDisplayName = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == CurrentContext.EmployeeID).FirstOrDefault().EmployeeName;
            emailTemplate.ToEmailAddress = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblInterviewManagement.CreatedBy).AsNoTracking().ToList().Select(r => r.EmailId).ToArray();
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }

        public async void SendInterviewCancelledMailToPanel(TblInterviewManagement tblInterviewManagement, ICollection<TblInterviewEmployeeMapping> tblInterviewEmployeeMapping)
        {
            DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblInterviewEmployeeMapping tblInterviewEmployees = dbContext.TblInterviewEmployeeMapping.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
            stringBuilder.AppendLine("Hi " + tblInterviewEmployees.Employee.EmployeeName + ",");
            stringBuilder.AppendLine("Interview has been Cancelled for you on '" + tblInterviewManagement.InterviewDate + "'.");
            stringBuilder.AppendLine("Applicant Name - " + tblInterviewManagement.Applicant.Name);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Interview has been Cancelled";
            emailTemplate.InvitationStartTime = dt;
            emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
            List<string> emails = new List<string>();
            foreach (TblInterviewEmployeeMapping interviewer in tblInterviewEmployeeMapping)
            {
                emails.Add(dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == interviewer.EmployeeId).FirstOrDefault().EmailId);
            }
            emailTemplate.ToEmailAddress = emails.ToArray();
            emailTemplate.UniqueIdentifier = tblInterviewManagement.EmailGuid;
            await EmailHelper.SendCancelCalendarInvite(emailTemplate);
        }

        private async void SendInterviewCancelledMailToRecruiter(TblInterviewManagement tblInterviewManagement)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Hi,");
            stringBuilder.AppendLine("I will be busy on '" + tblInterviewManagement.InterviewDate + "', so you can reschedule or cancel interview for me.");
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Interview Reschedule or cancel";
            emailTemplate.EmailFromDisplayName = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == CurrentContext.EmployeeID).FirstOrDefault().EmployeeName;
            emailTemplate.ToEmailAddress = dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == tblInterviewManagement.CreatedBy).AsNoTracking().ToList().Select(r => r.EmailId).ToArray();
            await EmailHelper.SendMailWithoutCalendarInvite(emailTemplate);
        }
        #endregion
    }
}