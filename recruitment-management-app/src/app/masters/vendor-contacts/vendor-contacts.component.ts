import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterService } from '../master.service';
import { VendorModel } from '../Models/VendorsModel';
import { VendorContactsModel } from '../Models/VendorContactsModel';

@Component({
  selector: 'app-vendor-contacts',
  templateUrl: './vendor-contacts.component.html',
  styleUrls: ['./vendor-contacts.component.css']
})
export class VendorContactsComponent implements OnInit {
  loader = false;
  result: string = '';
  constructor(private service: MasterService,private formBuilder: FormBuilder, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }
  Model = new VendorContactsModel();
  ModelList: any;
  isSubmitted = false;
  Action = "VendorContacts";
  readonlycontrols=false;
  vendorContactsform: FormGroup;


  InitFormControl() {
    this.vendorContactsform = this.formBuilder.group({
      contactPerson: ['', Validators.required],
      contactphoneNo: ['', Validators.required],
      contactPersonEmailId: ['', Validators.required],
      designation: ['',Validators.required]
     
    });
  }
  get f() { return this.vendorContactsform.controls; }


    VendorContactsTable = [
      { "sTitle": "Contact ID", data: "contactId" },
      { "sTitle": "Contact Person", data: "contactPerson" },
      { "sTitle": "Contact Person EmailId", data: "contactPersonEmailId" },
      { "sTitle": "Contact Person designation", data: "designation" },

    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.contactId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.contactId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];
  
  id: number = this.activeRoute.snapshot.params.id
  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  ngOnInit(): void {
    this.InitFormControl();
    this.GetAllVendorContacts();
   
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }
  
  GetAllVendorContacts() {
    this.loader = true;
    this.service.GetByCriteria(this.Action,this.id).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#VendorContactstable", res, this.VendorContactsTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }
  
  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.filter(r => r.contactId == parseInt(element.currentTarget.id))[0];
   this.Model=deptobj;
      
      this.ShowPopup();
    });

    $('.deleteRow').click((element) => {
      this.confirmDialog(element);
    });

  }

  Back(){
    this.router.navigate(['master/vendor/'])
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
    this.Model = new VendorContactsModel();
  }

  onSubmit() {
    this.isSubmitted = true;
    if(this.vendorContactsform.invalid)
    {
      return;
    }
    this.loader = true;
    if (this.Model) {
      this.Model.vendorId=parseInt(this.id.toString());
      if (this.Model.contactId > 0) {
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
    this.GetAllVendorContacts();
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
            this.GetAllVendorContacts();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }


  
}
