import { NgModule,NO_ERRORS_SCHEMA,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RequisitionManagementRoutingModule } from './requisition-management-routing.module';
import { RequisitionComponent } from './requisition/requisition.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { ViewRequisitionApplicantsComponent } from './view-requisition-applicants/view-requisition-applicants.component';
import {MatCardModule} from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';



@NgModule({
  declarations: [RequisitionComponent, ViewRequisitionApplicantsComponent, RequisitionComponent],
  imports: [
    CommonModule,
    RequisitionManagementRoutingModule,
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
export class RequisitionManagementModule { }
