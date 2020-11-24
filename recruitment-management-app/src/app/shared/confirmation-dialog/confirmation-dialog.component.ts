import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {
  title: string;
  message: string;
  constructor(public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogModel) {
      // Update view with given values
    this.title = data.title;
    this.message = data.message;
  }
  ngOnInit(): void {
  }
  
  onConfirm(): void {
    // Close the dialog, return true
    this.dialogRef.close(true);
  }
 
  onDismiss(): void {   
    // Close the dialog, return false
    this.dialogRef.close(false);   
  } 

}

export class ConfirmDialogModel {
 
  constructor(public title: string, public message: string) {
  }
}
