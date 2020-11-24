import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './shared/login/login.component';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component'
import { TopBarComponent } from './shared/top-bar/top-bar.component'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HomeComponent } from './shared/home/home.component';
import { ConfirmationDialogComponent } from './shared/confirmation-dialog/confirmation-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AgmCoreModule } from '@agm/core';
import { GoogleMapsComponent } from './shared/google-maps/google-maps.component';
import { ProgressbarComponent } from './shared/progressbar/progressbar.component';
import {MatExpansionModule} from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import {MatRadioModule} from '@angular/material/radio';
import { ChartsModule } from 'ng2-charts';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavBarComponent,
    TopBarComponent,
    HomeComponent,
    ConfirmationDialogComponent,
    GoogleMapsComponent,
    ProgressbarComponent,
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatDialogModule,
    MatSnackBarModule ,
    MatExpansionModule,
    MatIconModule,
    MatRadioModule,
    ChartsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCxyr-N6yHjiLmsFqaHRM_rbWvKgyx2y80',
      libraries: ['places']
    }) 
  ],
  exports: [
    MatButtonModule, MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
