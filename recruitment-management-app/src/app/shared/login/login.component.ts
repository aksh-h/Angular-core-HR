import { Component, OnInit} from '@angular/core';
import { LocalStorageService } from 'angular-web-storage';
import { FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']  
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  isSubmitted = false;
  Action = "Login";
  LoggedIn: Boolean;
  loader = false;

  constructor(private router: Router, private service: SharedService,
    private formBuilder: FormBuilder, private storage: LocalStorageService) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    }
    );
  }

  get f() { return this.loginForm.controls; }

  logintype :String ="Internal";

  onSubmit() {  
    this.storage.set("IsLoggedIn", true);
    this.isSubmitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.loader = true;
    var action="";
    if(this.logintype == "Internal")
    {
      action="Post";
    }
    else{
      action="Client";
    }
    this.service.Post(action, this.loginForm.value).subscribe((res) => {
      if (res) {
        this.storage.set("IsLoggedIn", true);
        this.storage.set("role",res.role);
        this.storage.set("userid",res.userid);
        this.storage.set("token",res.token);
        this.storage.set("loggedUser",res.username);      
        this.loader = false;  
        window.location.reload();
      }
      else {
        this.storage.set("IsLoggedIn", false);
        this.loader = false;   
      }

    });
  }
  
  
  }


