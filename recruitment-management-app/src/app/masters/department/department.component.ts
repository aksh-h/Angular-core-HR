import { Component, OnInit } from '@angular/core';
import { MasterService } from '../master.service'
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentModel } from '../Models/DepartmentModel'
import { CommonHelpers } from '../../shared/CommonHelpers';
import * as $ from 'jquery';
import { NgDialogAnimationService } from "ng-dialog-animation";
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';



@Component({
  selector: 'app-department',  
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  loader = false;
  result: string = '';

  constructor(private service: MasterService, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }
  Model = new DepartmentModel();
  ModelList: DepartmentModel[];
  isSubmitted = false;
  Action = "Department";

  DepartmentTable = [
    { "sTitle": "Department ID", data: "departmentId" },
    { "sTitle": "Department Name", data: "department" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.departmentId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.departmentId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];

  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }


  ngOnInit(): void {
    this.GetAllDepartments();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  GetAllDepartments() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#departmenttable", res, this.DepartmentTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.filter(r => r.departmentId == parseInt(element.currentTarget.id))[0];
      this.Model.department = deptobj.department;
      this.Model.departmentId = deptobj.departmentId;
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
    this.Model = new DepartmentModel();
  }

  OnSubmit() {
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.department) {
      if (this.Model.departmentId > 0) {
        this.service.Put(this.Action, this.Model).subscribe((res) => {
          if (res == "1") {
            this.Snackbar.open('Updated Successfully..!');
            this.Refresh();
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
            this.Snackbar.open('Added Successfully..!');
            this.Refresh();
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
    this.GetAllDepartments();
    this.HidePopup();
    this.Model = new DepartmentModel();

  }


  confirmDialog(element): void {
    const message = `Are you sure you want to do this?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      maxWidth: "400px",
      data: dialogData,
      autoFocus: true,
      animation:{to:"top"}
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result) {
        this.service.Delete("Delete" + this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
          if (res == "1") {
            debugger;
            this.GetAllDepartments();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }


  

}

