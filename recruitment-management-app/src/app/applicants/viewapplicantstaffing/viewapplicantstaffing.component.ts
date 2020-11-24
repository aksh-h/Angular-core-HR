import { Component, OnInit, ViewChild } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicantStaffingService } from '../applicantstaffing.service';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from 'src/app/shared/api.service';
import { InterviewStatus } from 'src/app/shared/constants';
import { InterviewManagementModel, InterviewManagementStaffingModel, SelectedApplicantsStaffingModel } from '../Models/InterviewManagement';
import { PanelGroupModel } from 'src/app/masters/Models/PanelGroupModel';
import { GoogleMapsComponent } from 'src/app/shared/google-maps/google-maps.component';
import { InterviewFinishDialogComponent } from 'src/app/employee/interview-finish-dialog/interview-finish-dialog';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-viewapplicantstaffing',
  templateUrl: './viewapplicantstaffing.component.html',
  styleUrls: ['./viewapplicantstaffing.component.css']
})
export class ViewapplicantstaffingComponent implements OnInit {

  panelOpenState = false;
  Addresses;
   feed=false;

  @ViewChild(MatAccordion) accordion: MatAccordion;
  isinterviewScheduled: boolean;
  constructor(private dialogAnimation: NgDialogAnimationService, private formBuilder: FormBuilder, private activeRoute: ActivatedRoute, private router: Router, private service: ApplicantStaffingService, private _helpers: CommonHelpers, public Snackbar: SnackBarService, public dialog: MatDialog, private apiService: ApiService) { }
  
  ModelList:any;
  Model:any;

  ApplicantsTable = [

    { "sTitle": "Panel Group", data: "interviewPanel" },
    {
      "sTitle": "Interviewer", "render": function (data, type, row, meta) {
        if (row.tblInterviewEmployeeMappingStaffing[0].employee == undefined) {
          return row.tblInterviewEmployeeMappingStaffing.map(r => r.employeeName).join(',');
        }
        else {
          return row.tblInterviewEmployeeMappingStaffing.map(r => r.employee.employeeName).join(',');
        }
      }
    },
    {
      "sTitle": "Interview Date", "render": function (data, type, row, meta) {
        return new Date(row.interviewDate).toLocaleDateString() + " " + new Date(row.interviewDate).toLocaleTimeString()
      }
    },
    {

      "sTitle": "Interview Status", "render": function (data, type, row, meta) {

        if (row.status == InterviewStatus.Selected) {
          return '<p style="color: darkgreen;">Selected</p>';
        }
        else if (row.status == InterviewStatus.InterviewScheduled) {
          return '<p style="color: darkmagenta;">Interview Scheduled</p>';
        }
        else {
          return '<p style="color: red;">' + row.status + '</p>';
        }
      }
    },
    {
      "sTitle": "Interviewer Comments", "render": function (data, type, row, meta) {
        debugger;
        return row.feedBack + '  ' + '<div id = "' + row.interviewId + '" class="fa-hover info pull-right"><a style="cursor: pointer;"><i class="fa fa-navicon"></i></a></div>'
      }
    },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        var template = '<div class="btn-group">';
        if (row.status == InterviewStatus.InterviewScheduled) {
          template = template + '<button style=" margin-left : 13px" title = "Approve" id = "' + row.interviewId + '" type = "button" class="btn btn-info reshedule">Reschedule</button> ';
          template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.interviewId + '" type="button" class="btn btn-danger cancel">Cancel</button></div> ';
        }
        else {
          template = template + '<div style=" margin-left : 13px">N/A'
        }
        template = template + '</div>'
        return template;;

      }
    }

  ]
