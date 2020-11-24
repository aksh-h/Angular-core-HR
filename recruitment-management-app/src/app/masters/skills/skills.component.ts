import { Component, OnInit } from '@angular/core';
import { MasterService } from '../master.service'
import { ActivatedRoute, Router } from '@angular/router';
import { SkillModel } from '../Models/SkillModel'
import { CommonHelpers } from '../../shared/CommonHelpers'
import * as $ from 'jquery'
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { NgDialogAnimationService } from "ng-dialog-animation";

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.css']
})
export class SkillsComponent implements OnInit {

  loader = false;
  result: string = '';

  constructor(private service: MasterService, public dialog: NgDialogAnimationService, public Snackbar: SnackBarService,
    private activeRoute: ActivatedRoute,private router: Router, private _helpers: CommonHelpers) {

  }
  Model = new SkillModel();
  ModelList: SkillModel[];
  isSubmitted = false;
  Action = "Skill";

  SkillTable = [
    { "sTitle": "Skill ID", data: "skillId" },
    { "sTitle": "Skill Name", data: "skillName" },
    {
      "sTitle": "Actions", "className": 'btn-group-xs', "orderable": false, "data": "Action", "width": "30%",
      "render": function (data, type, row, meta) {
        return '<div class="btn-group">' +
          '<button style="width:60px" title = "Edit" id = "' + row.skillId + '" type = "button" class="btn btn-success btn-actions editRow" > <i class="fa fa-edit"></i></button> ' +
          '<button style="width:60px ; margin-left : 13px" title="Delete" id = "' + row.skillId + '" type="button" class="btn btn-danger btn-actions deleteRow"><i class="fa fa-trash"></i></button></div> ';
      }
    }
  ];


  ngOnInit(): void {
    this.GetAllSkills();
    $('#myModal').on('shown.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 0.5 });
    })
    $('#myModal').on('hidden.bs.modal', function (e) {
      $("#pageContent").css({ opacity: 1 });
    })
  }

  GetAllSkills() {
    this.loader = true;
    this.service.GetAll(this.Action).subscribe((res) => {
      this.ModelList = res;
      this._helpers.CreateTable("#Skilltable", res, this.SkillTable);
      this.CalBackFunctions();
      this.loader = false;
    });
  }

  CalBackFunctions() {
    $(".editRow").click((element) => {
      var deptobj = this.ModelList.filter(r => r.skillId == parseInt(element.currentTarget.id))[0];
      this.Model.skillName = deptobj.skillName;
      this.Model.skillId = deptobj.skillId;
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
    this.Model = new SkillModel();
  }

  OnSubmit() {
    this.isSubmitted = true;
    this.loader = true;
    if (this.Model.skillName) {
      if (this.Model.skillId > 0) {
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
            this.Snackbar.open('Deleted Successfully..!'); 
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
    this.GetAllSkills();
    this.HidePopup();
    this.Model = new SkillModel();

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
            this.GetAllSkills();
          }
        })
      }
      else {
      }
    });
  }

}
