import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Injectable } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { AccountUser } from '../../models/account.interface';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
/** candidate-home component*/
export class RegisterComponent implements OnInit {
    errors: string;
    user: AccountUser;
    password: string;
    passwordVal: string;

    mailInvalid: boolean = false;
    passInvalid: boolean = false;
    passVerifyInvalid: boolean = false;
    
    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _accountService: AccountService) { 
                    this.errors = '';
                    this.password = '';
                    this.passwordVal = '';
                    this.user = {
                        id: '',
                        email: '',
                        firstName: '',
                        formattedName: '',
                        lastName: ''
                    };
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        
    }

    submitRegister({ value, valid }: { value: any, valid: boolean }) {
        this.mailInvalid = !this.validateEmail(this.user.email);
        this.passInvalid = this.password !== this.passwordVal;

        if(!valid || this.passInvalid || this.mailInvalid) {
            return;
        }
        
        this._accountService.register(this.user, this.password)
        .toPromise()
        .then(() => {
            this._router.navigate(['/home']);
        })
        .catch((err: any) => {
            this.errors = err;
        });
    }

    validateEmail(value: string): boolean{
        let emailRegex = new RegExp("^([A-Z|a-z|0-9](\.|_){0,1})+[A-Z|a-z|0-9]\@([A-Z|a-z|0-9])+((\.){0,1}[A-Z|a-z|0-9]){2}\.[a-z]{2,3}$");
        return emailRegex.test(value);
    }
}