import { VendorModel } from 'src/app/masters/Models/VendorsModel';

export class ApplicantMasterModel {
    public applicantId: Number;
    public name: String;
    public dob: Date;
    public emailAddress: String;
    public qualification: string;
    public experience: String;
    public relevantExperience: String;
    public currentCtc: Number;
    public expectedCtc: Number;
    public joiningTime: String;
    public skillsandProficiency: String;
    public locationPreference: String;
    public referedBy: Number;
    public jobType: String;
    public status: String;
    public noticePeriod: String;
    public applicantActive: String;
    public source: String
    public passportNo: String;
    public panNo: String;
    public sourceId: Number;
    public employeeEmailId: String;
    public portalId: Number;
    public vendorId: Number;
    public tblvendor: VendorModel[];
    public phoneNumber: Number;
    

}