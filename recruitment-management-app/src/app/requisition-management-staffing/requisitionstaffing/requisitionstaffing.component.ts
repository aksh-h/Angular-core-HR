import { Component, OnInit } from '@angular/core';
// import { RequisitionstaffingModel } from '../Models-staffing/RequisitionstaffingModel';
import { DepartmentModel } from 'src/app/masters/Models/DepartmentModel';
import { DesignationModel } from 'src/app/masters/Models/DesignationModel';
import { EmployeeModel } from 'src/app/masters/Models/EmployeeModel';
import { SkillModel } from 'src/app/masters/Models/SkillModel';
import { Router } from '@angular/router';
import { LocalStorageService } from 'angular-web-storage';
import { RequisitionManagementStaffingService } from 'src/app/requisition-management-staffing/requistion-management-staffing.service';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { ViewDialogComponent } from 'src/app/shared/view-requisition-dialog/view-dialog.component';
import { ConfirmationDialogComponent, ConfirmDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { ViewRequisitionApplicantsComponent } from 'src/app/requisition-management/view-requisition-applicants/view-requisition-applicants.component';
import { ProgressbarComponent } from 'src/app/shared/progressbar/progressbar.component';
import * as $ from 'jquery'
import { RequisitionStatus as RStatus } from '../../shared/constants'
import { CurrentContext as _context } from '../../shared/constants'
import {Roles as Role,RequisitionStatus} from '../../shared/constants'
import { RequisitionstaffingModel } from '../Models/RequisitionStaffingModel';
import { ViewRequistionApplicantsStaffingComponent } from '../view-requistion-applicants-staffing/view-requistion-applicants-staffing.component';
import { ClientscontactdetailsModel } from 'src/app/masters/Models/ClientcontactdetailsModel';
@Component({
  selector: 'app-requisitionstaffing',
  templateUrl: './requisitionstaffing.component.html',
  styleUrls: ['./requisitionstaffing.component.css']
})
export class RequisitionStaffingComponent implements OnInit {
  loader = false;
  result: string = '';
  
  constructor(private router: Router, private service: RequisitionManagementStaffingService, private formBuilder: FormBuilder, public dialog: NgDialogAnimationService,
    private storage: LocalStorageService, private _helpers: CommonHelpers, public Snackbar: SnackBarService) { }
  Model = new RequisitionstaffingModel();
  ModelList: RequisitionstaffingModel[];
  ConstModelList: RequisitionstaffingModel[];
  DeptList: DepartmentModel[];
  DesigList: DesignationModel[];
  SkillList: SkillModel[];
  EmployeeList: EmployeeModel[];
  WorkFlowList: any;
  ClientsList : any;
  isSubmitted = false;
  Action = "RequisitionStaffing";
  requisitionForm: FormGroup;
  SkillDropDownSettings = {};
  ContactDropDownSettings = {};
  EmployeeDropDownSettings = {};
  readonlycontrols = false;
  UserID: Number;
  showDialog: boolean = false;
  role : String;
  recruiterrole : String;
  recruiter : String;
  staffingrole: String;
  staffing : String;
  ContactsList :ClientscontactdetailsModel[];
  RequisitionTable = [
    { "sTitle": "Requisition ID", data: "requistionId" },
    { "sTitle": "Requisition Title", data: "title" },
    {
      "sTitle": "Created Date", "render": function (data, type, row, meta) {
        return new Date(row.createdDate).toLocaleDateString() + " " + new Date(row.createdDate).toLocaleTimeString()
      }
    },
    { "sTitle": "Status", data: "status" },
    { "sTitle": "Owner", data: "currentOwner" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        if (_context.RoleName == Role.StaffingLeadRole ||_context.RoleName == Role.SalesHead ) {
          if (_context.UserID == row.createdBy) {
            var template = '<div class="btn-group">';
            if (row.status == RStatus.Open) {
              template = template + '<button style="width:60px; margin-left : 13px" title = "Edit" id = "' + row.requistionId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ';
              template = template + '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.requistionId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div>';
            }
            else {
              template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow"><i class="fa fa-eye"></i></button>';
              if (row.status != RStatus.Closed && row.status != RStatus.Cancelled && row.status != RStatus.CancelRequested) {
                template = template + '<button style=" margin-left : 13px" title="Decline" id = "' + row.requistionId + '" type="button" class="btn btn-danger withdraw"><i class="fa fa-close"></i></button>';
                template = template + '<button style=" margin-left : 10px"  title = "Search Applicants" id = "' + row.requistionId + '" type = "button" class="btn btn-primary searchapplicantsRow" "><i class="fa fa-search"></i></button>';    
                if (row.status == RStatus.Approved){
                template = template + '<button style="margin-left : 13px" title="Assign" id = "' + row.requistionId + '" type="button" class="btn btn-success assignRow"><i class="fa fa-users "></i></button> ';
                template = template + '<button style="margin-left : 13px" title="Applicant Status" id = "' + row.requistionId + '" type="button" class="btn btn-success statusRow"><i class="fa fa-info-circle"></i></button> ';

              }
                if (row.status == RStatus.InProgress) {
                 template = template + '<button style="margin-left : 13px" title="Applicant Status" id = "' + row.requistionId + '" type="button" class="btn btn-success statusRow"><i class="fa fa-info-circle"></i></button> ';
                
              }
              }
            }
            template = template + '</div>'
            return template;
          }
          else if (_context.UserID != row.createdBy) {
            var template = '<div class="btn-group">';
            if (row.status == RStatus.Open) {
              template = template + '<button style=" margin-left : 13px" title = "Approve" id = "' + row.requistionId + '" type = "button" class="btn btn-primary approve"><i class="fa fa-fw"></i></button> ';
              template = template + '<button style=" margin-left : 13px" title="Decline" id = "' + row.requistionId + '" type="button" class="btn btn-danger decline"><i class="fa fa-close"></i></button>';
              template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow"><i class="fa fa-eye"></i></button></div> ';
            }
            else {
              template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow"><i class="fa fa-eye"></i></button>';
              if (row.status == RStatus.CancelRequested) {
                template = template + '<button title = "ApproveCancel" id = "' + row.requistionId + '" type = "button" style=" margin-left : 10px" class="btn btn-primary approvecancel"><i class="fa fa-times"</i></button> ';
              }
            }
            template = template + '</div>'
            return template;
          }
        }      
        else if (_context.RoleName == Role.StaffingLeadRole) {
          var template = '<div class="btn-group">';         
          if (row.status == RStatus.Open) {
            template = template + '<button style="width:60px; margin-left : 13px" title = "Edit" id = "' + row.requistionId + '" type = "button" class="btn btn-default btn-actions editRow" > <i class="fa fa-edit"></i></button> ';
            template = template + '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.requistionId + '" type="button" class="btn btn-default btn-actions deleteRow"><i class="fa fa-trash"></i></button>';       
          }
          if (row.status == RStatus.Approved) {
            template = template + '<button  title = "View" id = "' + row.requistionId + '" type = "button" class="btn btn-info viewRow" ><i class="fa fa-eye"></button> ';
            template = template + '<button style="margin-left : 13px" title="Applicant Status" id = "' + row.requistionId + '" type="button" class="btn btn-success statusRow"><i class="fa fa-info-circle"></i></button> ';
          }
          if (row.status == RStatus.InProgress) {
             template = template + '<button  title = "View" id = "' + row.requistionId + '" type = "button" class="btn btn-info viewRow" ><i class="fa fa-eye"></i></button> ';
            template = template + '<button style="margin-left : 13px" title="Applicant Status" id = "' + row.requistionId + '" type="button" class="btn btn-success statusRow"><i class="fa fa-info-circle"></button> ';
          } 
          else
          {
            template = template + '<button  title = "View" id = "' + row.requistionId + '" type = "button" class="btn btn-info viewRow" ><i class="fa fa-eye"></i></button> ';
          }        
          template = template + '</div>';
          return template;
        }  
        else if (_context.RoleName == Role.StaffingRole) {
          var template = '<div class="btn-group">';
          template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow"><i class="fa fa-eye"></i></button>';
          if (row.status == RStatus.AssignedToRecruiter) {
            template = template + '<button style=" margin-left : 10px"  title = "Start Working" id = "' + row.requistionId + '" type = "button" class="btn btn-primary startRow" ><i class="fa fa-hourglass-start" aria-hidden="true"></i></button>';
          }
          else if (row.status == RStatus.InProgress) {
            template = template + '<button style=" margin-left : 10px"  title = "Search Applicants" id = "' + row.requistionId + '" type = "button" class="btn btn-primary searchapplicantsRow" ><i class="fa fa-search"></i></button>';           
          }
          template = template + '</div>';
          return template;
        }
        else {
          return ''
        }
      }
    }
  ];
  InitFormControl() {
    this.requisitionForm = this.formBuilder.group({
      title: ['', Validators.required],
      skills: [''],
      departmentId: ['', Validators.required],
      designationId: ['', Validators.required],
      yearsofExperience: ['', Validators.required],
      joiningTenure: ['', Validators.required],
      noofPositions: ['', Validators.required],
      location: [],
      comments: [],
      managerComments: [],
      workflow: ['', Validators.required],
      clientId : ['', Validators.required],
      contactId :[''],
      primarySkills :['', Validators.required],
      secondarySkills :[''],
      contractPeriod :['', Validators.required],
      budget :['', Validators.required],
      tier : ['']
    });
  }
  ngOnInit(): void {
    this.HidePopup();
    this.HideManagerPopup();
    this.GetAllRequisitions();
    this.InitFormControl();
    window.scrollTo(0, 0);
    this.SkillDropDownSettings = {
      singleSelection: false,
      idField: 'skillId',
      textField: 'skillName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      disabled: "disabled"
    };
    this.ContactDropDownSettings = {
      singleSelection: false,
      idField: 'contactId',
      textField: 'contactPerson',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      disabled: "disabled"
    };
   
    this.EmployeeDropDownSettings = {
      singleSelection: false,
      idField: 'employeeId',
      textField: 'employeeName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      disabled: "disabled"
    };
    this.UserID = parseInt(this.storage.get("userid"));
    if (_context.RoleName == Role.StaffingRole) {
      this.readonlycontrols = true;
    }
    this.role = _context.RoleName;
    this.staffingrole = Role.StaffingLeadRole;
    this.staffing = Role.StaffingRole;
  }
  get f() { return this.requisitionForm.controls; }
  ClientContact()
  {
    this.service.GetByCriteria("ClientsContact",parseInt(this.Model.clientId.toString())).subscribe((res) => {
            this.ContactsList = res;
    });
  }
  
  GetAllRequisitions() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res.RequisitionList;
      this._helpers.CreateTable("#Requisitiontable", this.ModelList, this.RequisitionTable);   
      this.DeptList = res.DepartmentList;
      this.DesigList = res.DesignationList;
      this.SkillList = res.SkillList;
      this.EmployeeList = res.EmployeeList;
      this.WorkFlowList = res.WorkFlows;
      this.WorkFlowList.forEach(element => {
        element.workFlowJson = JSON.parse(element.workFlowJson);
      });
      this.ClientsList = res.Clients;
      this.ConstModelList = res.RequisitionList;
      this.CalBackFunctions();
      this.loader = false;
    });
  }
  CalBackFunctions() {
    $(".editRow").click((element) => {
      var requisitionobj: any;
      requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      let p = 0;
      this.Model.tblRequisitionSkillMappingStaffing.forEach(function (element) {
        element.skillName = requisitionobj.tblRequisitionSkillMapping[p].skill.skillName;
        p = p + 1;
      });
      if (this.Model.createdBy != this.UserID) {
        this.readonlycontrols = true;
      }
      this.ShowPopup();
    });
    
    $('.deleteRow').click((element) => {
      this.confirmDialog(element);
    });
    $(".approve").click((element) => {
      var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      this.Model.status = RStatus.Approved;
      this.ShowManagerPopup();
    });
    $('.decline').click((element) => {
      
      var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      this.Model.status = RStatus.Declined;
      this.ShowCancelPopup();
    });
    $(".viewRow").click((element) => {
      var requisitionobj: any;
      requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      let p = 0;
      this.Model.tblRequisitionSkillMappingStaffing.forEach(function (element) {
        element.skillName = requisitionobj.tblRequisitionSkillMappingStaffing[p].skill.skillName;
        p = p + 1;
      });
      this.Model.SkillsText = this.Model.tblRequisitionSkillMappingStaffing.map(function (val) { return val.skillName }).join(',')
      const dialogRef = this.dialog.open(ViewDialogComponent, {
        animation: { to: "left" },
        data: this.Model,
        autoFocus: true,
        width: "800px"
      });
    });
    
    $(".assignRow").click((element) => {
      var requisitionobj: any;
      requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      this.ShowRecruiterPopup();
    });
    $(".startRow").click((element) => {
      var requisitionobj: any;
      requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      this.Model.status = RStatus.InProgress;
      this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
        if (res > 0) {
          this.Refresh();
        }
        else if (res == "-1") {
        }
        else {
        }
        this.loader = false;
      });
    });
    $(".searchapplicantsRow").click((element) => {
      var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.router.navigate(["/applicants/dashboardstaffing/Requisition-" + requisitionobj.requistionId]);
    });
    $(".withdraw").click((element) => {
      var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      this.Model.status = RStatus.Approved;
      this.ShowCancelPopup();
    });
    $(".approvecancel").click((element) => {
      var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.Model = requisitionobj;
      const message = `Are you sure you want to do this?`;
      const dialogData = new ConfirmDialogModel("Confirm Action", message);
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        animation: { to: "top" },
        data: dialogData,
        autoFocus: true,
      });
      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.Model.status = RStatus.Cancelled;
          this.loader = true;
          this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
            if (res > 0) {
              this.Refresh();
              this.Snackbar.open('Requisition Cancelled Successfully..!');
            }
            else if (res == "-1") {
            }
            else {
            }
            this.loader = false;
          });
        }
      });
    });
    $(".statusRow").click((element) => {
      this.Model = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.service.GetByCriteria("GetApplicants" + this.Action ,parseInt(element.currentTarget.id)).subscribe((res) => {
        const dialogRef = this.dialog.open(ViewRequistionApplicantsStaffingComponent, {
          animation: { to: "left" },
          data: res,
          autoFocus: true,
          width:"540px",
          height:"550px"     
        });
        dialogRef.afterClosed().subscribe(dialogResult => {         
          if (dialogResult) {
            this.Model.status = RequisitionStatus.Closed;
            this.loader = true;
            this.service.Put("Close" + this.Action, this.Model).subscribe((res) => {
              if (res > 0) {
                this.Refresh();
                this.Snackbar.open('Requisition Closed Successfully..!');
              }             
              this.loader = false;
            });
          }
         
        });
      });
    });
  }
  ShowPopup() {
    $('#myModal').show();
    $('#index').hide();
  }
  HidePopup() {
    $('#myModal').hide();
    $('#index').show();
    this.Model = new RequisitionstaffingModel();
    this.isSubmitted = false;
    window.scroll(0, 0);
  }
  onSubmit() {
    
    this.isSubmitted = true;
    if (!this.requisitionForm.invalid) {
      this.loader = true;
      this.Model.departmentId = parseInt(this.requisitionForm.value.departmentId);
      this.Model.designationId = parseInt(this.requisitionForm.value.designationId);    
      if (this.Model.requistionId > 0) {
        this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
          if (res > 0) {
            this.Refresh();
            this.Snackbar.open('Updated Successfully..!');
          }
          else if (res == "-1") {
          }
          else {
          }
          this.loader = false;
        });
      }
      else {
        this.service.Post(this.Action, this.Model).subscribe((res) => {
          if (res > 0) {
            this.Refresh();
            this.Snackbar.open('Added Successfully..!');
          }
          else if (res == "-1") {
          }
          else {
          }
          this.loader = false;
        });
      }
    }
  }
  Refresh() {
    this.GetAllRequisitions();
    this.HidePopup();
    this.HideManagerPopup();
    this.HideRecruiterPopup();
    this.HideCancelPopup();
    this.isSubmitted = false;
    this.Model = new RequisitionstaffingModel();
    window.scrollTo(0, 0);
  }
  HideManagerPopup() {
    $('#ManagerModal').hide();
    this.isSubmitted = false;
  }
  ShowManagerPopup() {
    $('#ManagerModal').show();
  }
  OnManagerSubmit() {
    this.isSubmitted = true;
    if (this.Model.managerComments) {
      this.loader = true;
      this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
        if (res > 0) {
          this.Refresh();
          this.Snackbar.open('Updated Successfully..!');
        }
        else if (res == "-1") {
        }
        else {
        }
        this.loader = false;
      });
    }
  }
  HideRecruiterPopup() {
    $('#RecruiterModal').hide();
    this.isSubmitted = false;
  }
  ShowRecruiterPopup() {
    $('#RecruiterModal').show();
  }
  OnRecruiterSubmit() {
    this.isSubmitted = true;
    this.Model.status = RStatus.AssignedToRecruiter;
    this.Model.recruiterLeadId = _context.UserID;
    if (this.Model.recruiterComment) {
      this.loader = true;
      this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
        if (res > 0) {
          this.Refresh();
          this.Snackbar.open('Updated Successfully..!');
        }
        else if (res == "-1") {
        }
        else {
        }
        this.loader = false;
      });
    }
  }
  confirmDialog(element): void {
    const message = `Are you sure you want to do this?`;
    const dialogData = new ConfirmDialogModel("Confirm Action", message);
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      animation: { to: "top" },
      data: dialogData,
      autoFocus: true,
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result) {
        this.loader = true;
        this.service.Delete("Delete" + this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
          if (res > 0) {
            this.GetAllRequisitions();
            this.Snackbar.open('Deleted Successfully..!');
          }
          else {
          }
          this.loader = false;
        })
      }
      else {
      }
    });
  }
  OpenWorkFlow() {
    const dialogRef = this.dialog.open(ProgressbarComponent, {
      animation: { to: "top" },
      data: this.WorkFlowList,
      autoFocus: true,
      width: "50%"
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.Model.applicantWorkFlow = dialogResult;
      }
    });
  }
  HideCancelPopup() {
    $('#CancelModal').hide();
    this.isSubmitted = false;
  }
  ShowCancelPopup() {
    $('#CancelModal').show();
  }
  OnCancelSubmit() {
    this.isSubmitted = true;
    if (this.Model.cancelComments) {
      this.Model.status = RStatus.CancelRequested;
      this.loader = true;
      this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
        if (res > 0) {
          this.Refresh();
          this.Snackbar.open('Cancel Request Submitted Successfully..!');
        }
        else if (res == "-1") {
        }
        else {
        }
        this.loader = false;
      });
    }
  }
  radioChange(event)
  {
    if (event.value == "Client")
    {
    this.ModelList =  this.ConstModelList.filter(r => r.clientId > 0);
    this._helpers.CreateTable("#Requisitiontable", this.ModelList, this.RequisitionTable);
    }
    else
    {
      this.ModelList =  this.ConstModelList.filter(r => r.clientId == null || r.clientId < 1);
      this._helpers.CreateTable("#Requisitiontable", this.ModelList, this.RequisitionTable);
    }
  }
}