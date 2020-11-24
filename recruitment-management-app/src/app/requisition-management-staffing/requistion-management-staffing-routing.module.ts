import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuardService } from '../auth-guard.service';
import { RequisitionStaffingComponent } from './requisitionstaffing/requisitionstaffing.component';



const routes: Routes = [
  { path: 'requisition', component: RequisitionStaffingComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
 ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RequisitionManagementStaffingRoutingModule { }





