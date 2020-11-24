import { Component, OnInit, ViewChild } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
import { MatDialog } from '@angular/material/dialog';
import { Options } from 'ng5-slider';
import { ApplicantService } from '../applicant.service'
import { CommonHelpers } from '../../shared/CommonHelpers'
import { MatDrawer } from '@angular/material/sidenav'
import { SnackBarService } from 'src/app/shared/snack-bar.service';
import { NgDialogAnimationService } from "ng-dialog-animation";
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogModel, ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { ViewListRequisitionComponent } from 'src/app/shared/view-listrequisition-dialog/view-listrequisition-dialog';

@Component({
  selector: 'app-applicantdashboard',
  templateUrl: './applicantdashboard.component.html',
  styleUrls: ['./applicantdashboard.component.css']
})
export class ApplicantdashboardComponent implements OnInit {
  @ViewChild(MatAccordion) accordion: MatAccordion;
  @ViewChild(MatDrawer) drawer: MatDrawer;
  constructor(private dialogAnimation : NgDialogAnimationService,private activeRoute: ActivatedRoute, private router: Router, public Snackbar: SnackBarService, private service: ApplicantService, private _helpers: CommonHelpers, public dialog: MatDialog) { }

  SkillDropDownSettings = {};
  LocationsDropDownSettings = {};
  QualificationsDropDownSettings = {};
  NoticePeriodsDropDownSettings = {};
  ApplicantActiveFromDropDownSettings = {};
  JobTypesDropDownSettings = {};
  SkillList: any;
  LocationsList: any;
  QualificationsList: any;
  NoticePeriodsList: any;
  ApplicantActiveFromList: any;
  JobTypesList: any;
  ExpValue = 0;
  CurrentCTCValue = 0;
  ExpectedCTCValue = 0;
  loader = false;
  Model: any;
  TempModel = [];
  ApplicantResult: any;
  SelectedApplicant: any;
  SearchValue: String;
  showDetailPane: boolean;
  showAvailableApplicants: boolean = true;
  RequisitionAction = "Requisition";
  ApplicantAction = "Applicant";
  From: String = this.activeRoute.snapshot.params.from
  // -----Filtering declaration-------------------------------
  SkillSelectedItems = [];
  LocationSelectedItems = [];
  JobTypeSelectedItems = [];
  QualificationSelectedItems = [];
  NoticePeriodSelectedItems = [];
  ApplicantActiveSelectedItems = [];
  ExperienceSelectedItems = [];
  CurrentCTCItems = [];
  ExpectedCTCItems = [];
  RequisitionType : String = "Internal";
  // -----------------------------------------------------------

  Experienceoptions: Options = {
    floor: 0,
    ceil: 30
  };

  CurrentCTCoptions: Options = {
    floor: 0,
    ceil: 20
  };

  ExpectedCTCoptions: Options = {
    floor: 0,
    ceil: 30
  };

  RequisitionTable = [
    { "sTitle": "Requisition ID", data: "requistionId" },
    { "sTitle": "Requisition Title", data: "title" },

  ]

  ngOnInit(): void {
    if (this.From == "" || this.From == null || this.From == undefined)
    {
      this.From = 'Applicant'
      this.From ='ApplicantFromRequisition'
    }
    this.SelectedApplicant = {};
    this.AllDropDownSettings();
    this.GetAllData();
    this.GetAllDataFromRequisition();
    

  }


