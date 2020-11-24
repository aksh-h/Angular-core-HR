import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { InterviewerComponent } from './interviewer/interviewer.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InterviewFinishDialogComponent } from './interview-finish-dialog/interview-finish-dialog';
import {MatRadioModule} from '@angular/material/radio';
import { InterviewRescheduleDialogComponent } from './interview-reschedule-dialog/interview-reschedule-dialog.component';
import {MatExpansionModule} from '@angular/material/expansion';
import { InterviewFeedBackHistoryComponent } from './interview-feedback-history/interview-feedback-history';


@NgModule({
  declarations: [InterviewerComponent,InterviewFinishDialogComponent, InterviewRescheduleDialogComponent , InterviewFeedBackHistoryComponent],
  imports: [
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    EmployeeRoutingModule,
    MatProgressSpinnerModule,
    MatRadioModule   ,
    MatExpansionModule 
  ]
})
export class EmployeeModule { }
