import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ApplicantsRoutingModule } from './applicants-routing.module';
import { ApplicantdashboardComponent } from './applicantdashboard/applicantdashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import { Ng5SliderModule } from 'ng5-slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatCardModule} from '@angular/material/card';
import { ShortlistedApplicantsComponent } from './shortlisted-applicants/shortlisted-applicants.component';
import { ViewapplicantComponent } from './viewapplicant/viewapplicant.component';
import { OwlDateTimeModule,OwlNativeDateTimeModule} from 'ng-pick-datetime';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants/selected-applicants.component';
import {MatRadioModule} from '@angular/material/radio';
import {MatButtonModule} from '@angular/material/button';
import { ApplicantMasterComponent } from './applicant-master/applicant-master.component';
import { ApplicantstaffingdashboardComponent } from './applicantstaffingdashboard/applicantstaffingdashboard.component';
import { SelectedApplicantsStaffingComponent } from './selected-applicants-staffing/selected-applicants-staffing.component';
import { ShortlistedApplicantsstaffingComponent } from './shortlisted-applicantsstaffing/shortlisted-applicantsstaffing.component';
import { ViewapplicantstaffingComponent } from './viewapplicantstaffing/viewapplicantstaffing.component';
import { ResumeuploadComponent } from './resumeupload/resumeupload.component';
//import { SalarybreakuptemplatesComponent } from './salarybreakuptemplates/salarybreakuptemplates.component';



@NgModule({
  declarations: [ApplicantdashboardComponent, ShortlistedApplicantsComponent, ViewapplicantComponent, SelectedApplicantsComponent, ApplicantMasterComponent, ApplicantstaffingdashboardComponent, SelectedApplicantsStaffingComponent, ShortlistedApplicantsstaffingComponent, ViewapplicantstaffingComponent, ResumeuploadComponent],
  imports: [
    CommonModule,
    ApplicantsRoutingModule,
    FormsModule,
    ReactiveFormsModule ,
    NgMultiSelectDropDownModule,
    MatProgressSpinnerModule ,
    MatExpansionModule,
    MatInputModule,
    MatIconModule,
    Ng5SliderModule,
    MatSlideToggleModule,
    MatSidenavModule,
    MatCardModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule ,
    MatRadioModule  ,
    MatButtonModule
  ]
})
export class ApplicantsModule { }