  AllDropDownSettings() {
    this.SkillDropDownSettings = {
      singleSelection: false,
      idField: 'skillId',
      textField: 'skillName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: true,
      disabled: "disabled"
    };

    this.LocationsDropDownSettings = {
      singleSelection: false,
      idField: 'locationId',
      textField: 'locationName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: true,
      disabled: "disabled"
    };

    this.QualificationsDropDownSettings = {
      singleSelection: false,
      idField: 'qualificationId',
      textField: 'qualification',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: true,
      disabled: "disabled"
    };

    this.NoticePeriodsDropDownSettings = {
      singleSelection: false,
      idField: 'noticePeriodId',
      textField: 'noticePeriod',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: true,
      disabled: "disabled"
    };

    this.ApplicantActiveFromDropDownSettings = {
      singleSelection: false,
      idField: 'applicantActiveId',
      textField: 'applicantActive',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: true,
      disabled: "disabled"
    };

    this.JobTypesDropDownSettings = {
      singleSelection: false,
      idField: 'jobTypeId',
      textField: 'jobType',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: true,
      disabled: "disabled"
    };
  }

  GetAllData() {
    this.loader = true;
    this.service.GetAll(this.ApplicantAction ).subscribe((res) => {
      //+"FromRequisition"
      this.Model = res.Applicants;
      this.ApplicantResult = res.Applicants;
      this.SkillList = res.SkillList;
      this.LocationsList = res.Locations;
      this.QualificationsList = res.Qualifications;
      this.NoticePeriodsList = res.NoticePeriods;
      this.ApplicantActiveFromList = res.ApplicantActiveFrom;
      this.JobTypesList = res.JobTypes;
      this.loader = false;
      this.showDetailPane = true;
      this.drawer.toggle();
      this.onSelect(0);
      this.ShowAvailabe();
    });
  }

  GetAllDataFromRequisition() {
    this.loader = true;
    this.service.GetAll(this.ApplicantAction +"FromRequisition" ).subscribe((res) => {
      this.Model = res.Applicants;
      this.ApplicantResult = res.Applicants;
      this.SkillList = res.SkillList;
      this.LocationsList = res.Locations;
      this.QualificationsList = res.Qualifications;
      this.NoticePeriodsList = res.NoticePeriods;
      this.ApplicantActiveFromList = res.ApplicantActiveFrom;
      this.JobTypesList = res.JobTypes;
      this.loader = false;
      this.showDetailPane = true;
      this.drawer.toggle();
      this.onSelect(0);
      this.ShowAvailabe();
    });
  }


  onSelect(index) {
    for (var i = 0; this.Model.length > i; i++) {
      this.Model[i].isSelected = false;
    }
    this.Model[index].isSelected = true;
    this.SelectedApplicant = {};
    this.SelectedApplicant = this.Model[index];
  }

  isEmpty() {
    for (var prop in this.SelectedApplicant) {
      if (this.SelectedApplicant.hasOwnProperty(prop)) {
        return false;
      }
    }
    return true;
  }


  GlobalFilter() {
    if (this.SearchValue.trim() != '') {
      var val = this.SearchValue.trim().toUpperCase();
      if (this.ApplicantResult.length > 0) {
        this.Model = this.ApplicantResult.filter(o => o.name.toUpperCase().includes(val) ||
          o.emailAddress.toUpperCase().includes(val) || o.qualification.toUpperCase().includes(val)
        );
      }
    }
    else {
      this.Model = this.ApplicantResult;
    }
  }

