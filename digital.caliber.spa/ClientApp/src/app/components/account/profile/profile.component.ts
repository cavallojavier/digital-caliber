import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../services/account.service';
import { AccountUser, AccountUpdate, PasswordUpdate } from '../../../models/account.interface';

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})

export class ProfileComponent implements OnInit {
    errors: string;
    passerrors: string;
    passwordVal: string;

    mailInvalid: boolean = false;
    passInvalid: boolean = false;
    passVerifyInvalid: boolean = false;

    accountUpdate: AccountUpdate;
    passwordUpdate: PasswordUpdate;
    
    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _accountService: AccountService) { 
                    this.errors = '';
                    this.passerrors = '';
                    this.passwordVal = '';
                    this.accountUpdate = {
                        email: '',
                        firstName: '',
                        lastName: ''
                    };
                    this.passwordUpdate = {
                        newPassword: '',
                        currentPassword: ''
                    };
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        let user = this._accountService.getLoggedUser();
        this.accountUpdate.firstName = user.firstName;
        this.accountUpdate.lastName = user.lastName;
        this.accountUpdate.email = user.email;
    }

    updateProfile({ value, valid }: { value: any, valid: boolean }) {
        this.mailInvalid = !this.validateEmail(this.accountUpdate.email);

        if(!valid || this.mailInvalid) {
            return;
        }
        
        this._accountService.updateAccount(this.accountUpdate)
        .toPromise()
        .then(() => {
            this._router.navigate(['/orthodontics']);
        })
        .catch((err: any) => {
            this.errors = err.error;
        });
    }

    updatePassword({ value, valid }: { value: any, valid: boolean }) {
        this.passInvalid = this.passwordUpdate.newPassword !== this.passwordVal;

        if(!valid || this.passInvalid) {
            return;
        }
        
        this._accountService.updatePassword(this.passwordUpdate)
        .toPromise()
        .then(() => {
            this._router.navigate(['/orthodontics']);
        })
        .catch((err: any) => {
            debugger;
            this.passerrors = err.error;
        });
    }

    validateEmail(value: string): boolean{
        let emailRegex = new RegExp("^([A-Z|a-z|0-9](\.|_){0,1})+[A-Z|a-z|0-9]\@([A-Z|a-z|0-9])+((\.){0,1}[A-Z|a-z|0-9]){2}\.[a-z]{2,3}$");
        return emailRegex.test(value);
    }
}