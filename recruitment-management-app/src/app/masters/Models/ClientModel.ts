import { DateTimeAdapter } from 'ng-pick-datetime';

export class ClientModel {
    public clientId: Number;
    public clientName: String;
    public IsActive: boolean;
    public addressline1: String;
    public addressline2: String;
    public city: number;
    public state: number;
    public country: number;
    public pincode: Number;
    public ndastartDate :Date;
    public ndaendDate :Date;
    public contractStartDate: Date;
    public contractEndDate:Date;
    public typeOfBusiness: String;
    public specialization: String;
    public annualRevenue: Number;
    public jobBoardUrl: String;
    public jobBoardUserId: String;
    public jobBoardPassword: String;
    public website: String;
    public createdBy: Number;
    public modifiedBy: Number;
    public createdDate: Date;
    public modifiedDate: Date;
}
