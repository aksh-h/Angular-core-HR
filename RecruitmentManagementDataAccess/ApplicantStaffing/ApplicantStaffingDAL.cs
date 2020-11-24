using Microsoft.EntityFrameworkCore;
using RecruitmentManagementDataAccess.Helpers;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementDataAccess.ApplicantStaffing
{
    public  class ApplicantStaffingDAL
    {

        private readonly  RecruitmentManagementContext dbContext;
        public ApplicantStaffingDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }

        #region Applicant

        #region Applicants

        public  int CreateApplicants(TblApplicants tblApplicants)
        {
            dbContext.Entry(tblApplicants).State = EntityState.Added;
            dbContext.TblApplicants.Add(tblApplicants);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblApplicants).State = EntityState.Detached;
            return result;
        }

        public  int DeleteApplicants(int key)
        {
            TblApplicants applicants = dbContext.TblApplicants.Where(x => x.ApplicantId == key).FirstOrDefault();
            dbContext.TblApplicants.Remove(applicants);
            return dbContext.SaveChanges();
        }
        public  int UpdateApplicants(TblApplicants tblApplicants)
        {
            dbContext.Entry(tblApplicants).State = EntityState.Modified;
            dbContext.TblApplicants.Update(tblApplicants);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblApplicants).State = EntityState.Detached;
            return result;

        }

        public  async  Task<TblApplicants> GetApplicantsByCriteria(int key)
        {
            return dbContext.TblApplicants.Where(x => x.ApplicantId == key).FirstOrDefault();
        }

        public  TblInterviewManagement GetInterviewById(int id)
        {

            var item = dbContext.TblInterviewManagementStaffing.Where(k => k.InterviewId == id).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMappingStaffing).ThenInclude(r => r.Employee).FirstOrDefault();
            TblInterviewManagement interviews = new TblInterviewManagement();


            List<TblInterviewEmployeeMapping> employeeMappings = new List<TblInterviewEmployeeMapping>();
            foreach (var interviewer in item.TblInterviewEmployeeMappingStaffing)
            {
                employeeMappings.Add(new TblInterviewEmployeeMapping()
                {
                    InterviewEmployeeMappingId = interviewer.InterviewEmployeeMappingId,
                    InterviewId = interviewer.InterviewId,
                    EmployeeId = interviewer.EmployeeId,
                    IsActive = interviewer.IsActive,
                    Employee = interviewer.Employee,


                });
            }
            List<TblRequisitionSkillMapping> skillMappings = new List<TblRequisitionSkillMapping>();


            foreach (var skill in item.ApplicantRequisition.Requisition.TblRequisitionSkillMappingStaffing)
            {
                skillMappings.Add(new TblRequisitionSkillMapping()
                {
                    RequisitionSkillId = skill.RequisitionSkillId,
                    RequisitionId = skill.RequisitionId,
                    SkillId = skill.SkillId,
                    Skill = skill.Skill
                });
            }
            TblInterviewManagement interview = new TblInterviewManagement()
            {
                InterviewId = item.InterviewId,
                InterviewPanel = item.InterviewPanel,
                ApplicantId = item.ApplicantId,
                InterviewDate = item.InterviewDate,
                Comments = item.Comments,
                IsCompleted = item.IsCompleted,
                CreatedBy = item.CreatedBy,
                CreatedDate = item.CreatedDate,
                ModifiedBy = item.ModifiedBy,
                ModifiedDate = item.ModifiedDate,
                ApplicantRequisitionId = item.ApplicantRequisitionId,
                Status = item.Status,
                Venue = item.Venue,
                FeedBack = item.FeedBack,
                EmailGuid = item.EmailGuid,
                Communication = item.Communication,
                Attitude = item.Attitude,
                SkillRatings = item.SkillRatings,
                RoundName = item.RoundName,
                ModeOfInterview = item.ModeOfInterview,
                IsClient = item.IsClient,

                Applicant = new TblApplicants()
                {
                    ApplicantId = item.Applicant.ApplicantId,
                    Name = item.Applicant.Name,
                    Dob = item.Applicant.Dob,
                    EmailAddress = item.Applicant.EmailAddress,
                    Qualification = item.Applicant.Qualification,
                    Experience = item.Applicant.Experience,
                    RelevantExperience = item.Applicant.RelevantExperience,
                    CurrentCtc = item.Applicant.CurrentCtc,
                    ExpectedCtc = item.Applicant.ExpectedCtc,
                    JoiningTime = item.Applicant.JoiningTime,
                    SkillsandProficiency = item.Applicant.SkillsandProficiency,
                    LocationPreference = item.Applicant.LocationPreference,
                    ReferedBy = item.Applicant.ReferedBy,
                    JobType = item.Applicant.JobType,
                    Status = item.Applicant.Status,
                    NoticePeriod = item.Applicant.NoticePeriod,
                    ApplicantActive = item.Applicant.ApplicantActive,
                    ShortlistedBy = item.Applicant.ShortlistedBy,
                },
                CreatedByNavigation = new TblEmployees()
                {
                    EmployeeId = item.CreatedByNavigation.EmployeeId,
                    EmployeeName = item.CreatedByNavigation.EmployeeName,
                    DesignationId = item.CreatedByNavigation.DesignationId,
                    DepartmentId = item.CreatedByNavigation.DepartmentId,
                    ReportingManagerId = item.CreatedByNavigation.ReportingManagerId,
                    RoleId = item.CreatedByNavigation.RoleId,
                    IsActive = item.CreatedByNavigation.IsActive,
                    CreatedDate = item.CreatedByNavigation.CreatedDate,
                    CreatedBy = item.CreatedByNavigation.CreatedBy,
                    ModifiedBy = item.CreatedByNavigation.ModifiedBy,
                    ModifiedDate = item.CreatedByNavigation.ModifiedDate,
                    EmailId = item.CreatedByNavigation.EmailId,

                },
                ApplicantRequisition = new TblApplicantRequisition()
                {
                    ApplicantRequisitionId = item.ApplicantRequisition.ApplicantRequisitionId,
                    ApplicantId = item.ApplicantRequisition.ApplicantId,
                    RequisitionId = item.ApplicantRequisition.RequisitionId,
                    Status = item.ApplicantRequisition.Status,
                    CreatedBy = item.ApplicantRequisition.CreatedBy,
                    ModifiedBy = item.ApplicantRequisition.ModifiedBy,
                    CreatedDate = item.ApplicantRequisition.CreatedDate,
                    ModifiedDate = item.ApplicantRequisition.ModifiedDate,
                    RecruiterComment = item.ApplicantRequisition.RecruiterComment,
                    CurrentCtc = item.ApplicantRequisition.CurrentCtc,
                    ExpectedCtc = item.ApplicantRequisition.ExpectedCtc,
                    NegotiatedCtc = item.ApplicantRequisition.NegotiatedCtc,
                    TentativeJoiningDate = item.ApplicantRequisition.TentativeJoiningDate,

                    Requisition = new TblRequisition()
                    {
                        RequistionId = item.ApplicantRequisition.Requisition.RequistionId,
                        DepartmentId = item.ApplicantRequisition.Requisition.DepartmentId,
                        YearsofExperience = item.ApplicantRequisition.Requisition.YearsofExperience,
                        JoiningTenure = item.ApplicantRequisition.Requisition.JoiningTenure,
                        NoofPositions = item.ApplicantRequisition.Requisition.NoofPositions,
                        Location = item.ApplicantRequisition.Requisition.Location,
                        IsActive = item.ApplicantRequisition.Requisition.IsActive,
                        Comments = item.ApplicantRequisition.Requisition.Comments,
                        Status = item.ApplicantRequisition.Requisition.Status,
                        ManagerEmployeeId = item.ApplicantRequisition.Requisition.ManagerEmployeeId,
                        ManagerComments = item.ApplicantRequisition.Requisition.ManagerComments,
                        CreatedBy = item.ApplicantRequisition.Requisition.CreatedBy,
                        CreatedDate = item.ApplicantRequisition.Requisition.CreatedDate,
                        ModifiedBy = item.ApplicantRequisition.Requisition.ModifiedBy,
                        ModifiedDate = item.ApplicantRequisition.Requisition.ModifiedDate,
                        DesignationId = item.ApplicantRequisition.Requisition.DesignationId,
                        Title = item.ApplicantRequisition.Requisition.Title,
                        RecruiterComment = item.ApplicantRequisition.Requisition.RecruiterComment,
                        RecruiterLeadId = item.ApplicantRequisition.Requisition.RecruiterLeadId,
                        CurrentOwner = item.ApplicantRequisition.Requisition.CurrentOwner,
                        ApplicantWorkFlow = item.ApplicantRequisition.Requisition.ApplicantWorkFlow,
                        CancelComments = item.ApplicantRequisition.Requisition.CancelComments,
                        ClientId = item.ApplicantRequisition.Requisition.ClientId,
                        TblRequisitionSkillMapping = skillMappings

                    }
                },
                TblInterviewEmployeeMapping = employeeMappings
            };
            interviews = interview;


            return interviews;
        }

        #endregion

        public  List<TblApplicants> GetAllApplicant()
        {
            return dbContext.TblApplicants.AsNoTracking().ToList();
        }
        public List<TblRequisitionSkillMappingStaffing> GetAllSkills(int RequisitionId)
        {
            return dbContext.TblRequisitionSkillMappingStaffing.AsNoTracking().Where(x=>x.RequisitionId == RequisitionId).ToList();
        }

        public  List<TblApplicantRequisitionStaffing> GetAllShortListedApplicant()
        {
            return dbContext.TblApplicantRequisitionStaffing.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Applicant.Status != ApplicantStatus.Selected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }

        public List<TblApplicantRequisitionClient> GetAllShortListedApplicantClient()
        {
            return dbContext.TblApplicantRequisitionClient.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Applicant.Status == ApplicantStatus.Blocked && r.Applicant.Status != ApplicantStatus.Selected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }

        public  List<TblInterviewManagementStaffing> GetAllApplicantInterviewDetails(int key)
        {
            var result = dbContext.TblInterviewManagementStaffing.AsNoTracking().Where(r => r.ApplicantRequisitionId == key).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.TblInterviewEmployeeMappingStaffing).ThenInclude(r => r.Employee).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(r => r.Skill).ToList();
            return result;
        }

        public  List<TblApplicantRequisitionStaffing> GetAllSelectedApplicant()
        {
            return dbContext.TblApplicantRequisitionStaffing.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Status == InterviewStatus.Selected).Include(r => r.Requisition).AsNoTracking().Include(r => r.Applicant).AsNoTracking().ToList();
        }
        public  List<TblApplicantRequisitionStaffing> GetAllRejectedApplicant()
        {
            return dbContext.TblApplicantRequisitionStaffing.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Status == InterviewStatus.Rejected).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }

        public  List<TblApplicantRequisitionStaffing> GetAllBlackListedApplicant()
        {
            return dbContext.TblApplicantRequisitionStaffing.AsNoTracking().Where(r => r.CreatedBy == CurrentContext.EmployeeID && r.Status == InterviewStatus.BlackListed).Include(r => r.Requisition).Include(r => r.Applicant).ToList();
        }

        private  int SaveApplicants(TblApplicants tblApplicants)
        {
            dbContext.TblApplicants.Add(tblApplicants);
            return dbContext.SaveChanges();
        }

        public  async Task<TblSelectedApplicantsStaffing> GetApplicantJoiningDetails(int id)
        {
           var result = dbContext.TblSelectedApplicantsStaffing.AsNoTracking().Where(r => r.ApplicantRequisitionId == id).AsNoTracking().FirstOrDefault();
            if(result == null)
            {
                result = new TblSelectedApplicantsStaffing();
            }
            return result;
            
        }

        public  int CreateApplicantJoining(TblSelectedApplicantsStaffing tblSelectedApplicants)
        {

            dbContext.Entry(tblSelectedApplicants).State = EntityState.Added;
            dbContext.TblSelectedApplicantsStaffing.Add(tblSelectedApplicants);
            int result = dbContext.SaveChanges();
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Detached;
            return result;
        }

        public  long UpdateApplicantJoining(TblSelectedApplicantsStaffing tblSelectedApplicants)
        {
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Added;
            dbContext.TblSelectedApplicantsStaffing.Add(tblSelectedApplicants);
            long result = dbContext.SaveChanges();
            dbContext.Entry(tblSelectedApplicants).State = EntityState.Detached;
            
            result = tblSelectedApplicants.SelectedApplicantsId;
            return result;
        }



        #endregion

        #region Interview
        public  TblApplicantRequisitionStaffing GetRequisitionApplicantDetails(long key)
        {
            
         
                return dbContext.TblApplicantRequisitionStaffing.AsNoTracking().Where(r => r.ApplicantRequisitionId == key).AsNoTracking().Include(r => r.Requisition).AsNoTracking().Include(r => r.Applicant).AsNoTracking().FirstOrDefault();
            
        }
        public  List<TblEmployeePanelMapping> GetAllInterviewer()
        {
            return dbContext.TblEmployeePanelMapping.AsNoTracking().Where(r => r.IsActive == true).Include(r => r.Employee).ToList();
        }
        public  int SaveInterviewDetails(TblInterviewManagementStaffing tblInterviewManagement)
        {
            dbContext.Entry(tblInterviewManagement).State = EntityState.Added;
            tblInterviewManagement.CreatedBy = CurrentContext.EmployeeID;
            tblInterviewManagement.CreatedDate = DateTime.Now;
            tblInterviewManagement.IsCompleted = false;
            tblInterviewManagement.EmailGuid = new Guid();
            tblInterviewManagement.IsClient = true;
            if (string.IsNullOrEmpty(tblInterviewManagement.InterviewPanel))
            {
                tblInterviewManagement.InterviewPanel = "Client";
            }
            string Round = tblInterviewManagement.Status;
            tblInterviewManagement.Status = InterviewStatus.InterviewScheduled;
            dbContext.TblInterviewManagementStaffing.Add(tblInterviewManagement);
            dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;
            if (tblInterviewManagement.InterviewPanel == "Client")
            {
                tblInterviewManagement.TblInterviewEmployeeMappingStaffing = new List<TblInterviewEmployeeMappingStaffing>();
                tblInterviewManagement.TblInterviewEmployeeMappingStaffing.Add(new TblInterviewEmployeeMappingStaffing()
                {
                    EmployeeId = 9,
                    InterviewId = tblInterviewManagement.InterviewId,
                    IsActive = true,
                });

            }
            for (int i = 0; tblInterviewManagement.TblInterviewEmployeeMappingStaffing.Count > i; i++)
            {
                tblInterviewManagement.TblInterviewEmployeeMappingStaffing.ElementAt(i).InterviewId = tblInterviewManagement.InterviewId;
                tblInterviewManagement.TblInterviewEmployeeMappingStaffing.ElementAt(i).IsActive = true;
                dbContext.TblInterviewEmployeeMappingStaffing.Add(tblInterviewManagement.TblInterviewEmployeeMappingStaffing.ElementAt(i));

            }
            dbContext.SaveChanges();
            //Requisition
            var requisition = dbContext.TblApplicantRequisitionStaffing.Where(r => r.ApplicantRequisitionId == tblInterviewManagement.ApplicantRequisitionId).FirstOrDefault();
            requisition.Status = Round + "-" + InterviewStatus.InterviewScheduled;
            requisition.ModifiedBy = CurrentContext.EmployeeID;
            requisition.ModifiedDate = DateTime.Now;
            dbContext.Entry(requisition).State = EntityState.Modified;
            dbContext.TblApplicantRequisitionStaffing.Update(requisition);
            dbContext.SaveChanges();
            dbContext.Entry(requisition).State = EntityState.Detached;
            SendInterviewScheduledMailToApplicant(tblInterviewManagement);
           // SendInterviewScheduledMailToPanel(tblInterviewManagement);
            return 1;
        }
        public  int UpdateInterview(TblInterviewManagementStaffing tblInterviewManagement)
        {
            if (CurrentContext.RoleName != RoleConstants.EmployeeRole && CurrentContext.RoleName != RoleConstants.ManagerRole)
            {
                var interviewers = dbContext.TblInterviewEmployeeMappingStaffing.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).ToList();
                SendInterviewCancelledMailToPanel(tblInterviewManagement, interviewers);
                foreach (TblInterviewEmployeeMappingStaffing interviewer in interviewers)
                {
                    dbContext.TblInterviewEmployeeMappingStaffing.Remove(interviewer);
                }
                dbContext.SaveChanges();

                for (int i = 0; tblInterviewManagement.TblInterviewEmployeeMappingStaffing.Count > i; i++)
                {
                    TblInterviewEmployeeMappingStaffing tblInterviewEmployeeMapping = new TblInterviewEmployeeMappingStaffing();
                    tblInterviewEmployeeMapping.InterviewId = tblInterviewManagement.InterviewId;
                    tblInterviewEmployeeMapping.IsActive = true;
                    tblInterviewEmployeeMapping.EmployeeId = tblInterviewManagement.TblInterviewEmployeeMappingStaffing.ElementAt(i).EmployeeId;
                    dbContext.TblInterviewEmployeeMappingStaffing.Add(tblInterviewEmployeeMapping);



                }
                dbContext.SaveChanges();
                //SendInterviewScheduledMailToPanel(tblInterviewManagement);
            }

            dbContext.Entry(tblInterviewManagement).State = EntityState.Modified;
            tblInterviewManagement.ModifiedBy = CurrentContext.EmployeeID;
            tblInterviewManagement.ModifiedDate = DateTime.Now;
            string Round = tblInterviewManagement.Status;
            if (CurrentContext.RoleName != RoleConstants.EmployeeRole && CurrentContext.RoleName != RoleConstants.ManagerRole && CurrentContext.RoleName != RoleConstants.StaffingLeadRole && CurrentContext.RoleName != RoleConstants.Client && CurrentContext.RoleName != RoleConstants.StaffingRole)
            {
                tblInterviewManagement.Status = InterviewStatus.InterviewScheduled;
            }
            dbContext.TblInterviewManagementStaffing.Update(tblInterviewManagement);
            dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;



            //Requisition           
            var requisition = dbContext.TblApplicantRequisitionStaffing.Where(r => r.ApplicantRequisitionId == tblInterviewManagement.ApplicantRequisitionId).FirstOrDefault();
            if (CurrentContext.RoleName != RoleConstants.EmployeeRole && CurrentContext.RoleName != RoleConstants.ManagerRole && CurrentContext.RoleName != RoleConstants.StaffingLeadRole && CurrentContext.RoleName != RoleConstants.Client)
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
            dbContext.TblApplicantRequisitionStaffing.Update(requisition);
            dbContext.SaveChanges();
            dbContext.Entry(requisition).State = EntityState.Detached;
            SendFeedbackMailToRecruiter(tblInterviewManagement);
            return 1;
        }

        public int UpdateStatusOnStaffingFeedback(TblInterviewManagementStaffing tblInterviewManagement)
        {
            var entity = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).FirstOrDefault();
            entity.RecruitersFeedBack = tblInterviewManagement.RecruitersFeedBack;
            dbContext.Entry(tblInterviewManagement).State = EntityState.Modified;
            dbContext.TblInterviewManagementStaffing.Update(entity);
            int result=dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;
            return result;
        }

        public  int CancelInterview(long id)
        {
            TblInterviewManagementStaffing tblInterviewManagement = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewId == id).FirstOrDefault();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Modified;
            tblInterviewManagement.ModifiedBy = CurrentContext.EmployeeID;
            tblInterviewManagement.ModifiedDate = DateTime.Now;
            tblInterviewManagement.Status = InterviewStatus.Cancelled;
            dbContext.TblInterviewManagementStaffing.Update(tblInterviewManagement);
            dbContext.SaveChanges();
            dbContext.Entry(tblInterviewManagement).State = EntityState.Detached;

            //Requisition           
            var requisition = dbContext.TblApplicantRequisitionStaffing.Where(r => r.ApplicantRequisitionId == tblInterviewManagement.ApplicantRequisitionId).FirstOrDefault();
            requisition.Status = requisition.Status.Split("-")[0] + "-" + tblInterviewManagement.Status;
            requisition.ModifiedBy = CurrentContext.EmployeeID;
            requisition.ModifiedDate = DateTime.Now;
            dbContext.Entry(requisition).State = EntityState.Modified;
            dbContext.TblApplicantRequisitionStaffing.Update(requisition);
            dbContext.SaveChanges();
            dbContext.Entry(requisition).State = EntityState.Detached;

            if (CurrentContext.RoleName == RoleConstants.RecruiterRole)
            {

                var interviewers = dbContext.TblInterviewEmployeeMappingStaffing.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).ToList();
                foreach (TblInterviewEmployeeMappingStaffing interviewer in interviewers)
                {
                    //Send Email Cancelled Interview

                    dbContext.Entry(interviewer).State = EntityState.Modified;
                    interviewer.IsActive = false;
                    dbContext.TblInterviewEmployeeMappingStaffing.Update(interviewer);
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

        public  List<TblInterviewManagementStaffing> GetAllInterviews(long applicantId)
        {
            return dbContext.TblInterviewManagementStaffing.Where(r => r.ApplicantId == applicantId).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMappingStaffing).ThenInclude(r => r.Employee).ToList();
        }

        #endregion

        #region Email       


        private  async void SendInterviewScheduledMailToApplicant(TblInterviewManagementStaffing tblInterviewManagement)
        {
            DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
            var interview = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).AsNoTracking().Include(r => r.Applicant).Include(r => r.ApplicantRequisition.Requisition.Designation).FirstOrDefault();
            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.ToEmailAddress = new string[] { dbContext.TblApplicants.Where(r => r.ApplicantId == tblInterviewManagement.ApplicantId).FirstOrDefault().EmailAddress };
            StringBuilder stringBuilder = new StringBuilder();
            TblInterviewEmployeeMappingStaffing tblInterviewEmployees = dbContext.TblInterviewEmployeeMappingStaffing.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
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

        //private  async void SendInterviewScheduledMailToPanel(TblInterviewManagementStaffing tblInterviewManagement)
        //{
        //    try
        //    {
        //        long reid = tblInterviewManagement.ApplicantRequisition.RequisitionId;
        //        var contactdetails = dbContext.TblRequisitionClientContactMappingStaffing.Where(r => r.RequisitionId == reid).Include(r => r.Contact);
        //        DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
        //        var interview = dbContext.TblInterviewManagementStaffing.Where(r => r.InterviewId == tblInterviewManagement.InterviewId).AsNoTracking().Include(r => r.Applicant).Include(r => r.ApplicantRequisition.Requisition.Designation).FirstOrDefault();
        //        EmailTemplate emailTemplate = new EmailTemplate();
        //        StringBuilder stringBuilder = new StringBuilder();
        //        TblInterviewEmployeeMappingStaffing tblInterviewEmployees = dbContext.TblInterviewEmployeeMappingStaffing.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
        //        stringBuilder.AppendLine("Hi " + tblInterviewEmployees.Employee.EmployeeName + ",");
        //        stringBuilder.AppendLine("Interview has been Raised for you on '" + tblInterviewManagement.InterviewDate + "' at '" + tblInterviewManagement.Venue + "'.");
        //        stringBuilder.AppendLine("Applicant Name - " + interview.Applicant.Name);
        //        stringBuilder.AppendLine("Comments about Candidate - '" + tblInterviewManagement.Comments + "'.");
        //        emailTemplate.Body = stringBuilder.ToString();
        //        emailTemplate.Subject = "Interview has been Scheduled";
        //        emailTemplate.InvitationStartTime = dt;
        //        emailTemplate.InvitationEndTime = dt.AddHours(1);
        //        emailTemplate.Location = tblInterviewManagement.Venue;
        //        emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
        //        List<string> emails = new List<string>();

        //        foreach (TblRequisitionClientContactMappingStaffing clientsContactDetails in contactdetails)
        //        {
        //            emails.Add(clientsContactDetails.Contact.ContactPersonEmailId);
        //        }

        //        emailTemplate.ToEmailAddress = emails.ToArray();
        //        emailTemplate.UniqueIdentifier = tblInterviewManagement.EmailGuid;
        //        await EmailHelper.SendMailWithCalendarInvite(emailTemplate);
        //    }
        //    catch
        //    {

        //    }
        //}

        private  async void SendFeedbackMailToRecruiter(TblInterviewManagementStaffing tblInterviewManagement)
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


        public  async void SendInterviewCancelledMailToPanel(TblInterviewManagementStaffing tblInterviewManagement, ICollection<TblInterviewEmployeeMappingStaffing> tblInterviewEmployeeMapping)
        {
            DateTime dt = (DateTime)tblInterviewManagement.InterviewDate;
            EmailTemplate emailTemplate = new EmailTemplate();
            StringBuilder stringBuilder = new StringBuilder();
            TblInterviewEmployeeMappingStaffing tblInterviewEmployees = dbContext.TblInterviewEmployeeMappingStaffing.AsNoTracking().Where(r => r.InterviewId == tblInterviewManagement.InterviewId).Include(r => r.Employee).FirstOrDefault();
            stringBuilder.AppendLine("Hi " + tblInterviewEmployees.Employee.EmployeeName + ",");
            stringBuilder.AppendLine("Interview has been Cancelled for you on '" + tblInterviewManagement.InterviewDate + "'.");
            stringBuilder.AppendLine("Applicant Name - " + tblInterviewManagement.Applicant.Name);
            emailTemplate.Body = stringBuilder.ToString();
            emailTemplate.Subject = "Interview has been Cancelled";
            emailTemplate.InvitationStartTime = dt;
            emailTemplate.EmailFromDisplayName = tblInterviewEmployees.Employee.EmployeeName;
            List<string> emails = new List<string>();
            foreach (TblInterviewEmployeeMappingStaffing interviewer in tblInterviewEmployeeMapping)
            {
                emails.Add(dbContext.TblEmployees.AsNoTracking().Where(r => r.EmployeeId == interviewer.EmployeeId).FirstOrDefault().EmailId);
            }
            emailTemplate.ToEmailAddress = emails.ToArray();
            emailTemplate.UniqueIdentifier = tblInterviewManagement.EmailGuid;
            await EmailHelper.SendCancelCalendarInvite(emailTemplate);
        }


        private  async void SendInterviewCancelledMailToRecruiter(TblInterviewManagementStaffing tblInterviewManagement)
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

