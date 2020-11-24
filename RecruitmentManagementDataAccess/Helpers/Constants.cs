using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentManagementDataAccess.Helpers
{
    public static class RoleConstants
    {
        public const string ManagerRole = "Manager";
        public const string AdminRole = "Admin";
        public const string EmployeeRole = "Employee";
        public const string RecruiterRole = "Recruiter";
        public const string RecruiterLeadRole = "RecruiterLead";
        public const string HRRole = "HR";
        public const string StaffingLeadRole = "StaffingLead";
        public const string StaffingRole = "Staffing";
        public const string SalesHead = "SalesHead";
        public const string Client = "Client";
        

    }
    public static class RequisitionStatus
    {
        public const string Open = "Open";
        public const string Approved = "Approved";
        public const string Declined = "Declined";
        public const string AssignedToRecruiter = "AssignedToRecruiter";
        public const string AssignedToStaffing = "AssignedToStaffing";
        public const string InProgress = "InProgress";
        public const string Closed = "Closed";
        public const string Cancelled = "Cancelled";
        public const string Internal = "Internal";
        public const string Client = "Client";
        public const string CancelRequested = "CancelRequested";
    }

    public static class ApplicantStatus
    {
        public const string Available = "Available";
        public const string Blocked = "Blocked";
        public const string BlackListed = "BlackListed";
        public const string Selected = "Selected";

    }

    public static class InterviewStatus
    {      
        public const string InterviewScheduled = "Interview Scheduled";
        public const string Selected = "Selected";
        public const string Rejected = "Rejected";
        public const string BlackListed = "BlackListed";
        public const string NotAssigned = "NotAssigned";
        public const string Cancelled = "Cancelled";
        public const string RescheduleRequested = "Reschedule Requested";

    }



}
