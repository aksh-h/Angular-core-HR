export class InterviewManagementModel {
    public interviewId: Number;
    public interviewPanel: String;
    public applicantId: Number;
    public interviewDate: String;
    public status: string;
    public comments: String;
    public isCompleted: Boolean;
    public createdBy: Number;
    public createdDate: Number;
    public modifiedBy: Number;
    public modifiedDate: Date;
    public applicantRequisitionId: Number;
    public venue: String;
    public feedBack: String;
    public tblInterviewEmployeeMapping: any;
    public roundName : String;
    public modeOfInterview : String
}

export class InterviewManagementStaffingModel {
    public interviewId: Number;
    public interviewPanel: String;
    public applicantId: Number;
    public interviewDate: String;
    public status: string;
    public comments: String;
    public isCompleted: Boolean;
    public createdBy: Number;
    public createdDate: Number;
    public modifiedBy: Number;
    public modifiedDate: Date;
    public applicantRequisitionId: Number;
    public venue: String;
    public feedBack: String;
    public tblInterviewEmployeeMappingStaffing: any;
    public roundName : String;
    public modeOfInterview : String;
    public isClient:boolean;
    public sendFeedbacktoClient:boolean;
    public recruitersFeedBack:string;
    public tblSelectedApplicantsStaffing:any;
}

export class SelectedApplicantsStaffingModel
{
    public selectedApplicantsId :Number
    public applicantRequisitionId : Number;
    public applicantJoining :String;
    public isOnboarded :String;
    public joinedDate :String;
    public clientOnboardingDate :String;
    public createdDate :Date;
    public modifiedDate :Date;
    public createdBy :Date;
    public modifiedBy :Date;
}

export class SelectedApplicantsModel {
    public selectedApplicantsId: Number
    public applicantRequisitionId: Number;
    public applicantJoining: string;
    public joinedDate: string;
    public createdDate: Date;
    public modifiedDate: Date;
    public createdBy: Date;
    public modifiedBy: Date;
}​​​​​​​​

