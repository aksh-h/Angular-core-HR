import { SkillModel } from 'src/app/masters/Models/SkillModel';
import { DepartmentModel } from 'src/app/masters/Models/DepartmentModel';
import { DesignationModel } from 'src/app/masters/Models/DesignationModel';

export class RequisitionModel {
    public requistionId: Number;
    public title: String;
    public departmentId: Number;
    public yearsofExperience: Number;
    public noofPositions: Number;
    public joiningTenure: Number;
    public location: String;
    public comments: String;
    public status: String;
    public managerEmployeeId: Number;
    public managerComments: String;
    public designationId: Number;
    public tblRequisitionSkillMapping: SkillModel[];
    public modifiedDate: Date;
    public createdDate: Date;
    public createdBy: number;
    public modifiedBy: number;
    public isActive: boolean;
    public recruiterComment: boolean;
    public TblRequisitionRecruiterMapping: any;
    public recruiterLeadId: Number;
    public SkillsText: string;
    public department: DepartmentModel;
    public designation: DesignationModel;
    public applicantWorkFlow: String;
    public cancelComments : String;
    public clientId : Number;
    public primarySkills :String;
    public secondarySkills :String;


}

