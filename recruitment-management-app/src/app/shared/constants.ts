export const Roles = {
    ManagerRole: "Manager",
    AdminRole: "Admin",
    EmployeeRole: "Employee",
    RecruiterRole: "Recruiter",
    RecruiterLeadRole: "RecruiterLead",
    HRRole: "HR",
    StaffingLeadRole: "StaffingLead",
    StaffingRole:"Staffing",
    SalesHead :"SalesHead",
    Client:"Client",
    

};
 
export const RequisitionStatus = {
    Open: "Open",
    Approved: "Approved",
    Declined: "Declined",
    AssignedToRecruiter: "AssignedToRecruiter",
    InProgress: "InProgress"  ,
    Closed: "Closed",
    Cancelled: "Cancelled",
    CancelRequested: "CancelRequested"
};

export const ApplicantStatus = {
    Available: "Available",
    Blocked: "Blocked",      
    BlackListed: "BlackListed"  
};

export const InterviewStatus = {   
    InterviewScheduled: "Interview Scheduled",
    Selected: "Selected",
    Rejected: "Rejected" , 
    BlackListed: "BlackListed" , 
    NotAssigned : "NotAssigned"  ,
    Cancelled: "Cancelled",
    RescheduleRequested: "Reschedule Requested"
};
 
export abstract  class CurrentContext {
    public static UserID : Number;
    public static RoleName : String;
}