import { Component, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { NgDialogAnimationService } from "ng-dialog-animation";
import { CommonHelpers } from '../../shared/CommonHelpers'
import { Router } from '@angular/router';
import { InterviewFeedBackHistoryComponent } from 'src/app/employee/interview-feedback-history/interview-feedback-history';
import { InterviewStatus } from 'src/app/shared/constants';
import { ApplicantStaffingService } from '../applicantstaffing.service';

@Component({
  selector: 'app-shortlisted-applicantsstaffing',
  templateUrl: './shortlisted-applicantsstaffing.component.html',
  styleUrls: ['./shortlisted-applicantsstaffing.component.css']
})
export class ShortlistedApplicantsstaffingComponent implements OnInit {

  constructor(private router: Router, private service: ApplicantStaffingService, private _helpers: CommonHelpers, public Snackbar: SnackBarService, public dialog: NgDialogAnimationService) { }

  ApplicantsTable = [
    { "sTitle": "Applicant Name", data: "applicant.name" },
    { "sTitle": "Requisition Title", data: "requisition.title" },
    {
      "sTitle": "ShortListed Date", "render": function (data, type, row, meta) {
        return new Date(row.createdDate).toLocaleDateString() + " " + new Date(row.createdDate).toLocaleTimeString()
      }
    },
    {
      "sTitle": "Applicant Status", data: "status"
      , "render": function (data, type, row, meta) {
        var template = '';
        if (row.status == InterviewStatus.Selected) {
          template = '<span style="color: darkgreen;">' + row.status + '</span>';
        }
        else if (row.status == InterviewStatus.Rejected) {
          template = '<span style="color: red;">' + row.status + '</span>';
        }
        else {
          template = '<span style="color: darkmagenta;">' + row.status + '</span>';
        }
        template = template + ' ' + '<div id = "' + row.applicantId + '" class="fa-hover info pull-right"><a style="cursor: pointer;"><i class="fa fa-navicon"></i></a></div>';
        return template;
      }
    },
    { "sTitle": "Owner", data: "requisition.currentOwner" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        var template = '<div class="btn-group">';
        template = template + '<button title = "View" id = "' + row.applicantRequisitionId + '" type = "button" class="btn btn-primary viewapplicantsRow" >View Details</button>';
        template = template + '</div>';
        return template;
      }
    }
  ]
  loader = false;
  Action = "Applicant";

  ngOnInit(): void {
    this.GetAllShortListedApplicants("");
  }

  GetAllShortListedApplicants(type) {
    this.loader = true;
    this.service.GetAll("ShortListed" + this.Action + type).subscribe((res) => {     
      this._helpers.CreateTable("#Applicantstable", res, this.ApplicantsTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  

  CalBackFunctions() {
    $(".viewapplicantsRow").click((element) => {
      this.router.navigate(['applicants/viewapplicantstaffing/' + element.currentTarget.id + '/ShortList'])
    });

    $(".info").click((element) => {
      this.service.GetByCriteria("Interviews", element.currentTarget.id).subscribe((res) => {        
        
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

  radioChange(event)
  {
    if (event.value == "Client")
    {
      this.GetAllShortListedApplicants("Client");

    }
    else
    {
      this.GetAllShortListedApplicants("");
    }
  }

  

}



