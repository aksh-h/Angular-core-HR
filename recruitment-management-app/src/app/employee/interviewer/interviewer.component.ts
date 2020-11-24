import { Component, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { NgDialogAnimationService } from "ng-dialog-animation";
import { CommonHelpers } from '../../shared/CommonHelpers'
import { EmployeeService } from '../employee.service'
import { ApplicantStatus, InterviewStatus, Roles } from 'src/app/shared/constants';
import { InterviewFinishDialogComponent } from '../interview-finish-dialog/interview-finish-dialog';
import { ConfirmationDialogComponent, ConfirmDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { InterviewRescheduleDialogComponent } from '../interview-reschedule-dialog/interview-reschedule-dialog.component';
import { element } from 'protractor';
import { InterviewFeedBackHistoryComponent } from '../interview-feedback-history/interview-feedback-history';
import { Roles as Role } from '../../shared/constants'
import { CurrentContext as _context } from '../../shared/constants'

@Component({
  selector: 'app-interviewer',
  templateUrl: './interviewer.component.html',
  styleUrls: ['./interviewer.component.css']
})
export class InterviewerComponent implements OnInit {

  constructor(private dialogAnimation: NgDialogAnimationService, private service: EmployeeService, private _helpers: CommonHelpers, public Snackbar: SnackBarService, public dialog: NgDialogAnimationService) { }
  loader = false;
  Action = "Employee";
  Action1 ="GetAllClientInterviews";
  Model: any;
  ModelList: any;


  EmployeeTable = [
    { "sTitle": "Applicant Name", data: "applicant.name" },
    {
      "sTitle": "Interview Date", "render": function (data, type, row, meta) {
        return new Date(row.interviewDate).toLocaleDateString() + " " + new Date(row.interviewDate).toLocaleTimeString()
      }
    },
    { "sTitle": "Recruiter", data: "createdByNavigation.employeeName" },
    {
      
      "sTitle": "Status", "render": function (data, type, row, meta) {
        var template = '';
        if (row.status == InterviewStatus.Selected) {
           template = '<span style="color: darkgreen;">Selected</span>';          
        }
        else if (row.status == InterviewStatus.InterviewScheduled) {
           template = '<span style="color: darkmagenta;">Interview Scheduled</span>' ;
        }
        else {
           template = '<span style="color: red;">' + row.status + '</span>';
        }
        if(_context.RoleName ==  Role.Client){

        }
        else{
        template = template + ' ' + '<div id = "' + row.interviewId + '" class="fa-hover info pull-right"><a style="cursor: pointer;"><i class="fa fa-navicon"></i></a></div>';
        }
        return template;
      }
      
    },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        if (row.status == InterviewStatus.InterviewScheduled) {
          var template = '<div class="btn-group">';
          template = template + '<button style="margin-right:7px;" title = "View" id = "' + row.interviewId + '" type = "button" class="btn btn-primary takeinterviewRow" >Finish</button>';
          template = template + '<button  style="margin-right:7px;" title = "View" id = "' + row.interviewId + '" type = "button" class="btn btn-danger declineRow" >Decline</button>';
          template = template + '<button title = "View" id = "' + row.interviewId + '" type = "button" class="btn btn-info rescheduleRow" >Reschedule</button>';
          template = template + '</div>';
          return template;
        }
        else {
          return '';
        }
      }
    }
  ]

  ngOnInit(): void {
    if(_context.RoleName == Roles.Client)
    {
      this.GetAllClientInterviewList();
    }
    else
    {
    this.GetAllInterviewList();
    }
  }


  GetAllInterviewList() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this._helpers.CreateTable("#Interviewtable", res, this.EmployeeTable);
      this.ModelList = res;
      this.CalBackFunctions();
      this.loader = false;
    });
   
  }

  GetAllClientInterviewList() {
    this.loader = true;
    this.service.GetAll(this.Action1).subscribe((res) => {
      this._helpers.CreateTable("#Interviewtable", res, this.EmployeeTable);
      this.ModelList = res;
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  CalBackFunctions() {
    $(".takeinterviewRow").click((element) => {
      var interviewobj: any;
      interviewobj = this.ModelList.filter(r => r.interviewId == parseInt(element.currentTarget.id))[0];
      this.Model = interviewobj;
      this.Model.isfrom = "Interviewer"
      const dialogRef = this.dialog.open(InterviewFinishDialogComponent, {
        animation: { to: "bottom" },
        data: interviewobj,
        autoFocus: true,
        width: "600px"
      });

      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.loader = true;
          dialogResult.isCompleted = true;
          if(dialogResult.isClient){
            this.service.PostwithCustomController("UpdateInterview", dialogResult,"ApplicantStaffing").subscribe((res) => {
            
              if (res > 0) {
                this.loader = false;
                if (_context.RoleName == Roles.Client) {
                  this.GetAllClientInterviewList();
                }
                else {
                  this.GetAllInterviewList();
                }
                this.Snackbar.open('Feed Back Updated Successfully..!');
                window.scroll(0,0);
              }
            });
          }
         else{
          this.service.Put("UpdateInterview", dialogResult).subscribe((res) => {
            
            if (res > 0) {
              this.loader = false;
              this.GetAllInterviewList();
              this.Snackbar.open('Feed Back Updated Successfully..!');
              window.scroll(0,0);
            }
          });
         }
        
        }
      });
    });


    $(".declineRow").click((element) => {
      const message = `Are you sure you cancel this interview?`;

      const dialogData = new ConfirmDialogModel("Confirm Action", message);

      const dialogRef = this.dialogAnimation.open(ConfirmationDialogComponent, {
        animation: { to: "top" },
        data: dialogData,
        autoFocus: true,

      });

      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.service.Delete("CancelInterview", parseInt(element.currentTarget.id)).subscribe((res) => {
            if (res == "1") {
              this.GetAllInterviewList();
              this.Snackbar.open('Interview Cancelled Sucessfully..!');
              window.scroll(0,0);
            }
          })
        }
      });
    });

    $(".rescheduleRow").click((element) => {
      var interviewobj: any;
      interviewobj = this.ModelList.filter(r => r.interviewId == parseInt(element.currentTarget.id))[0];
      this.Model = interviewobj;

      const dialogRef = this.dialog.open(InterviewRescheduleDialogComponent, {
        animation: { to: "bottom" },
        data: interviewobj,
        autoFocus: true,
        width: "500px"
      });

      dialogRef.afterClosed().subscribe(dialogResult => {
        if (dialogResult) {
          this.loader = true;         
          this.service.Put("UpdateInterview", dialogResult).subscribe((res) => {
            if (res > 0) {
              this.loader = false;
              this.GetAllInterviewList();
              this.Snackbar.open('Reschedule Requested Sucessfully..!');
              window.scroll(0,0);
            }
          });
        }
      });
    });


    $(".info").click((element) => {
      debugger;
      var id = this.ModelList.filter(r => r.interviewId == parseInt(element.currentTarget.id))[0].applicantRequisitionId;
      this.service.GetByCriteria("GetInterviewHistory",id).subscribe((res) =>
      {
        const dialogRef = this.dialog.open(InterviewFeedBackHistoryComponent, {
          animation: { to: "bottom" },
          data: res,
          autoFocus: true,
          width: "550px", 
          maxHeight:"600px"
        });
      });
   
    });


  }

}