  onFilterSelect(item: any) {
    debugger;
    // ------------- Location -----------------------
    if (this.ApplicantResult != undefined && this.ApplicantResult.length > 0) {    
      var FFilter = this.ApplicantResult;
   
      if (this.LocationSelectedItems) {  
        FFilter = FFilter.filter(x => x.locationPreference.includes(this.LocationSelectedItems.map(x => x.locationName).toString()));       
      }

      // --------------- JobType -------------------------
      if (this.JobTypeSelectedItems) {
        FFilter = FFilter.filter(j => j.jobType.includes(this.JobTypeSelectedItems.map(j => j.jobType).toString()));
      }

      // --------------- Skills -------------------------
      if (this.SkillSelectedItems) {
        FFilter = FFilter.filter(s => s.skillsandProficiency.includes(this.SkillSelectedItems.map(s => s.skillName).toString()));
      }

      // --------------- Qualification -------------------------
      if (this.QualificationSelectedItems) {
        FFilter = FFilter.filter(q => q.qualification.includes(this.QualificationSelectedItems.map(q => q.qualification).toString()));
      }

      // --------------- Notice Period -------------------------
      if (this.NoticePeriodSelectedItems) {
        FFilter = FFilter.filter(n => n.noticePeriod.includes(this.NoticePeriodSelectedItems.map(n => n.noticePeriod).toString()));
      }

      // --------------- Applicant Active -------------------------
      if (this.ApplicantActiveSelectedItems) {
        FFilter = FFilter.filter(a => a.applicantActive.includes(this.ApplicantActiveSelectedItems.map(a => a.applicantActive).toString()));
      }

      // --------------- Current CTC -------------------------
      if (this.CurrentCTCItems) {
        FFilter = FFilter.filter((c) => (c.currentCtc >= 0) && (c.currentCtc <= this.CurrentCTCItems)); 
      }

      // --------------- Expected CTC -------------------------
      if (this.ExpectedCTCItems) {
        FFilter = FFilter.filter((e) => (e.expectedCtc >= 0) && (e.expectedCtc <= this.ExpectedCTCItems));
      }

      // --------------- Experience -------------------------
      if (this.ExperienceSelectedItems) {
        FFilter = FFilter.filter((ex) => (parseFloat(ex.experience) >= 0) && (parseFloat(ex.experience) <= Number(this.ExperienceSelectedItems)));
      }
      // if(FFilter != undefined && FFilter.length > 0){
        this.Model = FFilter;
      // }
    }
  }

  confirmDialog(applicantID): void {
    const message = `Are you sure you want shortlist this applicant?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialogAnimation.open(ConfirmationDialogComponent, {
      animation: { to: "top" },
      data: dialogData,
      autoFocus: true,

    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      var result = dialogResult;
      if (result) {
        var array = this.From.split('-');
        this.ShortListApplicant(applicantID, array[1]);
      }
    });
  }



  ShortList(applicantID) {
    if (this.From.includes('Applicant')) {
      this.loader = true;
      this.service.GetByCriteria(this.RequisitionAction, false).subscribe((res) => {
        if (this.RequisitionType == "Client")
        {
          res =  res.filter(r => r.clientId > 0);        
        }
        else
        {
          res =  res.filter(r => r.clientId == null || r.clientId < 1);         
        }
        res.Type = this.RequisitionType;
        const dialogRef = this.dialog.open(ViewListRequisitionComponent, {         
          data: res,
          autoFocus: true,
          width: "800px",
          height: "400px"
        });
        this.loader = false;

        dialogRef.afterClosed().subscribe(dialogResult => {
          var result = dialogResult;
          if (result) {
            this.ShortListApplicant(applicantID, result);
          }
          else {
          }
        });

      });
    }
    else if (this.From.includes('Requisition')) {
      this.confirmDialog(applicantID);
    }

  }

 
  ShowAvailabe()
  {
   
    debugger;
    if (this.showAvailableApplicants){
  debugger;
    this.Model = this.ApplicantResult.filter(p => p.status == "Available");
    
    }
    else
    {
      this.onFilterSelect("");
    }
  }

  private ShortListApplicant(applicantID, requisitionID) {
    this.loader = true;
    var Internal = true;
    if (this.RequisitionType == "Client")
    {
      Internal = false;
    }
    var data = { "ApplicantID": applicantID, "RequisitionID": requisitionID , "IsInternal" : Internal };
    this.service.Post(this.RequisitionAction, data).subscribe((res) => {
      if (res > 0) {
        debugger;
        this.Snackbar.open('Applicant Shortlisted Sucessfully..!');
        this.ApplicantResult.filter(o => o.applicantId == applicantID)[0].status = "Blocked";
      }
      else {
      }
      this.loader = false;
    })
  }

  
  radioChange(event)
  {
    
  }


}

