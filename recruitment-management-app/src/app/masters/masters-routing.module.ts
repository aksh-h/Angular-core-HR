import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DepartmentComponent } from './department/department.component'
import { DesignationComponent } from './designation/designation.component'
import { PanelGroupsComponent } from './panel-groups/panel-groups.component'
import { SkillsComponent } from './skills/skills.component'
import { PanelEmployeeMappingComponent } from './panel-employee-mapping/panel-employee-mapping.component'
import { AuthGuardService } from '../auth-guard.service';
import { VendorComponent } from './vendor/vendor.component';
import { VendorContactsComponent } from './vendor-contacts/vendor-contacts.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientcontactdetailsComponent } from './clientcontactdetails/clientcontactdetails.component';
import { JobportalComponent } from './jobportal/jobportal.component';
import { SalarybreakuptemplatesComponent } from './salarybreakuptemplates/salarybreakuptemplates.component';
//import { SalarybreakuptemplatesComponent } from './salarybreakuptemplates/salarybreakuptemplates.component';
//import { SalarybreakuptemplatesComponent } from './salarybreakuptemplates/salarybreakuptemplates.component';

const routes: Routes = [
  { path: 'department', component: DepartmentComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'designation', component: DesignationComponent , pathMatch:'full', canActivate: [AuthGuardService] },
  { path: 'panelgroup', component: PanelGroupsComponent , pathMatch:'full', canActivate: [AuthGuardService] },
  { path: 'skills', component: SkillsComponent , pathMatch:'full', canActivate: [AuthGuardService] },
  { path: 'panelemployeemapping', component: PanelEmployeeMappingComponent , pathMatch:'full', canActivate: [AuthGuardService] },
  { path: 'vendor', component: VendorComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'vendorContacts/:id', component: VendorContactsComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'client', component: ClientsComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'clientContacts/:id', component: ClientcontactdetailsComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path: 'jobportal', component: JobportalComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  //{ path: 'salarybreakuptemplate', component:SalarybreakuptemplatesComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
  { path:'salarybreakuptemplates',component:SalarybreakuptemplatesComponent,pathMatch:'full' , canActivate: [AuthGuardService]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MastersRoutingModule { }
