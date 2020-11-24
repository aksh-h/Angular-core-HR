import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { ConfirmationDialogComponent, ConfirmDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { MasterService } from '../master.service';
import { SalaryBreakupTemplatesModel } from '../Models/SalarybreakupTemplatesModel';
//import { SalaryBreakupTemplatesModel } from '../Models/SalarybreakupTemplatesModel';

@Component({
  selector: 'app-salarybreakuptemplates',
  templateUrl: './salarybreakuptemplates.component.html',
  styleUrls: ['./salarybreakuptemplates.component.css']
})
export class SalarybreakuptemplatesComponent implements OnInit {

  loader = false;
  result: string = '';

  constructor(private service: MasterService, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }
  Model = new SalaryBreakupTemplatesModel();
  ModelList: SalaryBreakupTemplatesModel[];
  isSubmitted = false;
  Action = "SalaryBreakUp";
  path = "";


  ImportForm = new FormGroup({
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required]),
    templateName: new FormControl('', [Validators.required]),
    salarayTemplateId: new FormControl('') 
  });

  SalaryBreakupTable = [
    { "sTitle": "SalarayTemplateId", data: "salarayTemplateId" },
    { "sTitle": "TemplateName", data: "templateName" },
    
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.SalarayTemplateId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.SalarayTemplateId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];

  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }


  ngOnInit(): void {
    this.GetAllSalaryBreakup();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  GetAllSalaryBreakup() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#SalaryBreakupTable", res, this.SalaryBreakupTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.filter(r => r.salarayTemplateId == parseInt(element.currentTarget.id))[0];
      this.Model.templateName = deptobj.templateName;
      this.Model.salarayTemplateId = deptobj.salarayTemplateId;
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
    this.Model = new SalaryBreakupTemplatesModel();
  }

  OnSubmit() {
    debugger;
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.templateName) {
      if (this.Model.salarayTemplateId > 0) {
        this.service.Postfile(this.Action+"/"+ this.Model.templateName,FormData).subscribe((res) => {
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
    this.GetAllSalaryBreakup();
    this.HidePopup();
    this.Model = new SalaryBreakupTemplatesModel();

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
            this.GetAllSalaryBreakup();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

  get f() {
    return this.ImportForm.controls;
  } 
  
  onFileChange(event) {

    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.path = file.name;
      this.ImportForm.patchValue({
        fileSource: file
      });
    }
  }

  Browse() {
    document.getElementById("file").click();
  }
}

