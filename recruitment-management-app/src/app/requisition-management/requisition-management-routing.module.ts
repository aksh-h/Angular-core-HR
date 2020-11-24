import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RequisitionComponent } from './requisition/requisition.component';
import { AuthGuardService } from '../auth-guard.service';


const routes: Routes = [
  { path: 'requisition', component: RequisitionComponent , pathMatch:'full' , canActivate: [AuthGuardService]  },
 ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RequisitionManagementRoutingModule { }
