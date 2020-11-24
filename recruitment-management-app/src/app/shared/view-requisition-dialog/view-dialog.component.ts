import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RequisitionModel } from '../../requisition-management/Models/RequisitionModel';

@Component({
  selector: 'app-view-dialog',
  templateUrl: './view-dialog.component.html',
  styleUrls: ['./view-dialog.component.css']
})
export class ViewDialogComponent implements OnInit {
  title: string;
  message: string;
  constructor(public dialogRef: MatDialogRef<ViewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public Model: RequisitionModel) {    
    this.title = "Requisition Details";    
  }
  ngOnInit(): void {
  }
  
  onConfirm(): void {    
    this.dialogRef.close(true);
  }
 
  onDismiss(): void {  
    this.dialogRef.close(false);
  } 

}


