import { Component } from '@angular/core';
import { LocalStorageService } from 'angular-web-storage';
import { Router } from '@angular/router';
import { CurrentContext as context } from './shared/constants'
import * as $ from 'jquery'


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']  

})
export class AppComponent {
  title = 'recruitment-management-app';
  showLogin = true;

  constructor(private router: Router, private storage: LocalStorageService) { }

  ngOnInit() {
    var valid = this.storage.get('IsLoggedIn');
    if (valid) {          
      this.showLogin = false;      
    }
    else {
      this.showLogin = true;
    }
    context.UserID = this.storage.get("userid");
    context.RoleName = this.storage.get("role");
  }

}


