import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Response, RequestOptions, ResponseOptions } from '@angular/http';

import { ConfigService } from './config.service';
import { BaseService } from './base.service';
import { SpinnerService } from './spinner.service';

import { AccountUser } from '../models/account.interface';

import { Observable } from 'rxjs';
@Injectable()

export class AccountService extends BaseService implements OnInit{
    private baseUrl: string = '';
    private loggedIn = false;
    public loggedUser: AccountUser | null;
    public redirectUrl: string;

    constructor(private http: HttpClient, private configService: ConfigService, private spinner: SpinnerService) {
        super(spinner);

        this.redirectUrl = '';
        this.loggedIn = !!localStorage.getItem('auth_token');
        this.baseUrl = configService.getApiURI();
        this.loggedUser = {
            id: '',
            email: '',
            firstName: '',
            lastName: '',
            formattedName: ''
        };
    }

    ngOnInit(): void {
        
    }

    logout() {
        
        return this.http.post(this.baseUrl + '/account/logout', '')
        .subscribe({
            complete: () => {
                localStorage.clear();
                this.loggedIn = false;
                this.loggedUser = null;
                this.spinner.hide();
            }
        });
    }

    public getAuthToken(): string {
        return localStorage.getItem('auth_token') || '';
    }
    
    isLoggedIn(): boolean{
        return eval(localStorage.getItem('isLoggedIn') || 'false');
    }

    register(account: AccountUser, password: string){

        debugger;
        let user = {
            firstName: account.firstName,
            lastName: account.lastName,
            email: account.email,
            password: password
        };

        let body = JSON.stringify(user);
        let url = '/account/register';

        this.spinner.show();
        return this.http.post(this.baseUrl + url, body)
        .subscribe({
            next: (auth: any) => {
                localStorage.setItem('uId', auth.uId);
                localStorage.setItem('auth_token', auth.auth_token);

                this.loggedUser = {
                    id: auth.uId,
                    firstName: auth.firstName,
                    lastName: auth.lastName,
                    formattedName: auth.formattedName,
                    email: auth.email
                }

                this.loggedIn = true;

                localStorage.setItem("user",  JSON.stringify(this.loggedUser));
                localStorage.setItem('isLoggedIn', 'true');

                return true;
            },
            error: (err: any) => {
                this.handleError;
                return false;
            },
            complete: () => {
                this.spinner.hide();
            }
        });
    }

    public login(email: string, password: string): any{
        let user = {
            email: email,
            password: password
        };

        let body = JSON.stringify(user);
        let url = '/account/login';

        this.spinner.show();
        return this.http.post(this.baseUrl + url, body)
        .subscribe({
            next: (auth: any) => {
                localStorage.setItem('uId', auth.uId);
                localStorage.setItem('auth_token', auth.auth_token);

                this.loggedUser = {
                    id: auth.uId,
                    firstName: auth.firstName,
                    lastName: auth.lastName,
                    formattedName: auth.formattedName,
                    email: auth.email
                }

                this.loggedIn = true;

                localStorage.setItem("user",  JSON.stringify(this.loggedUser));
                localStorage.setItem('isLoggedIn', 'true');
            },
            error: (err: any) => {
                this.handleError
            },
            complete: () => {
                this.spinner.hide();
            }
        });
    }
}