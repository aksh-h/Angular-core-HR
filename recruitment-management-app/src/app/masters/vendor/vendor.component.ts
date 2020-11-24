import { Component, OnInit } from '@angular/core';

// import { applicantModel } from '../Models/ap

import * as $ from 'jquery';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterService } from '../master.service';
import { VendorModel } from '../Models/VendorsModel';

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.css']
})
export class VendorComponent implements OnInit {
  loader = false;
  result: string = '';

  constructor(private service: MasterService,private formBuilder: FormBuilder, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }

  Model = new VendorModel();
  ModelList: any;
  isSubmitted = false;
  Action = "Vendors";
  readonlycontrols=false;
  vendorform: FormGroup;

  CountryList:any;
  StateList : any;
  CityList : any;

  InitFormControl() {
    this.vendorform = this.formBuilder.group({
      vendorName: ['', Validators.required],
     
      ndastartDate: ['',Validators.required],
      ndaendDate: ['',Validators.required],
      contractStartDate: ['',Validators.required],
      contractEndDate: ['',Validators.required],
      typeofBusiness:[],
      specialization: [],     
      website: [],
      addressLine1: ['',Validators.required],
      addressLine2: [],
      city: ['',Validators.required],
      state: ['',Validators.required],
      country: ['',Validators.required],
      pinCode: ['',Validators.required]
      
     

    });
  }
  get f() { return this.vendorform.controls; }

  VendorTable = [
    { "sTitle": "Vendor ID", data: "vendorId" },
    { "sTitle": "Vendor Name", data: "vendorName" },
    { "sTitle": "Type of Business", data: "typeofBusiness" },

    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.vendorId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.vendorId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> '+
          '<button style="width:100px ; height:32px; margin-left : 13px" title="ContactDetails" id = "' + row.vendorId + '" type="button" class="btn btn-primary  contactDetails">Contact Details</button></div> ';

        }
    }
  ];
  
  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  ngOnInit(): void {
    this.InitFormControl();
    this.GetAllVendors();
   
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }
  
  CountryChange()
  {
    this.service.GetByCriteria("State",parseInt(this.Model.country.toString())).subscribe((res) => {
            this.StateList = res;
    });

  }

  StateChange()
  {
    this.service.GetByCriteria("City",this.Model.state).subscribe((res) => {
            this.CityList = res;
    });

  } 

 
  GetAllVendors() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#Vendortable", res.Vendors, this.VendorTable);
      this.CountryList=res.Countries;
      this.CalBackFunctions();
      this.loader = false;
    });
  }
  
  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.Vendors.filter(r => r.vendorId == parseInt(element.currentTarget.id))[0];
   this.Model=deptobj;
      
      this.ShowPopup();
    });

    $('.deleteRow').click((element) => {
      this.confirmDialog(element);
    });
    $('.contactDetails').click((element) => {
      this.router.navigate(["/master/vendorContacts/" +parseInt(element.currentTarget.id)]);
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
    this.Model = new VendorModel();
  }

  onSubmit() {
    this.isSubmitted = true;
    if(this.vendorform.invalid)
    {
      return;
    }
    this.loader = true;
    if (this.Model) {
      if (this.Model.vendorId > 0) {
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
    this.GetAllVendors();
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
      animation:{to:"top"}
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      this.result = dialogResult;
      if (this.result) {
        this.service.Delete("Delete" + this.Action, parseInt(element.currentTarget.id)).subscribe((res) => {
          if (res == "1") {
            debugger;
            this.GetAllVendors();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

  
}
