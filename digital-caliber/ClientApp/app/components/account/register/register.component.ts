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
    private errors: string;
    private user: AccountUser;
    private password: string;
    private passwordVal: string;

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
        debugger;

        if(!valid || this.password !== this.passwordVal) {
            return;
        }
        
        this._accountService.register(this.user, this.password);
    }
}