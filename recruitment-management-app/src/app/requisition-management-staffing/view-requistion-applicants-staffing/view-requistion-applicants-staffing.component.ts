import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InterviewStatus } from 'src/app/shared/constants';

@Component({
  selector: 'app-view-requistion-applicants-staffing',
  templateUrl: './view-requistion-applicants-staffing.component.html',
  styleUrls: ['./view-requistion-applicants-staffing.component.css']
})
export class ViewRequistionApplicantsStaffingComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ViewRequistionApplicantsStaffingComponent>,
    @Inject(MAT_DIALOG_DATA) public Model: any) {
  }

  InProgressApplicants: Number = 0;
  ShorlistedApplicants: Number = 0;
  SelectedApplicants: Number = 0;
  RejectedApplicants: Number = 0;
  FinalMessage: String;
  ngOnInit(): void {
    if (this.Model && this.Model.length > 0) {
      this.ShorlistedApplicants = this.Model.length;
      this.SelectedApplicants = this.Model.filter(o => o.status == InterviewStatus.Selected).length;
      this.RejectedApplicants = this.Model.filter(o => o.status == InterviewStatus.Rejected).length;
      this.InProgressApplicants = this.Model.filter(o => o.status != InterviewStatus.Selected && o.status != InterviewStatus.Rejected && o.status != InterviewStatus.BlackListed).length;
      if (this.SelectedApplicants == this.Model[0].requisition.noofPositions) {
        this.FinalMessage = "Required Positions has been closed by the Recreuiters, Requisition Can be closed"
      }
    }
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

}

