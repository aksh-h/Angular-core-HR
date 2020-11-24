import { Component, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { NgDialogAnimationService } from "ng-dialog-animation";
import { Router } from '@angular/router';

import { CommonHelpers } from 'src/app/shared/CommonHelpers';

import { ApplicantStaffingService } from '../applicantstaffing.service';


@Component({
  selector: 'app-selected-applicants-staffing',
  templateUrl: './selected-applicants-staffing.component.html',
  styleUrls: ['./selected-applicants-staffing.component.css']
})
export class SelectedApplicantsStaffingComponent implements OnInit {

  constructor(private router: Router, private service: ApplicantStaffingService, private _helpers: CommonHelpers, public Snackbar: SnackBarService, public dialog: NgDialogAnimationService) { }

  ApplicantType : String = "Selected";

  ApplicantsTable = [
    { "sTitle": "Applicant Name", data: "applicant.name" },
    { "sTitle": "Requisition Title", data: "requisition.title" },
    { "sTitle": "ShortListed Date", data: "createdDate" },
    { "sTitle": "Applicant Status", data: "status" },
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
    this.GetAllApplicants();
  }

  GetAllApplicants() {
    this.loader = true;  
    this.service.GetAll(this.ApplicantType + this.Action).subscribe((res) => {
      this._helpers.CreateTable("#Applicantstable", res, this.ApplicantsTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  CalBackFunctions() {
    $(".viewapplicantsRow").click((element) => {
      this.router.navigate(['applicants/viewapplicantstaffing/' + element.currentTarget.id + '/Selected'])
    });
  }

  FilterApplicants()
  {
    this.GetAllApplicants();
  }

}


