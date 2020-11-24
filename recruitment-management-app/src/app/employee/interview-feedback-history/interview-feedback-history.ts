import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-view-dialog',
  templateUrl: './interview-feedback-history.html',
  styleUrls: ['./interview-feedback-history.css']
})
export class InterviewFeedBackHistoryComponent implements OnInit {
  title: string;
  message: string;

  constructor(public dialogRef: MatDialogRef<InterviewFeedBackHistoryComponent>,
    @Inject(MAT_DIALOG_DATA) public Model: any) {
    this.title = "FeedBack History for " + Model[0].applicant.name;

  }
  ngOnInit(): void {
    debugger;
    this.Model.forEach(element => {
      if(element.skillRatings != null || element.communication != null || element.attitude != null){
      var ratings = element.skillRatings.split(',');
      if (ratings.length > 0) {
        if(element.applicantRequisition.requisition.tblRequisitionSkillMapping==undefined)
        {
          element.tblInterviewEmployeeMapping=element.tblInterviewEmployeeMappingStaffing;
          element.applicantRequisition.requisition.tblRequisitionSkillMapping=element.applicantRequisition.requisition.tblRequisitionSkillMappingStaffing;
        }
        element.applicantRequisition.requisition.tblRequisitionSkillMapping.forEach(element => {
          for (var i = 0; ratings.length > i; i++) {
            var obj = ratings[i].split(':');
            if (element.skill.skillId == obj[0]) {
              element.skill.skillRating = obj[1];
            }
          }
        });
      }
      var communication = ""
      switch (element.communication) {
        case 1: {
          communication = "Excellent"
          break;
        }
        case 2: {
          communication = "Good"
          break;
        }
        case 3: {
          communication = "Average"
          break;
        }
        case 4: {
          communication = "Poor"
          break;
        }
        default :
        {
          communication = element.communication
        }
      }
      element.communication = communication;

      var attitude = ""
      switch (element.attitude) {
        case 1: {
          attitude = "Excellent"
          break;
        }
        case 2: {
          attitude = "Good"
          break;
        }
        case 3: {
          attitude = "Average"
          break;
        }
        case 4: {
          attitude = "Poor"
          break;
        }
        default :
        {
          attitude = element.attitude
        }
     
      }
      element.attitude = attitude;

      element.interviewDate = new Date(element.interviewDate).toLocaleDateString() + ' ' + new Date(element.interviewDate).toLocaleTimeString() 
    }
    });
    

  }
  isSubmitted = false;
  onConfirm(): void {

  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }


}