CurrentRoundName:string;
  loader = false;
  Action = "Applicant";
  Action1="ApplicantJoining";
  
  id: Number = this.activeRoute.snapshot.params.id
  from: String = this.activeRoute.snapshot.params.from
  InterviewModelList: InterviewManagementStaffingModel[];
  ApplicantModel: any;
  InterviewModel: InterviewManagementStaffingModel;
  InterviewForm: FormGroup;
  readonlycontrols: boolean = false;
  readonlyFinalcontrols: boolean = false;
  readonlySelectedcontrols:boolean = false; 
  isSubmitted: boolean = false;
  InterviewerList: any;
  InterviewerListAll: any;
  PanelList: PanelGroupModel[];
  InterviewerDropDownSettings = {};
  status: String;
  message: String;
  finalmessage: String;
  ApplicantRequisition: any;
  Rounds: any;
  RejectedIndex = -1;
  InterviewCount = 0;
  reschedule = false;
  readonlyFinalcontrols2 = true;
  SelectedApplicantsModel:SelectedApplicantsStaffingModel;
  SelectedForm:FormGroup;
  SalaryTemplates: any;
  SelectedSalaryTemplate: any;
  SelectedSalaryFormulas: any;


  ngOnInit(): void {

    this.InterviewModel = new InterviewManagementStaffingModel();
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
    this.InterviewForm = this.formBuilder.group({
      panel: ['', Validators.required],
      interviewer: ['', Validators.required],
      interviewDate: ['', Validators.required],
      comments: [''],
      venue: [''],
      moi : [''],
      sendFeedbacktoClient:[''],
    });
    this.SelectedApplicantsModel = new SelectedApplicantsStaffingModel();
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
    this.SelectedForm = this.formBuilder.group({
      applicantJoining: ['', Validators.required],
      joinedDate: ['', Validators.required],
      isOnboarded: ['', Validators.required],
      clientOnboardingDate: [''],
      
    }); 
    this.InterviewerDropDownSettings = {
      singleSelection: false,
      idField: 'employeeId',
      textField: 'employeeName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
      allowSearchFilter: true
    };

    this.GetApplicantDetails();
    this.GetSelectedApplicants();

  }

  get f() { return this.InterviewForm.controls; }
  get g() { return this.SelectedForm.controls; }


  GetApplicantDetails() {
    this.loader = true;
    this.service.GetByCriteria("ShortListed" + this.Action, this.id).subscribe((res) => {

      this._helpers.CreateTable("#Interviewtable", res.InterviewDetails, this.ApplicantsTable, false, false);
      this.SalaryTemplates = res.SalaryTemplates;

      ///Need to remove Manjunath 
      this.SelectedSalaryTemplate = JSON.parse(this.SalaryTemplates[0].templateJson);
      this.SelectedSalaryFormulas = JSON.parse(this.SalaryTemplates[0].formalsJson);

      ///////////////
      this.InterviewModelList = res.InterviewDetails;
      this.ApplicantRequisition = res.ApplicantDetails;
      this.ApplicantModel = res.ApplicantDetails.applicant;
      this.status = res.ApplicantDetails.status;
      if (this.status == InterviewStatus.NotAssigned) {
        this.status = "ShortListed";
      }
      this.InterviewerListAll = res.InterviewerList;
      this.Rounds = res.ApplicantDetails.requisition.applicantWorkFlow.split(':')[1].split(',');
      for (var i = 0; this.InterviewerListAll.length > i; i++) {
        this.InterviewerListAll[i].employeeName = this.InterviewerListAll[i].employee.employeeName;
      }
      this.PanelList = res.PanelList;
      this.readonlyFinalcontrols = false;
      this.readonlycontrols = false;
      this.InterviewCount = this.InterviewModelList.filter(o => o.status != InterviewStatus.Cancelled && o.status != InterviewStatus.RescheduleRequested).length;
      this.AssigStatus();
      this.CalBackFunctions();
      this.reschedule = false;
      this.loader = false;
      this.InterviewModel.modeOfInterview = "Telephonic"
      this.CurrentRoundName=this.Rounds[this.InterviewCount];
     // this.InterviewModel.sendFeedbacktoClient=true;
    });
  }

 EnableClientFeedBack= false;

 GetSelectedApplicants()
 {
   debugger;
   this.loader = true;
     this.service.GetByCriteria("GetApplicantJoiningDetails" , this.id).subscribe((res) => {

     this.SelectedApplicantsModel = res.result;
     var now = new Date(res.result.clientOnboardingDate);
     var day = ("0" + now.getDate()).slice(-2);
     var month = ("0" + (now.getMonth() + 1)).slice(-2);
     this.SelectedApplicantsModel.clientOnboardingDate = now.getFullYear()+"-"+(month)+"-"+(day) ;

     now = new Date(res.result.joinedDate);
     day = ("0" + now.getDate()).slice(-2);
     month = ("0" + (now.getMonth() + 1)).slice(-2);
     this.SelectedApplicantsModel.joinedDate = now.getFullYear()+"-"+(month)+"-"+(day) ;

     this.loader = false;
   });
   }

  AssigStatus() {

    if (this.status == InterviewStatus.Selected) {
      this.readonlyFinalcontrols = true;
      this.readonlycontrols = true;
      this.finalmessage = 'Applicant has already been Selected.';
      this.message = this.finalmessage;
    }
    else if (this.status == InterviewStatus.Rejected) {
      this.readonlyFinalcontrols = true;
      this.readonlycontrols = true;
      this.finalmessage = 'Applicant has already been Rejected.';
      this.message = this.finalmessage;
    }
    else if (this.status == InterviewStatus.BlackListed) {
      this.readonlyFinalcontrols = true;
      this.readonlycontrols = true;
      this.finalmessage = 'Applicant has been BlackListed.';
      this.message = this.finalmessage;
    }
    else if (this.status == "ShortListed" || this.status.includes(InterviewStatus.Selected) || this.status.includes(InterviewStatus.Cancelled) || this.status.includes(InterviewStatus.RescheduleRequested)) {
      this.readonlycontrols = false;
      if (this.Rounds.length != this.InterviewCount) {
        this.readonlyFinalcontrols = true;
        this.finalmessage = 'You Cannot Select/Reject Untill All Rounds are Completed';
      }
      else if (this.Rounds.length == this.InterviewCount) {
        this.readonlycontrols = true;
        this.message = "Applicant has cleared all the Rounds."
        //this.setStep(1) ;
        if(this.Rounds[this.InterviewCount -1]=="Client")
        {
          this.EnableClientFeedBack = true;
        }
       
      }
    }
    else {
      this.readonlycontrols = true;
      this.isinterviewScheduled = false;
      if (this.status.includes(InterviewStatus.BlackListed))
        this.message = 'Applicant has been BlackListed. You Cannot Assign Any Interviews for this Requisition.';
      else if (this.status.includes(InterviewStatus.InterviewScheduled)) {
        this.isinterviewScheduled = true;
        this.InterviewCount = this.InterviewCount - 1;
        this.message = 'You Cannot Assign Interview untill Assigned Interview is completed.';
        if (this.Rounds.length != this.InterviewModelList.length) {
          this.readonlyFinalcontrols = true;
          this.finalmessage = 'You Cannot Select/Reject Untill All Rounds are Completed';
        }
      }
      else if (this.status.includes(InterviewStatus.Rejected)) {
        this.message = 'Applicant has been Rejected. You Cannot Assign Any Interviews for this Requisition.';
        this.RejectedIndex = this.InterviewCount - 1;
      }

    }
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.InterviewForm.valid || this.CurrentRoundName =='Client') {
      debugger;
      this.InterviewModel.interviewDate = new Date((this.InterviewModel.interviewDate).toString()).toLocaleDateString() + " " + new Date((this.InterviewModel.interviewDate).toString()).toLocaleTimeString();
      this.InterviewModel.applicantId = this.ApplicantModel.applicantId;
      this.InterviewModel.applicantRequisitionId = this.id;
      this.loader = true;
      this.InterviewModel.status = this.Rounds[this.InterviewCount];
      this.InterviewModel.roundName = this.Rounds[this.InterviewCount];
      this.InterviewModel.sendFeedbacktoClient=this.feed;
      var action = "AddInterview"
      if (this.reschedule) {
        this.InterviewModel.isCompleted = false;
        action = "UpdateInterview"
      }
      this.InterviewModel.interviewDate = new Date((this.InterviewModel.interviewDate).toString()).toLocaleDateString() + " " + new Date((this.InterviewModel.interviewDate).toString()).toLocaleTimeString();
      this.service.Post(action + this.Action, this.InterviewModel).subscribe((res) => {
        if (res > 0) {
          this.loader = false;
          this.Snackbar.open('Interview Scheduled Successfully..!');
          this.InterviewModel = new InterviewManagementStaffingModel();
          this.GetApplicantDetails();
          this.isSubmitted = false;
          window.scroll({
            top: 0,
            left: 0,
            behavior: 'smooth'
          });
        }
      });
    }
  }

  FilterInterviewers(ischange) {
    if (ischange) {
      this.InterviewModel.tblInterviewEmployeeMappingStaffing = [];
    }
    var panelid = this.PanelList.filter(o => o.panelGroupName == this.InterviewModel.interviewPanel)[0].panelGroupId;
    this.InterviewerList = this.InterviewerListAll.filter(o => o.panelGroupId == panelid);
  }

  OpenGoogleMap() {
    if (!this.readonlycontrols) {
      window.scroll({
        top: 0,
        behavior: 'smooth'
      });

      const dialogRef = this.dialog.open(GoogleMapsComponent, {
        autoFocus: true,
        // width: "800px",
        // height: "600px"
      });

      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.InterviewModel.venue = dialogResult.address + " - " + dialogResult.link;
        }
      });
    }
  }

  ChangeApplicantStatus() {
    this.isSubmitted = true;
    if (!this.ApplicantRequisition.recruiterComment)
      return;
    if (this.ApplicantRequisition.status == "Selected") {
      if (!this.ApplicantRequisition.currentCtc || !this.ApplicantRequisition.expectedCtc || !this.ApplicantRequisition.negotiatedCtc || !this.ApplicantRequisition.tentativeJoiningDate) {
        return;
      }
    }

    if (!this.readonlyFinalcontrols) {
      this.service.Put("Requisition", this.ApplicantRequisition).subscribe((res) => {
        if (res > 0) {
          this.loader = false;
          this.Snackbar.open('Applicant ' + this.ApplicantRequisition.status + ' Successfully..!');
          if (this.ApplicantRequisition.status == InterviewStatus.Selected) {
            this.router.navigate(["applicants/selected/Selected"]);
          }
          else if (this.ApplicantRequisition.status == InterviewStatus.Rejected) {
            this.router.navigate(["applicants/selected/Rejected"]);
          }
          else if (this.ApplicantRequisition.status == InterviewStatus.BlackListed) {
            this.router.navigate(["applicants/selected/BlackListed"]);
          }
        }
      });
    }
  }

  FeedBack()
  {
    debugger;
    let interviewID = this.InterviewModelList.filter(o => o.interviewPanel == "Client")[this.InterviewModelList.filter(o => o.interviewPanel == "Client").length - 1].interviewId;
    this.service.GetByCriteria("GetInterviewById",interviewID).subscribe((res) => {
      res.isfrom = "Interviewer"
     const dialogRef = this.dialogAnimation.open(InterviewFinishDialogComponent, {
       
       animation: { to: "bottom" },
       data: res,
       autoFocus: true,
       width: "600px"
     });
   
    dialogRef.afterClosed().subscribe(dialogResult => {
     if (dialogResult) {
       this.loader = true;
       dialogResult.isCompleted = true;
       dialogResult.tblInterviewEmployeeMappingStaffing = dialogResult.tblInterviewEmployeeMapping;
         this.service.Post("UpdateInterview",dialogResult).subscribe((res) => {
         
           if (res > 0) {
             this.loader = false;
             this.GetApplicantDetails();
             this.Snackbar.open('Feed Back Updated Successfully..!');
             window.scroll(0,0);
           }         
         });
     }
    });
    // dialogRef.afterClosed().subscribe(dialogResult => {
    //   if (dialogResult) {
    //     this.loader = true;
    //     dialogResult.isCompleted = true;
    //     dialogResult.tblInterviewEmployeeMappingStaffing = dialogResult.tblInterviewEmployeeMapping;
    //       this.service.Post("UpdateStatusOnStaffingFeedback",dialogResult).subscribe((res) => {
          
    //         if (res > 0) {
    //           this.loader = false;
    //           this.GetApplicantDetails();
    //           this.Snackbar.open('Feed Back Updated Successfully..!');
    //           window.scroll(0,0);
    //         }         
    //       });
    //   }
    //  });
  });
  };
  checkboxchange(f)
  {
    if(f == false)
    this.feed= true;
    else
    this.feed= false;
  }
  onChange(){
    //  this.InterviewForm.setValue({moi: ''}); 
    //this.first.reset(); this.last.reset(); 
    //this.InterviewModel.modeOfInterview.widget.option("value",null);   
    //this.InterviewModel.modeOfInterview= null;
    //alert("hi");
    this.InterviewModel.venue="";
  }
