import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RequisitionModel } from '../../requisition-management/Models/RequisitionModel';
import { CommonHelpers } from '../../shared/CommonHelpers'
import { MatDialog } from "@angular/material/dialog";
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-listrequisition-dialog',
  templateUrl: './view-listrequisition-dialog.html',
  styleUrls: ['./view-listrequisition-dialog.css']
})
export class ViewListRequisitionComponent implements OnInit {
  title: string;
  message: string;
  SelectedModel: any;

  RequisitionTable = [
    { "sTitle": "Requisition Title", data: "title" },
    { "sTitle": "Created Date", data: "createdDate" },
    { "sTitle": "Status", data: "status" },
    { "sTitle": "Owner", data: "currentOwner" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        var template = '<div class="btn-group">';
        template = template + '<button style=" margin-left : 13px" title="View" id = "' + row.requistionId + '" type="button" class="btn btn-info viewRow">View</button>   &nbsp;&nbsp;';
        template = template + '<button style=" margin-left : 10px"  title = "View" id = "' + row.requistionId + '" type = "button" class="btn btn-info assignRow" >Assign</button>';
        template = template + '</div>';
        return template;
      }
    }
  ];

  constructor(public dialogRef: MatDialogRef<ViewListRequisitionComponent>,
    @Inject(MAT_DIALOG_DATA) public Model: any, private _helpers: CommonHelpers,
    public dialog: MatDialog) {
    this.title = "Requisition Details";
  }
  ngOnInit(): void {   
    // this.dialogRef.updateSize("70%", "60%");
    this._helpers.CreateTable("#Requisitiontable", this.Model, this.RequisitionTable, true, false);
    $(".viewRow").click((element) => {
      var requisitionobj: any;
      requisitionobj = this.Model.filter(r => r.requistionId == parseInt(element.currentTarget.id))[0];
      this.SelectedModel = requisitionobj;
      let p = 0;
      if(this.SelectedModel.tblRequisitionSkillMapping==undefined)
      {     
        this.SelectedModel.tblRequisitionSkillMapping=this.SelectedModel.tblRequisitionSkillMappingStaffing;
      }
      this.SelectedModel.tblRequisitionSkillMapping.forEach(function (element) {
       
        element.skillName = requisitionobj.tblRequisitionSkillMapping[p].skill.skillName;
        p = p + 1;
      });
      
      this.SelectedModel.SkillsText = this.SelectedModel.tblRequisitionSkillMapping.map(function (val) { return val.skillName }).join(',');
      
      this.ShowView();
    });

    $(".assignRow").click((element) => {      
      this.dialogRef.close(element.currentTarget.id); 
    });
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

  ShowView() {
    $("#viewSelectedModel").show();
    $("#listrequisition").hide();
  }


  HideView() {
    $("#viewSelectedModel").hide();
    $("#listrequisition").show();
    this.SelectedModel = new RequisitionModel();
  }

}


