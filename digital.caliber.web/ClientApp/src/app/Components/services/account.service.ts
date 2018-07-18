import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import { ConfigService } from './config.service';
import { BaseService } from './base.service';
import { SpinnerService } from './spinner.service';

import { AccountUser } from '../models/account.interface';

import { Observable, of } from "rxjs";
import { catchError, map, tap } from 'rxjs/operators';

@Injectable()
export class AccountService extends BaseService implements OnInit{
    private baseUrl: string = '';
    public loggedUser: AccountUser | null;
    public redirectUrl: string;

    constructor(private http: HttpClient, private configService: ConfigService, private spinner: SpinnerService) {
        super(spinner);

        this.redirectUrl = '';
        this.baseUrl = this.configService.getApiURI();
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

    getLoggedUser(): AccountUser | null{
        let usr = localStorage.getItem("user");
        
        if(usr){
            this.loggedUser = JSON.parse(usr);
            return this.loggedUser;
        }

        return null;
    }

    logout(): Observable<any> {
        return this.http.post(this.baseUrl + '/account/logout', '')
        .pipe(
            map(() => {
                localStorage.clear();
                this.loggedUser = null;
            }),
            catchError(this.handleError)
        );
        
    }

    public getAuthToken(): string {
        return localStorage.getItem('auth_token') || '';
    }
    
    isLoggedIn(): boolean{
        return eval(localStorage.getItem('isLoggedIn') || 'false');
    }

    register(account: AccountUser, password: string):Observable<any>{
        let url = this.baseUrl + '/account/register';
        
        this.spinner.show();

        return this.http.post<AccountUser>(url, {
            firstName: account.firstName,
            lastName: account.lastName,
            email: account.email,
            password: password
            })
        .pipe(
            map((auth:any) => {
                //let auth = JSON.parse(res);
                localStorage.setItem('uId', auth.uId);
                localStorage.setItem('auth_token', auth.auth_token);

                this.loggedUser = {
                    id: auth.uId,
                    firstName: auth.firstName,
                    lastName: auth.lastName,
                    formattedName: auth.formattedName,
                    email: auth.email
                }

                localStorage.setItem("user",  JSON.stringify(this.loggedUser));
                localStorage.setItem('isLoggedIn', 'true');
            }),
            catchError(this.handleError)
        );
    }

    public login(email: string, password: string, remember: boolean): Observable<any>{
        let user = {
            email: email,
            password: password
        };

        let body = JSON.stringify(user);
        let url = '/account/login';

        this.spinner.show();
        return this.http.post(this.baseUrl + url, {email: email,
            password: password,
            rememberMe: remember})
        .pipe(
          map((auth:any) => {
                //let auth = JSON.parse(res);
                localStorage.setItem('uId', auth.uId);
                localStorage.setItem('auth_token', auth.auth_token);

                this.loggedUser = {
                    id: auth.uId,
                    firstName: auth.firstName,
                    lastName: auth.lastName,
                    formattedName: auth.formattedName,
                    email: auth.email
                }

                localStorage.setItem("user",  JSON.stringify(this.loggedUser));
                localStorage.setItem('isLoggedIn', 'true');
            }),
            catchError(this.handleError)
        );
    }
}
