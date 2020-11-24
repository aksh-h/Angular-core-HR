import { Component, OnInit } from '@angular/core';
import { MasterService } from '../master.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { ClientscontactdetailsModel } from '../Models/ClientcontactdetailsModel';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-clientcontactdetails',
  templateUrl: './clientcontactdetails.component.html',
  styleUrls: ['./clientcontactdetails.component.css']
})
export class ClientcontactdetailsComponent implements OnInit {
  loader = false;
  result: string = '';
  // mobNumberPattern = "^((\\+91-?)|0)?[0-9]{10}$";  

 constructor(private service: MasterService,private formBuilder: FormBuilder, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }

  Model = new ClientscontactdetailsModel();
  ModelList: any;
  isSubmitted = false;
  Action = "Clientscontact";
  readonlycontrols=false;
  clientcontactform: FormGroup;

  InitFormControl() {
    this.clientcontactform = this.formBuilder.group({
      contactPerson: ['', Validators.required],
      contactPhoneNo: ['', Validators.required],
      contactPersonEmailId: ['', Validators.required],
      contactPersonDesignation: ['', Validators.required]
      
    });
  }

  get f() { return this.clientcontactform.controls; }

  ClientsTable = [
    { "sTitle": "Contact Person", data: "contactPerson" },
    { "sTitle": "Contact PhoneNo", data: "contactPhoneNo" },
    { "sTitle": "Contact Person EmailId", data: "contactPersonEmailId" },
    { "sTitle": "Contact Person Designation", data: "contactPersonDesignation" },
    
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.contactId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.contactId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];
  
  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  ngOnInit(): void {
    this.InitFormControl();
    this.GetAllClientsContact();
   
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }
 
  
  

  GetAllClientsContact() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#Clientscontactdetailstable", res, this.ClientsTable);
      
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
    this.router.navigate(['master/client/'])
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
    this.Model = new ClientscontactdetailsModel();
  }

  onSubmit() {
    this.isSubmitted = true;
    if(this.clientcontactform.invalid)
    {
      return;
    }
    this.loader = true;
    if (this.Model) {
      if (this.Model.clientId > 0) {
        this.service.Put("Update"+this.Action, this.Model).subscribe((res) => {
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
    this.GetAllClientsContact();
    this.HidePopup();
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
            this.GetAllClientsContact();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

}
