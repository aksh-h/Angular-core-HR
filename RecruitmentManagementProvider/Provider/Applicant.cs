using Microsoft.AspNetCore.Http;
using RecruitmentManagementDataAccess.Masters;
using RecruitmentManagementDataAccess.ModelExtensions;
using RecruitmentManagementDataAccess.Models;
using RecruitmentManagementProvider.IProvider;
using ResumeParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RecruitmentManagementProvider.Provider
{
    public class Applicant : IApplicant
    {
        ApplicantDAL ApplicantDAL = new ApplicantDAL();
       
        public int CreateApplicants(TblApplicants tblApplicants)
        {
            return ApplicantDAL.CreateApplicants(tblApplicants);
        }

        public int DeleteApplicants(int key)
        {
            return ApplicantDAL.DeleteApplicants(key);
        }

        public int UpdateApplicants(TblApplicants tblApplicants)
        {
            return ApplicantDAL.UpdateApplicants(tblApplicants);
        }

        public TblApplicants GetApplicantsByCriteria(int key)
        {
            return ApplicantDAL.GetApplicantsByCriteria(key);
        }
        public List<TblApplicants> GetAllApplicant()
        {
            return ApplicantDAL.GetAllApplicant();
        }

        public List<TblApplicants> GetAllApplicantFromRequisition(int RequisitionId)
        {
            var response =  ApplicantDAL.GetAllApplicant();
            ResumeProcessor resumeProcessor = new ResumeProcessor();
            var skills = ApplicantDAL.GetAllSkills(RequisitionId);
            foreach (var applicant in response)
            {
                
             //  string parsedResume =  resumeProcessor.GetRawString(applicant.ResumeLocation);
            }
            return response;
        }

        

        public TblApplicantRequisition GetRequisitionApplicantDetails(long key)
        {
            return ApplicantDAL.GetRequisitionApplicantDetails(key);
        }

        public List<TblEmployeePanelMapping> GetAllInterviewer()
        {
            return ApplicantDAL.GetAllInterviewer();
        }

        public List<TblApplicantRequisition> GetAllShortListedApplicant()
        {
            return ApplicantDAL.GetAllShortListedApplicant();
        }

        public List<TblApplicantRequisitionClient> GetAllShortListedApplicantClient()
        {
            return ApplicantDAL.GetAllShortListedApplicantClient();
        }

        public List<TblInterviewManagement> GetAllApplicantInterviewDetails(int key)
        {
            return ApplicantDAL.GetAllApplicantInterviewDetails(key);
        }

        public int SaveInterviewDetails(TblInterviewManagement tblInterviewManagement)
        {            
            return ApplicantDAL.SaveInterviewDetails(tblInterviewManagement);
        }

        public int UpdateInterview(TblInterviewManagement tblInterviewManagement)
        {
            return ApplicantDAL.UpdateInterview(tblInterviewManagement);
        }

        public List<TblApplicantRequisition> GetAllSelectedApplicant()
        {
            return ApplicantDAL.GetAllSelectedApplicant();
        }

        public List<TblApplicantRequisition> GetAllRejectedApplicant()
        {
            return ApplicantDAL.GetAllRejectedApplicant();
        }

        public List<TblApplicantRequisition> GetAllBlackListedApplicant()
        {
            return ApplicantDAL.GetAllBlackListedApplicant();
        }

        

        public int CancelInterview(long id)
        {
            return ApplicantDAL.CancelInterview(id);
        }

        public List<TblInterviewManagement> GetAllInterviews(long applicantId)
        {
            return ApplicantDAL.GetAllInterviews(applicantId);
        }

        public async Task<TblSelectedApplicants> GetInternalApplicantJoiningDetails(int id)
        {
            return await ApplicantDAL.GetInternalApplicantJoiningDetails(id);
        }

        public int CreateInternalApplicantJoining(TblSelectedApplicants tblSelectedApplicants)
        {
            return ApplicantDAL.CreateInternalApplicantJoining(tblSelectedApplicants);
        }
        public long UpdateInternalApplicantJoining(TblSelectedApplicants tblSelectedApplicants)
        {
            return ApplicantDAL.UpdateInternalApplicantJoining(tblSelectedApplicants);
        }

        public string ImportExcel(IFormFile file)
        {
            if (file.Length > 0)
            {
                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return fullPath;
            }
            else
            {
                return "0";
            }
        }

        public int ResumeImport(string path,ResumeUpload resumeUpload)
        {
            ResumeProcessor resumeProcessor = new ResumeProcessor();
            var model= resumeProcessor.ProcessModel(path);
            TblApplicants tblApplicants = new TblApplicants();
            tblApplicants.Name = model.Name;
            tblApplicants.EmailAddress = model.EmailAddress;
           // tblApplicants.
            if(resumeUpload.SourceId == 1)
            {
                tblApplicants.Source = "Direct";
               
            }
            else if (resumeUpload.SourceId == 2)
            {
                tblApplicants.Source = "Vendor";

            }
            else if (resumeUpload.SourceId == 3)
            {
                tblApplicants.Source = "Portal";

            }
            else if (resumeUpload.SourceId == 4)
            {
                tblApplicants.Source = "Internal";

            }
            tblApplicants.VendorId = resumeUpload.VendorId;
            tblApplicants.PortalId = resumeUpload.PortalId;
            tblApplicants.EmailAddress = resumeUpload.EmployeeEmailId;
            return ApplicantDAL.CreateApplicants(tblApplicants);

        }


    }
}
