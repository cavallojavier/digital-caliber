import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { SpinnerService } from '../../services/spinner.service';
import { AccountService } from '../../services/account.service';
import { AccountUser } from '../../models/account.interface';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
/** candidate-home component*/
export class RegisterComponent {
    errors: string;
    user: AccountUser;
    password: string;
    passwordVal: string;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _accountService: AccountService,
                private _spinner: SpinnerService) { 
                    this.errors = '';

                    this.password = '';
                    this.passwordVal = '';
                    this.user = {
                        id: '',
                        email: '',
                        firstName: '',
                        lastName: '',
                        formattedName: ''
                    };
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
    }

    submitLogin({ value, valid }: { value: any, valid: boolean }) {
        debugger;
        if(!valid) return;
        
        this._accountService.register(this.user, this.password);

    }
}