//   <md-radio-button *ngFor="let a of array" [value]="a"
//     (change)="radioChange($event)">
// {{a}}
// </md-radio-button>
  CalBackFunctions() {
    $(".reshedule").click((element) => {
      this.InterviewModel = this.InterviewModelList.filter(o => o.interviewId == parseInt(element.currentTarget.id))[0];
      for (var i = 0; this.InterviewModel.tblInterviewEmployeeMappingStaffing.length > i; i++) {
        this.InterviewModel.tblInterviewEmployeeMappingStaffing[i].employeeName = this.InterviewModel.tblInterviewEmployeeMappingStaffing[i].employee.employeeName;
      }
      this.FilterInterviewers(false);
      this.readonlycontrols = false;
      this.message = "";
      this.setStep(0);
      this.reschedule = true;
    });

    $(".info").click((element) => {
      debugger;
      var interviewobj: any = this.InterviewModelList.filter(r => r.interviewId == parseInt(element.currentTarget.id))[0];
      if(interviewobj.skillRatings != null){
      var ratings = interviewobj.skillRatings.split(',');
      if (ratings.length > 0) {
        interviewobj.applicantRequisition.requisition.tblRequisitionSkillMappingStaffing.forEach(element => {
          for (var i = 0; ratings.length > i; i++) {
            var obj = ratings[i].split(':');
            if (element.skill.skillId == obj[0]) {
              element.skill.skillRating = obj[1];
            }
          }

        });
      }
    }
      const dialogRef = this.dialogAnimation.open(InterviewFinishDialogComponent, {
        animation: { to: "bottom" },
        data: interviewobj,
        autoFocus: true,
        width: "600px"
      });
    });

    $(".cancel").click((element) => {
      this.confirmDialog(element.currentTarget.id);
    });

  }

  step = 1;

  setStep(index: number) {
    this.step = index;
  }

 
  confirmDialog(interviewID): void {
    const message = `Are you sure you cancel this interview?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialogAnimation.open(ConfirmationDialogComponent, {
      animation: { to: "top" },
      data: dialogData,
      autoFocus: true,

    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.service.Delete("CancelInterview", parseInt(interviewID)).subscribe((res) => {
          if (res == "1") {
            this.GetApplicantDetails();
            this.Snackbar.open('Interview Cancelled Sucessfully..!');
          }
        })
      }
    });
  }
  MOIChange(){
    this.InterviewModel.venue=""
  }
  CancelReschedule() {
    debugger;
    this.InterviewModel = new InterviewManagementStaffingModel();
    this.InterviewerList = [];
    this.AssigStatus();
   // this.setStep(1);
    this.reschedule = false;
    window.scroll(0, 0);
  }

  FinalStatusChange() {
    if (this.ApplicantRequisition.status == "Selected") {
      this.readonlyFinalcontrols = false;
    }
    else {
      // this.readonlyFinalcontrols = true;
    }
  }

  fetchAddress(){
    this.apiService.getAddress().subscribe((data) => {
      this.Addresses = data;      
   });
  }

  onSelect(obj){
if(obj == 'first'){
  this.InterviewModel.venue = this.Addresses[0].address;
}
    else {
      this.InterviewModel.venue = this.Addresses[1].address;
    }
  }

  
  SelectedSubmit() {
    this.isSubmitted = true;
    if (this.SelectedForm.valid) {
      if (this.SelectedApplicantsModel.selectedApplicantsId > 0) {
        this.loader = true;
        this.SelectedApplicantsModel.applicantRequisitionId = this.id;
        this.service.Put("Update" + this.Action1, this.SelectedApplicantsModel).subscribe((res) => {
          if (res > 0) {
            this.Snackbar.open('Updated Successful..!!');
            res = this.SelectedApplicantsModel.selectedApplicantsId;
            // this.SelectedApplicantsModel.selectedApplicantsId=(res);
            this.isSubmitted = false;
            window.scroll({
              top: 0,
              left: 0,
              behavior: 'smooth'
            });
          }
          else if (res < 0) {
            this.loader = false;
            this.Snackbar.open('Failed !!');
          }
 
        });
      }
      else {
        this.loader = true;
        this.SelectedApplicantsModel.applicantRequisitionId = this.id;
        this.service.Post("Create" + this.Action1, this.SelectedApplicantsModel).subscribe((res) => {
          if (res > 0) {
            this.Snackbar.open('Created Successfully..!');
            this.isSubmitted = false;
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

  OnChangeSalary(row, column) {
    debugger;
    alert(row + column);
  }

  DoCalculation(row, column) {
    var formulalist = ["*", "%", "SUM", "/", "AVERAGE", "COUNT", "COUNTA", "MIN", "MAX"]
    for (var key in this.SelectedSalaryFormulas) {
      if (this.SelectedSalaryFormulas.hasOwnProperty(key)) {
        var formula = this.SelectedSalaryFormulas[key];
        for (var p = 0; formulalist.length > p; p++) {
          var operator = formulalist[p];
          if (formula.includes(operator)) {
            
            var numberofoperators = formula.split(operator);
            if (numberofoperators.length > 2) {

            }

          }
        }

      }
    }
  }

  SplitRecursive()
  {

  }

}

// $('.ClientButton').click((element) => {
//   this.conform(element);
// });

// confirm(): void {
//   const message = `Are you sure you want to do this?`;

//   const dialogData = new ConfirmDialogModel("Confirm Action", message);

//   const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
//     maxWidth: "400px",
//     data: dialogData,
//     autoFocus: true,
//     animation:{to:"top"}
//   });

