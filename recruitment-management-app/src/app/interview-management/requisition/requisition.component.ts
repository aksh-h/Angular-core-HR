// import { Component, OnInit } from '@angular/core';
// import { InterviewManagementService } from '../interview-management.service'
// //import { RequisitionModel } from '../Models/RequisitionModel'
// import { CommonHelpers } from '../../shared/CommonHelpers'
// import * as $ from 'jquery'
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { DepartmentModel } from 'src/app/masters/Models/DepartmentModel';
// import { DesignationModel } from 'src/app/masters/Models/DesignationModel';
// import { EmployeeModel } from 'src/app/masters/Models/EmployeeModel';
// import { SkillModel } from 'src/app/masters/Models/SkillModel';
// import { LocalStorageService } from 'angular-web-storage';
// import { Roles as Role } from '../../shared/constants'
// import { RequisitionStatus as RStatus } from '../../shared/constants'
// import { CurrentContext as _context } from '../../shared/constants'
// import { SnackBarService } from 'src/app/shared/snack-bar.service';
// import { NgDialogAnimationService } from "ng-dialog-animation";
// import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
// import { RequisitionModel } from 'src/app/requisition-management/Models/RequisitionModel';



// @Component({
//   selector: 'app-requisition',
//   templateUrl: './requisition.component.html',
//   styleUrls: ['./requisition.component.css']
// })
// export class RequisitionComponent implements OnInit {

//   loader = false;
//   result: string = '';

//   constructor(private service: InterviewManagementService, private formBuilder: FormBuilder, public dialog: NgDialogAnimationService,
//     private storage: LocalStorageService, private _helpers: CommonHelpers, public Snackbar: SnackBarService) { }
    
//   Model = new RequisitionModel();
//   ModelList: RequisitionModel[];
//   DeptList: DepartmentModel[];
//   DesigList: DesignationModel[];
//   SkillList: SkillModel[];
//   EmployeeList: EmployeeModel[];
//   isSubmitted = false;
//   Action = "Requisition";
//   requisitionForm: FormGroup;
//   SkillDropDownSettings = {};
//   EmployeeDropDownSettings = {};
//   readonlycontrols = false;
//   UserID: Number;
//   showDialog : boolean = false;

//   RequisitionTable = [
//     { "sTitle": "Requisition ID", data: "requistionId" },
//     { "sTitle": "Requisition Title", data: "title" },
//     { "sTitle": "Created Date", data: "createdDate" },
//     { "sTitle": "Status", data: "status" },
//     { "sTitle": "Owner", data: "currentOwner" },
//     {
//       "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
//       "render": function (data, type, row, meta) {
//         if (_context.RoleName == Role.ManagerRole || _context.RoleName == Role.AdminRole || _context.RoleName == Role.EmployeeRole) {
//           if (_context.UserID == row.createdBy) {
//             var template = '<div class="btn-group">';
//             if (row.status == RStatus.Open) {
//               template = template + '<button style="width:60px" title = "Edit" id = "' + row.requistionId + '" type = "button" class="btn btn-default btn-actions editRow" > <i class="fa fa-edit"></i></button> ';
//               template = template + '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.requistionId + '" type="button" class="btn btn-default btn-actions deleteRow"><i class="fa fa-trash"></i></button></div>';
//             }
//             else {
//               template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow">View</button></div> ';
//             }
//             return template;
//           }
//           else if (_context.UserID != row.createdBy) {
//             var template = '<div class="btn-group">';
//             if (row.status == RStatus.Open) {
//               template = template + '<button title = "Approve" id = "' + row.requistionId + '" type = "button" class="btn btn-primary approve">Approve </button> ';
//               template = template + '<button style=" margin-left : 13px" title="Decline" id = "' + row.requistionId + '" type="button" class="btn btn-danger decline">Decline</button>';
//               template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow">View</button></div> ';
//             }
//             else {
//               template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow">View</button></div> ';
//             }
//             return template;
//           }
//         }
//         else if (_context.RoleName == Role.RecruiterLeadRole) {
//           var template = '<div class="btn-group">';
//           template = template + '<button  title = "View" id = "' + row.requistionId + '" type = "button" class="btn btn-info viewRow" >View</button> ';
//           if (row.status == RStatus.Approved) {
//             template = template + '<button style="margin-left : 13px" title="Assign" id = "' + row.requistionId + '" type="button" class="btn btn-success assignRow">Assign</button> ';
//           }
//           template = template + '</div>';
//           return template;
//         }
//         else if (_context.RoleName == Role.RecruiterRole) {
//           var template = '<div class="btn-group">';
//           template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow">View</button>';
//           if (row.status == RStatus.AssignedToRecruiter) {

