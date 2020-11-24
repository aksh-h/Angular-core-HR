import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InterviewerComponent } from './interviewer/interviewer.component';
import { AuthGuardService } from '../auth-guard.service';


const routes: Routes = [
  {
    path : 'interviewer' , component : InterviewerComponent , pathMatch:'full',canActivate:[AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
