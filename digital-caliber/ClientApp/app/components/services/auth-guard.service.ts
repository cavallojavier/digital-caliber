import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AccountService } from './account.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private accountService: AccountService, private _router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        debugger;
        let url: string = state.url

        return this.checkLogin(url);
    }

    checkLogin(url: string): boolean {
        
        if (this.accountService.isLoggedIn()) { return true; }

        // Store the attempted URL for redirecting
        this.accountService.redirectUrl = url;

        // Redirect candidate sign-in
        if (url.startsWith("/candidate")) {
            this._router.navigate(['/candidate/sign-in']);
        } else {
            this._router.navigate(['/agent/sign-in']);
        }
       
        return true;
    }
}