//             template = template + '<button  title = "View" id = "' + row.requistionId + '" type = "button" class="btn btn-info startRow" >Start Working</button>';

//           }
//           template = template + '</div>';
//           return template;
//         }
//         else {
//           return ''
//         }
//       }
//     }
//   ];


//   InitFormControl() {
//     this.requisitionForm = this.formBuilder.group({
//       title: ['', Validators.required],
//       skills: ['', Validators.required],
//       departmentId: ['', Validators.required],
//       designationId: ['', Validators.required],
//       yearsofExperience: ['', Validators.required],
//       joiningTenure: ['', Validators.required],
//       noofPositions: ['', Validators.required],
//       location: [],
//       comments: [],
//       managerComments: []
//     });
//   }


//   ngOnInit(): void {

//     this.HidePopup();
//     this.HideManagerPopup();
//     this.GetAllRequisitions();
//     this.InitFormControl();

//     this.SkillDropDownSettings = {
//       singleSelection: false,
//       idField: 'skillId',
//       textField: 'skillName',
//       selectAllText: 'Select All',
//       unSelectAllText: 'UnSelect All',
//       itemsShowLimit: 5,
//       allowSearchFilter: true,
//       disabled: "disabled"
//     };

//     this.EmployeeDropDownSettings = {
//       singleSelection: false,
//       idField: 'employeeId',
//       textField: 'employeeName',
//       selectAllText: 'Select All',
//       unSelectAllText: 'UnSelect All',
//       itemsShowLimit: 5,
//       allowSearchFilter: true,
//       disabled: "disabled"
//     };
//     this.UserID = parseInt(this.storage.get("userid"));

//     if (_context.RoleName == Role.RecruiterLeadRole || _context.RoleName == Role.RecruiterRole) {
//       this.readonlycontrols = true;
//     }

//   }

//   get f() { return this.requisitionForm.controls; }

//   GetAllRequisitions() {
//     this.loader = true;
//     this.service.GetAll(this.Action).subscribe((res) => {
//       this._helpers.CreateTable("#Requisitiontable", res.RequisitionList, this.RequisitionTable);
//       this.ModelList = res.RequisitionList;
//       this.DeptList = res.DepartmentList;
//       this.DesigList = res.DesignationList;
//       this.SkillList = res.SkillList;
//       this.EmployeeList = res.EmployeeList;
//       this.CalBackFunctions();
//       this.loader = false;
//     });
//   }

//   CalBackFunctions() {
//     $(".editRow").click((element) => {
//       var requisitionobj: any;
//       requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
//       this.Model = requisitionobj;
//       let p = 0;
//       this.Model.tblRequisitionSkillMapping.forEach(function (element) {
//         element.skillName = requisitionobj.tblRequisitionSkillMapping[p].skill.skillName;
//         p = p + 1;
//       });
//       if (this.Model.createdBy != this.UserID) {
//         this.readonlycontrols = true;
//       }
//       this.ShowPopup();
//     });

//     $('.deleteRow').click((element) => {
//       this.confirmDialog(element);      
//     });

//     $(".approve").click((element) => {
//       var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
//       this.Model = requisitionobj;
//       this.Model.status = RStatus.Approved;
//       this.ShowManagerPopup();
//     });

//     $('.decline').click((element) => {
//       var requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
//       this.Model = requisitionobj;
//       this.Model.status = RStatus.Declined;
//       this.ShowManagerPopup();
//     });

//     $(".viewRow").click((element) => {
//       var requisitionobj: any;
//       requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
//       this.Model = requisitionobj;
//       let p = 0;
//       this.Model.tblRequisitionSkillMapping.forEach(function (element) {
//         element.skillName = requisitionobj.tblRequisitionSkillMapping[p].skill.skillName;
//         p = p + 1;
//       });
//       this.readonlycontrols = true;
//       this.ShowPopup();
//     });

