import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MastersRoutingModule } from './masters-routing.module';
import { DepartmentComponent } from './department/department.component';
import { DesignationComponent } from './designation/designation.component';
import { SkillsComponent } from './skills/skills.component';
import { PanelGroupsComponent } from './panel-groups/panel-groups.component';
import { PanelEmployeeMappingComponent } from './panel-employee-mapping/panel-employee-mapping.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { VendorComponent } from './vendor/vendor.component';
import { VendorContactsComponent } from './vendor-contacts/vendor-contacts.component';
import { ClientsComponent } from './clients/clients.component';

import { ClientcontactdetailsComponent } from './clientcontactdetails/clientcontactdetails.component';
import { JobportalComponent } from './jobportal/jobportal.component';
import { SalarybreakuptemplatesComponent } from './salarybreakuptemplates/salarybreakuptemplates.component';




@NgModule({
  declarations: [DepartmentComponent, DesignationComponent, SkillsComponent, PanelGroupsComponent, PanelEmployeeMappingComponent, VendorComponent, VendorContactsComponent,ClientsComponent,ClientcontactdetailsComponent, JobportalComponent, SalarybreakuptemplatesComponent],
  imports: [
    CommonModule,
    MastersRoutingModule,
    FormsModule,
    ReactiveFormsModule,    
    MatSnackBarModule,
    MatProgressSpinnerModule  
    
  ]
})
export class MastersModule { }
