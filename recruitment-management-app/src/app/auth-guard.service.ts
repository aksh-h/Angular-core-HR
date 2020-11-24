import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { LocalStorageService } from 'angular-web-storage';


@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(
    private router: Router, private storage: LocalStorageService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isLoggedIn = this.storage.get("IsLoggedIn")
    if (isLoggedIn) {
      return true;
    } else {
      return false;
    }
  }

}