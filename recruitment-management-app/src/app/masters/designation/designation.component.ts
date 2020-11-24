import { Component, OnInit } from '@angular/core';
import { MasterService } from '../master.service'
import { ActivatedRoute, Router } from '@angular/router';
import { DesignationModel } from '../Models/DesignationModel'
import { NgDialogAnimationService } from "ng-dialog-animation";
import { CommonHelpers } from '../../shared/CommonHelpers'
import * as $ from 'jquery'
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.css']
})
export class DesignationComponent implements OnInit {

  loader = false;
  result: string = '';

  constructor(private service: MasterService, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute,  private router: Router, private _helpers: CommonHelpers) {

  }
  Model = new DesignationModel();
  ModelList: DesignationModel[];
  isSubmitted = false;
  Action = "Designation";

  DesignationTable = [
    { "sTitle": "Designation ID", data: "designationId" },
    { "sTitle": "Designation Name", data: "designation" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.designationId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.designationId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];


  ngOnInit(): void {
    this.GetAllDesignations();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  GetAllDesignations() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#designationtable", res, this.DesignationTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.filter(r => r.designationId == parseInt(element.currentTarget.id))[0];
      this.Model.designation = deptobj.designation;
      this.Model.designationId = deptobj.designationId;
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
    this.Model = new DesignationModel();
  }

  OnSubmit() {
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.designation) {
      if (this.Model.designationId > 0) {
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
    this.GetAllDesignations();
    this.HidePopup();
    this.Model = new DesignationModel();

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
            this.GetAllDesignations();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

}
