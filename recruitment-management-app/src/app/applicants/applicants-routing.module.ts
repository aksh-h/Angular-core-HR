import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicantdashboardComponent } from './applicantdashboard/applicantdashboard.component';
import { AuthGuardService } from '../auth-guard.service';
import { ShortlistedApplicantsComponent } from './shortlisted-applicants/shortlisted-applicants.component';
import { ViewapplicantComponent } from './viewapplicant/viewapplicant.component';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants/selected-applicants.component';
import { ApplicantMasterComponent } from './applicant-master/applicant-master.component';
import { ApplicantstaffingdashboardComponent } from './applicantstaffingdashboard/applicantstaffingdashboard.component';
import { ShortlistedApplicantsstaffingComponent } from './shortlisted-applicantsstaffing/shortlisted-applicantsstaffing.component';
import { SelectedApplicantsStaffingComponent } from './selected-applicants-staffing/selected-applicants-staffing.component';
import { ViewapplicantstaffingComponent } from './viewapplicantstaffing/viewapplicantstaffing.component';
import { ResumeuploadComponent } from './resumeupload/resumeupload.component';
//import { SalarybreakuptemplatesComponent } from './salarybreakuptemplates/salarybreakuptemplates.component';


const routes: Routes = [
  { path: 'dashboard/:from', component: ApplicantdashboardComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'shortlisted', component: ShortlistedApplicantsComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'viewapplicant/:id/:from', component: ViewapplicantComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'selected/:from', component: SelectedApplicantsComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'applicant', component: ApplicantMasterComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'dashboardstaffing/:from', component: ApplicantstaffingdashboardComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'shortlistedstaffing', component: ShortlistedApplicantsstaffingComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'selectedstaffing/:from', component: SelectedApplicantsStaffingComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'viewapplicantstaffing/:id/:from', component: ViewapplicantstaffingComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'resumeupload', component:ResumeuploadComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
 

 ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicantsRoutingModule { }
