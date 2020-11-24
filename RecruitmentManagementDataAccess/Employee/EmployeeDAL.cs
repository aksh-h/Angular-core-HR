using RecruitmentManagementDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ServiceStack;

namespace RecruitmentManagementDataAccess.Employee
{

    public  class EmployeeDAL
    {
        private readonly  RecruitmentManagementContext dbContext;
        public EmployeeDAL()
        {
            dbContext = new RecruitmentManagementContext();
        }

        public  List<TblInterviewManagement> GetAllInterviews()
        {
            var interviews = dbContext.TblInterviewManagement.Where(r => r.TblInterviewEmployeeMapping.Any(k => k.EmployeeId == CurrentContext.EmployeeID && k.IsActive == true)).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMapping).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMapping).ThenInclude(r => r.Employee).ToList();
            var clientinterviews = dbContext.TblInterviewManagementStaffing.Where(r => r.TblInterviewEmployeeMappingStaffing.Any(k => k.EmployeeId == CurrentContext.EmployeeID && k.IsActive == true)).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMappingStaffing).ThenInclude(r => r.Employee).ToList();
            foreach (var item in clientinterviews)
            {
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
                    RecruitersFeedBack= item.RecruitersFeedBack,

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
                interviews.Add(interview);
            }
            return interviews.OrderBy(x => x.InterviewDate).ToList();
        }

        public List<TblInterviewManagement> GetAllInterviewsHistory(long ApplicantRequisitionID)
        {
            var interviews = dbContext.TblInterviewManagement.Where(r => r.ApplicantRequisitionId == ApplicantRequisitionID).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMapping).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMapping).ThenInclude(r => r.Employee).ToList();
            var clientinterviews = dbContext.TblInterviewManagementStaffing.Where(r => r.ApplicantRequisitionId == ApplicantRequisitionID).AsNoTracking().Include(r => r.Applicant).Include(r => r.CreatedByNavigation).Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMappingStaffing).ThenInclude(r => r.Employee).ToList();
            foreach (var item in clientinterviews)
            {
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
                    RecruitersFeedBack = item.RecruitersFeedBack,

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
                interviews.Add(interview);
            }
            return interviews.OrderBy(x => x.InterviewDate).ToList();
        }

        public  List<TblInterviewManagementStaffing> GetAllClientInterviews()
        {
            var interview = dbContext.TblInterviewManagementStaffing.AsNoTracking().Where(r => r.RoundName == "Client" && r.ApplicantRequisition.Requisition.TblRequisitionClientContactMappingStaffing.Any(u => u.ContactId == CurrentContext.EmployeeID)).AsNoTracking().Include(r => r.Applicant).AsNoTracking().Include(r => r.CreatedByNavigation).AsNoTracking().Include(r => r.ApplicantRequisition).ThenInclude(r => r.Requisition).ThenInclude(r => r.TblRequisitionSkillMappingStaffing).ThenInclude(r => r.Skill).Include(r => r.TblInterviewEmployeeMappingStaffing).ThenInclude(r => r.Employee).AsNoTracking().ToList();
            return interview;
        }

        
    }
}


