import { Component, OnInit } from '@angular/core';
import { MasterService } from '../master.service'
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeePanelModel } from '../Models/EmployeePanelModel'
import { CommonHelpers } from '../../shared/CommonHelpers'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgDialogAnimationService } from "ng-dialog-animation";
import * as $ from 'jquery'
import { PanelGroupModel } from '../Models/PanelGroupModel';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';


@Component({
  selector: 'app-panel-employee-mapping',
  templateUrl: './panel-employee-mapping.component.html',
  styleUrls: ['./panel-employee-mapping.component.css']
})

export class PanelEmployeeMappingComponent implements OnInit {

  loader = false;
  result: string = '';

  constructor(private service: MasterService,private formBuilder: FormBuilder, public dialog: NgDialogAnimationService,
    private _helpers: CommonHelpers, public Snackbar: SnackBarService) { }
  Model = new EmployeePanelModel();
  ModelList: EmployeePanelModel[];
  EmpList: EmployeePanelModel[];
  PGList: PanelGroupModel[];
  isSubmitted = false;
  Action = "EmployeePanel";
  EmpPanelGroupForm: FormGroup;

  EmployeePanelTable = [
    { "sTitle": "Employee Name", data: "employee.employeeName" },
    { "sTitle": "Panel Name", data: "panelGroup.panelGroupName" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.employeePanelMappingId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.employeePanelMappingId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];

  // InitFormControl()
  // {
  //   this.EmpPanelGroupForm = this.formBuilder.group({
  //     EmployeePanelMappingID: [],
  //     EmployeeID: ['', Validators.required],
  //     PanelGroupID: ['', Validators.required],
  //     Employee: ['', Validators.required],
  //     PanelGroup: ['', Validators.required],
  //   });    
  // }


  ngOnInit(): void {
    this.GetAllEmployeePanels();
    // this.InitFormControl();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  // get f() { return this.EmpPanelGroupForm.controls; }

  GetAllEmployeePanels() {
    debugger;
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res.EmployeePanelMappingList;
      this._helpers.CreateTable("#EmployeePanelTable", res.EmployeePanelMappingList, this.EmployeePanelTable);
      this.EmpList = res.EmployeeList;
      this.PGList = res.PanelGroupList;
      this.CalBackFunctions();
      this.loader = false;
    });
  }


  CalBackFunctions() {
    $(".editRow").click((element) => {
      debugger;
      var deptobj = this.ModelList.filter(r => r.employeePanelMappingId == parseInt(element.currentTarget.id))[0];
      this.Model = deptobj;
      this.isSubmitted = true;
      this.ShowPopup();
    });

    $('.deleteRow').click((element) => {
      this.confirmDialog(element);

      // this.service.Delete("Delete" + this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
      //   if (res == "1") {        
      //     this.GetAllEmployeePanels();
      //     this.HidePopup();
      //   }
      //   else {
         
      //   }
      //   this.loader = false;
      // })
    });

  }

  ShowPopup() {
    $('#myModal').show();
    this.isSubmitted = false;
  }

  HidePopup() {
    $('#myModal').hide();
    this.Model = new EmployeePanelModel();
  }

  OnSubmit() {
    debugger;
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.employeeId > 0 && this.Model.panelGroupId) {
      if (this.Model.employeePanelMappingId > 0) {
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
    this.GetAllEmployeePanels();
    this.HidePopup();
    this.Model = new EmployeePanelModel();
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
            this.GetAllEmployeePanels();
          }
        })
      }
      else {
      }
    });
  }


}
