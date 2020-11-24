import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment as ENV } from "../../environments/environment";
import { Observable } from "rxjs";
import { LocalStorageService } from 'angular-web-storage';



@Injectable({
  providedIn: 'root'
})
export class MasterService {


  Controller = "Masters/"
  APIURL = `${ENV.ApiURL}` + this.Controller;
  httpHeaders = new HttpHeaders();


  constructor(private httpclient: HttpClient, private storage: LocalStorageService) {
    var token : string = this.storage.get("token");
    var userid : string = this.storage.get("userid");
    var role : string = this.storage.get("role");
    this.httpHeaders = this.httpHeaders.set('Authorization', "Bearer " + token.toString());
    this.httpHeaders = this.httpHeaders.set('UserID', userid.toString());
    this.httpHeaders = this.httpHeaders.set('Role', role.toString());
    this.httpHeaders= this.httpHeaders.set("content-type", "application/json"); 
  }

  GetAll(Action: String): Observable<any> {     
    return this.httpclient.get(this.APIURL + Action, { headers: this.httpHeaders })
  }

  GetByCriteria(Action: String, ID: number): Observable<any> {    
    return this.httpclient.get(this.APIURL + Action + "/" + ID, { headers: this.httpHeaders })
  }

  Post(Action: String, model: any) {    
    return this.httpclient.post(this.APIURL + Action, model, { headers: this.httpHeaders })
  }

  Postfile(Action: String, model: any) {
    return this.httpclient.post(this.APIURL + Action, model, { reportProgress: true })
  }

  Put(Action: String, model: any) {    
    debugger;
    return this.httpclient.put(this.APIURL + Action + "/" + 1, model, { headers: this.httpHeaders })
  }

  Delete(Action: String, ID: number) {    
    return this.httpclient.delete(this.APIURL + Action + "/" + ID, { headers: this.httpHeaders })
  }

}