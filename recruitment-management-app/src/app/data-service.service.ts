import { Injectable } from '@angular/core';
import { LoggedUser } from './models/commonModel';

@Injectable({
  providedIn: 'root'
})
export class DataServiceService {
  public LoggedUser: LoggedUser;
  constructor() { }
}
