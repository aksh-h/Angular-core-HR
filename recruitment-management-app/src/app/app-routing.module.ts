import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './auth-guard.service';
import { LoginComponent } from './shared/login/login.component'
import { HomeComponent } from './shared/home/home.component'



const routes: Routes = [
  {
    path : 'master' , 
    loadChildren : () => import('./masters/masters.module').then(m => m.MastersModule) , 
     canActivate: [AuthGuardService]
  },
  {
    path : 'applicants' , 
    loadChildren : () => import('./applicants/applicants.module').then(m => m.ApplicantsModule) , 
     canActivate: [AuthGuardService]
  },
  {
    path : 'login' , 
   component : LoginComponent 
  },
  {
    path : '' , 
    component : HomeComponent 
  },
  {
    path : 'home' , 
    component : HomeComponent 
  },
  {
    path : 'requisitions' , 
    loadChildren : () => import('./requisition-management/requisition-management.module').then(m => m.RequisitionManagementModule) , 
     canActivate: [AuthGuardService]
  },
  {
    path : 'requisitionstaffing' , 
    loadChildren : () => import('./requisition-management-staffing/requistion-management-staffing.module').then(m => m.RequisitionManagementStaffingModule) , 
     canActivate: [AuthGuardService]
  },
  {
    path : 'employee' , 
    loadChildren : () => import('./employee/employee.module').then(m => m.EmployeeModule) , 
     canActivate: [AuthGuardService]
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes ,{scrollPositionRestoration: 'enabled'})],
  exports: [RouterModule],
  providers : [AuthGuardService]
})
export class AppRoutingModule { }
