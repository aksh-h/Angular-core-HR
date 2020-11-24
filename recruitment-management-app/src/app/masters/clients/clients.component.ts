import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { ClientModel } from '../Models/ClientModel';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { MasterService } from '../master.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
  loader = false;
  result: string = '';

  constructor(private service: MasterService,private formBuilder: FormBuilder, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {

  }

  Model = new ClientModel();
  ModelList: any;
  isSubmitted = false;
  Action = "Clients";
  readonlycontrols=false;
  clientform: FormGroup;

  CountryList:any;
  StateList: any;
  CityList: any;

  InitFormControl() {
    this.clientform = this.formBuilder.group({
      clientName: ['', Validators.required],
      addressline1: ['', Validators.required],
      addressline2: [],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      pincode:['', Validators.required],
      ndastartDate: ['', Validators.required],
      ndaendDate: ['', Validators.required],
      contractStartDate: ['', Validators.required],
      contractEndDate: ['', Validators.required],
      typeOfBusiness: [],     
      specialization: [],
      annualRevenue: [],
      jobBoardUrl: [],
      jobBoardUserId: [],
      jobBoardPassword:[],
      website: [],
      createdBy: [],
      modifiedBy: [],
      createdDate: [],
      modifiedDate: [],
      
    });
  }
  get f() { return this.clientform.controls; }

  ClientsTable = [
    { "sTitle": "Client ID", data: "clientId" },
    { "sTitle": "Client Name", data: "clientName" },
    { "sTitle": "Type of Business", data: "typeOfBusiness" },
    
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.clientId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.clientId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> '+
          '<button style="width:100px ; height:32px; margin-left : 13px" title="ContactDetails" id = "' + row.clientId + '" type="button" class="btn btn-primary  contactDetails">Contact Details</button></div> ';
      }
    }
  ];
  
  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  ngOnInit(): void {
    this.InitFormControl();
    this.GetAllClients();
   
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
  

  GetAllClients() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#Clientstable", res.Clients, this.ClientsTable);
      this.CountryList=res.Countries;
      this.CalBackFunctions();
      this.loader = false;
    });
  }
  
  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.Clients.filter(r => r.clientId == parseInt(element.currentTarget.id))[0];
   this.Model=deptobj;
      
      this.ShowPopup();
    });

    $('.deleteRow').click((element) => {
      this.confirmDialog(element);
    });

    $('.contactDetails').click((element) => {
      this.router.navigate(["/master/clientContacts/" +parseInt(element.currentTarget.id)]);
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
    this.Model = new ClientModel();
  }
  
  onSubmit() {
    this.isSubmitted = true;
    if(this.clientform.invalid)
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
    this.GetAllClients();
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
            this.GetAllClients();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

}

