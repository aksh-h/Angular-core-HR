import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgDialogAnimationService } from 'ng-dialog-animation';
import { CommonHelpers } from 'src/app/shared/CommonHelpers';
import { ConfirmationDialogComponent, ConfirmDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { MasterService } from '../master.service';
import { JobPortalModel } from '../Models/JobportalModel';
@Component({
  selector: 'app-jobportal',
  templateUrl: './jobportal.component.html',
  styleUrls: ['./jobportal.component.css']
})
export class JobportalComponent implements OnInit {
  loader = false;
  result: string = '';
  constructor(private service: MasterService, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute, private router: Router, private _helpers: CommonHelpers) {
  }
  Model = new JobPortalModel();
  ModelList: JobPortalModel[];
  isSubmitted = false;
  Action = "JobPortal";
  JobPortalTable = [
    { "sTitle": "Portal ID", data: "portalId" },
    { "sTitle": "Portal Name", data: "portalName" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.portalId + '" type = "button" class="btn btn-default btn-actions editRow" ><i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.portalId + '" type="button" class="btn btn-default btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];
  isOpen = true;
  toggle() {
    this.isOpen = !this.isOpen;
  }

  ngOnInit(): void {
    this.GetAllJobPortals();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }
  GetAllJobPortals() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#jobportaltable", res, this.JobPortalTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }
  CalBackFunctions() {
    $(".editRow").click((element) => {
      var jobobj = this.ModelList.filter(r => r.portalId == parseInt(element.currentTarget.id))[0];
      this.Model.portalName = jobobj.portalName;
      this.Model.portalId = jobobj.portalId;
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
    this.Model = new JobPortalModel();
  }
  OnSubmit() {
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.portalName) {
      if (this.Model.portalId > 0) {
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
    this.GetAllJobPortals();
    this.HidePopup();
    this.Model = new JobPortalModel();
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
            this.GetAllJobPortals();
            this.Snackbar.open('Deleted Successfully..!');
          }
        })
      }
      else {
      }
    });
  }

  
}