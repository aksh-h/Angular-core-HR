import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MasterService } from '../masters/master.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  // -------- Google_API_Key -------------------------
  API_KEY = 'AIzaSyCxyr-N6yHjiLmsFqaHRM_rbWvKgyx2y80';
  Place_ID: string;
  Action = "GetMapAddress";

  constructor(private httpClient: HttpClient, private service: MasterService) { }

  public getURL(place_id){
    debugger;
    this.Place_ID = place_id;
    return this.httpClient.get(`https://maps.googleapis.com/maps/api/place/details/json?key=${this.API_KEY}&placeid=${this.Place_ID}`);
  }

  public getAddress(){
    debugger;
    return this.service.GetAll(this.Action);
}
}
