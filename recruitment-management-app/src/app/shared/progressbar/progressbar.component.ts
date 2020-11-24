import { Component, OnInit,  Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-progressbar',
  templateUrl: './progressbar.component.html',
  styleUrls: ['./progressbar.component.css']
})
export class ProgressbarComponent implements OnInit {

  title : String
  orderStatus : String
  constructor(public dialogRef: MatDialogRef<ProgressbarComponent>,
    @Inject(MAT_DIALOG_DATA) public items : any) {    
    this.title = "Select WorkFlow";    
  }

  ngOnInit(): void {
    this.orderStatus = this.items[this.items.length - 1]
  }

  onConfirm(workflowName,workflow): void {    
    this.dialogRef.close(workflowName + " : " + workflow);
  }
 
  onDismiss(): void {  
    this.dialogRef.close(false);
  } 

}
