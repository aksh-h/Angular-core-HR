import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InterviewStatus } from 'src/app/shared/constants';


@Component({
  selector: 'app-interview-reschedule-dialog',
  templateUrl: './interview-reschedule-dialog.component.html',
  styleUrls: ['./interview-reschedule-dialog.component.css']
})
export class InterviewRescheduleDialogComponent implements OnInit {

  title: string;
  message: string;
  constructor(public dialogRef: MatDialogRef<InterviewRescheduleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public Model: any) {    
    this.title = "Reschedule Request";    
    this.Model.status = null;
  }
  ngOnInit(): void {
  }
  isSubmitted = false;
  onConfirm(): void {  
    debugger;
    this.isSubmitted = true;  
    this.Model.status = InterviewStatus.RescheduleRequested;
    if (this.Model.status && this.Model.feedBack){
    this.dialogRef.close(this.Model);
    }
  }
 
  onDismiss(): void {  
    this.dialogRef.close(false);
  } 

}