//     $(".assignRow").click((element) => {
//       var requisitionobj: any;
//       requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
//       this.Model = requisitionobj;
//       this.ShowRecruiterPopup();
//     });

//     $(".startRow").click((element) => {
//       var requisitionobj: any;
//       requisitionobj = this.ModelList.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
//       this.Model = requisitionobj;
//       this.Model.status = RStatus.InProgress;
//       this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
//         if (res > 0) {
//           this.Refresh();
//         }
//         else if (res == "-1") {
//         }
//         else {
//         }
//         this.loader = false;
//       });

//     });

//   }

//   ShowPopup() {
//     $('#myModal').show();
//     $('#index').hide();
//   }

//   HidePopup() {
//     $('#myModal').hide();
//     $('#index').show();
//     this.Model = new RequisitionModel();
//     this.isSubmitted = false;
  
//   }

//   onSubmit() {
//     debugger;
//     this.isSubmitted = true;
//     if (!this.requisitionForm.invalid) {
//       this.loader = true;
//       this.Model.departmentId = parseInt(this.requisitionForm.value.departmentId);
//       this.Model.designationId = parseInt(this.requisitionForm.value.designationId);
//       if (this.Model.requistionId > 0) {
//         this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
//           if (res > 0) {
//             this.Refresh();
//             this.Snackbar.open('Updated Successfully..!');
//           }
//           else if (res == "-1") {
//           }
//           else {
//           }
//           this.loader = false;
//         });
//       }

//       else {
//         this.service.Post(this.Action, this.Model).subscribe((res) => {
//           if (res > 0) {
//             this.Refresh();
//             this.Snackbar.open('Added Successfully..!');
//           }
//           else if (res == "-1") {

//           }
//           else {

//           }
//           this.loader = false;
//         });
//       }
//     }

//   }

//   Refresh() {
//     this.GetAllRequisitions();
//     this.HidePopup();
//     this.HideManagerPopup();
//     this.HideRecruiterPopup();
//     this.isSubmitted = false;
//     this.Model = new RequisitionModel();

//   }

//   HideManagerPopup() {
//     $('#ManagerModal').hide();
//     this.isSubmitted = false;
//   }

//   ShowManagerPopup() {
//     $('#ManagerModal').show();
//   }

//   OnManagerSubmit() {
//     this.isSubmitted = true;
//     if (this.Model.managerComments) {
//       this.loader = true;
//       this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
//         if (res > 0) {
//           this.Refresh();
//           this.Snackbar.open('Updated Successfully..!');
//         }
//         else if (res == "-1") {
//         }
//         else {
//         }
//         this.loader = false;
//       });
//     }
//   }


//   HideRecruiterPopup() {
//     $('#RecruiterModal').hide();
//     this.isSubmitted = false;
//   }

//   ShowRecruiterPopup() {
//     $('#RecruiterModal').show();
//   }

//   OnRecruiterSubmit() {
//     this.isSubmitted = true;
//     this.Model.status = RStatus.AssignedToRecruiter;
//     this.Model.recruiterLeadId = _context.UserID;
//     if (this.Model.recruiterComment) {
//       this.loader = true;
//       this.service.Put("Update" + this.Action, this.Model).subscribe((res) => {
//         if (res > 0) {
//           this.Refresh();
//           this.Snackbar.open('Updated Successfully..!');
//         }
//         else if (res == "-1") {
//         }
//         else {
//         }
//         this.loader = false;
//       });
//     }
//   }


//   confirmDialog(element): void {
//     const message = `Are you sure you want to do this?`;

//     const dialogData = new ConfirmDialogModel("Confirm Action", message);

//     const dialogRef = this.dialog.open(ConfirmationDialogComponent, {     
//       animation:{to:"aside"},
//       data: dialogData,
//       autoFocus: true,
     
//     });

//     dialogRef.afterClosed().subscribe(dialogResult => {
//       this.result = dialogResult;
//       if (this.result) {   
//         this.loader = true;
//         this.service.Delete(this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
//           if (res > 0) {
//             this.GetAllRequisitions();
//             this.Snackbar.open('Deleted Successfully..!');
//           }
//           else {
//           }
//           this.loader = false;
//         })
//       }
//       else {
//       }
//     });
//   }


// }
