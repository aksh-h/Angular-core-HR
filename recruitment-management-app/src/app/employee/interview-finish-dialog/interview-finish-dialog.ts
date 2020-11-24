import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CONTEXT_NAME } from '@angular/compiler/src/render3/view/util';
import { CurrentContext as _context } from '../../shared/constants'
import { Roles as Role } from '../../shared/constants'
@Component({
  selector: 'app-view-dialog',
  templateUrl: './interview-finish-dialog.html',
  styleUrls: ['./interview-finish-dialog.css']
})
export class InterviewFinishDialogComponent implements OnInit {
  title: string;
  message: string;
  readonlycontrols = false;
  readonlyclients = 0;
  constructor(public dialogRef: MatDialogRef<InterviewFinishDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public Model: any) {
    this.title = "FeedBack for " + this.Model.applicant.name;
    if (this.Model.isfrom == "Interviewer") {
      
      this.readonlycontrols = false;

      if (_context.RoleName == Role.Client) {
        //this.readonlyclients = true;
        this.Model.status = null;

      }
      else if (_context.RoleName == Role.StaffingLeadRole || _context.RoleName == Role.StaffingRole) {
        this.readonlyclients = 1;
        this.readonlycontrols = true;
        var ratings = this.Model.skillRatings.split(',');
        this.Model.applicantRequisition.requisition.tblRequisitionSkillMapping.forEach(element => {
          for (var i = 0; ratings.length > i; i++) {
            var obj = ratings[i].split(':');
            if (element.skill.skillId == obj[0]) {
              element.skill.skillRating = obj[1];
            }
          }
        });

      }
      else if (_context.RoleName == Role.EmployeeRole) {
        //this.readonlyclients = false;
        this.Model.status = null;
      }

    }
    else {
      this.readonlycontrols = true;
    }
  
    
  }
  role:String;
  staffingrole : String;
  staffingleadrole : String;
  
  
  ngOnInit(): void {
    debugger;
    this.role = _context.RoleName;
    this.staffingrole = Role.StaffingRole;
    this.staffingleadrole = Role.StaffingLeadRole;

    if (this.Model.applicantRequisition.requisition.tblRequisitionSkillMapping == undefined)
    {
      this.Model.applicantRequisition.requisition.tblRequisitionSkillMapping = this.Model.applicantRequisition.requisition.tblRequisitionSkillMappingStaffing;
    }
    
  }

  isSubmitted = false;

  onConfirm(): void {
    debugger;
    this.isSubmitted = true;
    var skillrating = "";
    
     this.Model.applicantRequisition.requisition.tblRequisitionSkillMapping.forEach(element => {
      skillrating = skillrating + element.skill.skillId + ":" + element.skill.skillRating + ","
     });
    this.Model.SkillRatings = skillrating.substring(0, skillrating.length - 1);
    if (this.Model.status && this.Model.feedBack) {
      this.dialogRef.close(this.Model);
    }
  }

  onDismiss(): void {
    this.dialogRef.close(false);
  }

  maxLengthCheck(index) {
    debugger;
    var val = this.Model.applicantRequisition.requisition.tblRequisitionSkillMapping[index].skill.SkillRating;
    if (val > 10)
      this.Model.applicantRequisition.requisition.tblRequisitionSkillMapping[index].skill.SkillRating = parseInt(val.toString().slice(0, 1));
  }

}


