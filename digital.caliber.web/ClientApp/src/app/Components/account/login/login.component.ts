import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AccountService } from '../../services/account.service';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
/** candidate-home component*/
export class LoginComponent implements OnInit {
    errors: string = '';
    mail: string = 'cavallojavier@gmail.com';
    password: string = '';
    remember: boolean = false;

    mailInvalid: boolean = false;
    passInvalid: boolean = false;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _accountService: AccountService) { 
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
    }

    login({ value, valid }: { value: any, valid: boolean }) {
        this.mailInvalid = !this.validateEmail(this.mail);

        if(!valid || this.mailInvalid) {
            return;
        }
        
        this._accountService.login(this.mail, this.password, this.remember)
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