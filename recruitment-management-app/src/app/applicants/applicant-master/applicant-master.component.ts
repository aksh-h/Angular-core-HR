import { Component, OnInit } from '@angular/core';
import { ApplicantService } from '../applicant.service'

// import { applicantModel } from '../Models/ap

import * as $ from 'jquery';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ApplicantMasterModel } from '../Models/ApplicantMasterModel';
import { MasterService } from 'src/app/masters/master.service';
@Component({
  selector: 'app-applicant-master',
  templateUrl: './applicant-master.component.html',
  styleUrls: ['./applicant-master.component.css']
})
export class ApplicantMasterComponent implements OnInit {
  loader = false;
  result: string = '';


  constructor(private service: ApplicantService, private service1: MasterService, private formBuilder: FormBuilder, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {
  }
  Model = new ApplicantMasterModel();
  ModelList: any;
  VendorList: any;
  JobPortalList: any;
  selectedvalue: any;
  selectedType: any;
  isSubmitted = false;
  Action = "Applicant";
  path = "";
  readonlycontrols = false;
  applicantform: FormGroup;
  InitFormControl() {
    this.applicantform = this.formBuilder.group({
      name: ['', Validators.required],
      dob: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
      qualification: ['', Validators.required],
      experience: ['', Validators.required],
      relevantExperience: [],
      currentCtc: [],
      expectedCtc: [],
      joiningTime: [],
      skillsandProficiency: ['', Validators.required],
      locationPreference: [],
      referedBy: [],
      jobType: [],
      source: [],
      applicantActive: ['', Validators.required],
      noticePeriod: [],
      passportNo: [],
      panNo: ['', [Validators.required, Validators.pattern("[A-Z]{5}[0-9]{4}[A-Z]{1}")]],
      vendorId: [],
      portalId: [],
      employeeEmailId: [],
      sourceId: [],
      phoneNumber: ['', Validators.required],
      file: ['', [Validators.required]],
      fileSource: ['', [Validators.required]],

    });

  }

 

  get f() { return this.applicantform.controls; }
  
  

  ApplicantTable = [
    { "sTitle": "Applicant ID", data: "applicantId" },
    { "sTitle": "Applicant Name", data: "name" },
    { "sTitle": "Applicant Email", data: "emailAddress" },

    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.applicantId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.applicantId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];

  isOpen = true;
  toggle() {
    this.isOpen = !this.isOpen;
  }
  ngOnInit(): void {
    this.InitFormControl();
    this.GetAllApplicants();
    this.VendorSelection();
    this.JobPortalSelection();

    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  GetAllApplicants() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#Applicanttable", res.Applicants, this.ApplicantTable);
      //this.VendorList = res.Vendor;
      //this.JobPortalList = res.JobPortal;
      this.CalBackFunctions();
      this.loader = false;
    });


  }

  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.Applicants.filter(r => r.applicantId == parseInt(element.currentTarget.id))[0];
      this.Model = deptobj;

      this.ShowPopup();
    });
    $('.deleteRow').click((element) => {
      this.confirmDialog(element);
    });
  }
  ShowPopup() {
    debugger;
    $('#myModal').show();
    $('#index').hide();
    this.isSubmitted = false;
  }
  HidePopup() {
    $('#myModal').hide();
    $('#index').show();
    this.Model = new ApplicantMasterModel();
  }
  onSubmit() {
debugger;
    this.isSubmitted = true;
    if (this.applicantform.invalid) {
      return;
    }
     const formData = new FormData();
     formData.append('file', this.applicantform.get('fileSource').value);      
     formData.append('ApplicantModel',JSON.stringify(this.Model))
    this.loader = true;
    debugger;
    if (this.Model) {
      
      if (this.Model.applicantId > 0) {
        this.service.Put("Update" + this.Action, formData).subscribe((res) => {
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
        this.service.Post(this.Action, formData).subscribe((res) => {
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
    this.GetAllApplicants();
    this.HidePopup();
    //this.Model = new applicantModel();
  }

  confirmDialog(element): void {
    const message = `Are you sure you want to do this?`;
    const dialogData = new ConfirmDialogModel("Confirm Action", message);
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      maxWidth: "400px",
      data: dialogData,
      autoFocus: true,
      animation: { to: "top" }
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result) {
        this.service.Delete("Delete" + this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
          if (res == "1") {
            debugger;
            this.GetAllApplicants();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

  VendorSelection() {
    this.service1.GetAll("Vendors").subscribe((res) => {
      debugger;
      this.VendorList = res.Vendors;
    });
  }

  JobPortalSelection() {
    this.service1.GetAll("JobPortal").subscribe((res) => {
      debugger;
      this.JobPortalList = res.JobPortal;


    });

  }

  Browse() {
    document.getElementById("file").click();
   }

   

  ExportTemplate()
  {
    
  }

  onFileChange(event) {

    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.path = file.name;
      this.applicantform.patchValue({
        fileSource: file
      });
    }
  }

  onChange(event) {
    this.selectedType = event.target.value;
  }


}