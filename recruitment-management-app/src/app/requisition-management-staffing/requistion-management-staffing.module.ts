import { NgModule,NO_ERRORS_SCHEMA,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
//import { RequisitionstaffingComponent } from './requisitionstaffing/requisitionstaffing.component';
import { RequisitionStaffingComponent } from './requisitionstaffing/requisitionstaffing.component';
//import { ViewRequistionApplicantsStaffingComponent } from './view-requisition-applicants-staffing/view-requisition-applicants-staffing.component';
import { RequisitionManagementStaffingRoutingModule } from '../requisition-management-staffing/requistion-management-staffing-routing.module';
import { ViewRequistionApplicantsStaffingComponent } from './view-requistion-applicants-staffing/view-requistion-applicants-staffing.component';

@NgModule({
  declarations: [RequisitionStaffingComponent, ViewRequistionApplicantsStaffingComponent,RequisitionStaffingComponent],
  imports: [
    CommonModule,
    RequisitionManagementStaffingRoutingModule,
    FormsModule,
    ReactiveFormsModule ,
    NgMultiSelectDropDownModule,
    MatProgressSpinnerModule ,
    MatButtonModule,
    MatCardModule,
    MatRadioModule
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ]
})
export class RequisitionManagementStaffingModule { }