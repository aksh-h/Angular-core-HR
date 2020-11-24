import { Component, OnInit } from '@angular/core';
import { MasterService } from '../master.service'
import { ActivatedRoute, Router } from '@angular/router';
import { PanelGroupModel } from '../Models/PanelGroupModel'
import { CommonHelpers } from '../../shared/CommonHelpers'
import * as $ from 'jquery'
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { NgDialogAnimationService } from "ng-dialog-animation";

@Component({
  selector: 'app-panel-groups',
  templateUrl: './panel-groups.component.html',
  styleUrls: ['./panel-groups.component.css']
})
export class PanelGroupsComponent implements OnInit {

  loader = false;
  result: string = '';

  constructor(private service: MasterService, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }
  Model = new PanelGroupModel();
  ModelList: PanelGroupModel[];
  isSubmitted = false;
  Action = "PanelGroup";

  PanelGroupTable = [
    { "sTitle": "PanelGroup ID", data: "panelGroupId" },
    { "sTitle": "PanelGroup Name", data: "panelGroupName" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.panelGroupId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.panelGroupId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];


  ngOnInit(): void {
    this.GetAllPanelGroups();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  GetAllPanelGroups() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#PanelGrouptable", res, this.PanelGroupTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }




  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.filter(r => r.panelGroupId == parseInt(element.currentTarget.id))[0];
      this.Model.panelGroupName = deptobj.panelGroupName;
      this.Model.panelGroupId = deptobj.panelGroupId;
      this.ShowPopup();
    });

    $('.deleteRow').click((element) => {
      this.confirmDialog(element);
    });

  }

  ShowPopup() {
    $('#myModal').show();
    this.isSubmitted = false;
  }

  HidePopup() {
    $('#myModal').hide();
    this.Model = new PanelGroupModel();
  }

  OnSubmit() {
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.panelGroupName) {
      if (this.Model.panelGroupId > 0) {
        this.service.Put(this.Action, this.Model).subscribe((res) => {
          if (res == "1") {
            this.Refresh();
            this.Snackbar.open('Updated Successfully..!'); 
          }
          else if (res == "-1") {
           
          }
          else {
            
          }
          this.loader = false;
        });
      }

      else {
        this.service.Post(this.Action, this.Model).subscribe((res) => {
          if (res == "1") {
            this.Refresh();
            this.Snackbar.open('Added Successfully..!'); 
          }
          else if (res == "-1") {
           
          }
          else {
            
          }
          this.loader = false;
        });
      }
    }

  }

  Refresh() {
    this.GetAllPanelGroups();
    this.HidePopup();
    this.Model = new PanelGroupModel();

  }

  confirmDialog(element): void {
    const message = `Are you sure you want to do this?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      // maxWidth: "400px",
      data: dialogData,
      autoFocus: true,
      animation:{to:"top"}
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result) {
        this.service.Delete("Delete" + this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
          if (res == "1") {       
            this.Snackbar.open('Deleted Successfully..!'); 
            this.GetAllPanelGroups();
          }
        })
      }
      else {
      }
    });
  }

}
