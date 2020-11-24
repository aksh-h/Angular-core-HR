import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment as ENV } from "../../environments/environment";
import { Observable } from "rxjs";
import { LocalStorageService } from 'angular-web-storage';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  
  constructor(private httpclient: HttpClient, private storage: LocalStorageService) {
    try{
    var token : string = this.storage.get("token");
    var userid : string = this.storage.get("userid");
    var role : string = this.storage.get("role");
    this.httpHeaders = this.httpHeaders.set('Authorization', "Bearer " + token.toString());
    this.httpHeaders = this.httpHeaders.set('UserID', userid.toString());
    this.httpHeaders = this.httpHeaders.set('Role', role.toString());
    this.httpHeaders= this.httpHeaders.set("content-type", "application/json"); 
    }
    catch
    {

    }
  }

  Controller = "Login/"
  APIURL = `${ENV.ApiURL}` + this.Controller;
  httpHeaders = new HttpHeaders();

  GetAll(Action: String): Observable<any> {
    this.httpHeaders.set("content-type", "application/json");    
    return this.httpclient.get(this.APIURL + Action, { headers: this.httpHeaders })
  }

  GetByCriteria(Action: String, ID: number): Observable<any> {
    this.httpHeaders.set("content-type", "application/json");
    return this.httpclient.get(this.APIURL + Action + "/" + ID, { headers: this.httpHeaders })
  }

  Post(Action: String, model: any) : Observable<any> {
    this.httpHeaders.set("content-type", "application/json");
    return this.httpclient.post(this.APIURL + Action, model, { headers: this.httpHeaders })
  }

  Put(Action: String, model: any) {
    this.httpHeaders.set("content-type", "application/json");
    return this.httpclient.put(this.APIURL + Action, model, { headers: this.httpHeaders })
  }

  Delete(Action: String, ID: number) {
    this.httpHeaders.set("content-type", "application/json");
    return this.httpclient.delete(this.APIURL + Action, { headers: this.httpHeaders  })
  }
}
