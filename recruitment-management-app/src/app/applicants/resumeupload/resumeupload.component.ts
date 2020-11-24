import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { MasterService } from 'src/app/masters/master.service';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { ApplicantService } from '../applicant.service';
import { ResumeuploadModel } from '../Models/ResumeUploadModel';

@Component({
  selector: 'app-resumeupload',
  templateUrl: './resumeupload.component.html',
  styleUrls: ['./resumeupload.component.css']
})
export class ResumeuploadComponent implements OnInit {

  constructor(private service: ApplicantService,private service1:MasterService,private formBuilder: FormBuilder, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) { 

  }
  Model: ResumeuploadModel;
  VendorList: any;
  JobPortalList: any;
  selectedvalue: any;
  selectedType: any;
  Action ="ResumeImport";
  path = ""

  ImportForm = new FormGroup({
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required]),
    sourceId :new FormControl(''),
    vendorId :new FormControl(''),
    portalId : new FormControl(''),
    employeeEmailId :new FormControl('')
  });

  ngOnInit(): void {
    this.Model=new ResumeuploadModel();
    this.VendorSelection();
    this.JobPortalSelection();
  }

   Browse() {
    document.getElementById("file").click();
   }

  submit() {
    debugger;
    const formData = new FormData();
    formData.append('file', this.ImportForm.get('fileSource').value);      
    formData.append('ResumeUploadModel',JSON.stringify(this.Model))
    if (this.path.length > 0) {
      this.service.Postfile(this.Action,formData).subscribe(res => {
      
        if (res == "0") {
          alert("Couldnt Save the file");
        }
        else {
          alert("Records Updated Successfully")
        }
      });
    }
    else {
      alert('Please Select File');
    }


  }


  get f() {
    return this.ImportForm.controls;
  }

  ExportTemplate()
  {
    
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



  onChange(event) {
    this.selectedType = event.target.value;
  }
}



