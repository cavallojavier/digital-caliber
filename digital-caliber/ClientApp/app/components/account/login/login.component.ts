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
    private errors: string = '';
    private mail: string = 'cavallojavier@gmail.com';
    private password: string = '';
    private remember: boolean = false;
    private firstName1: string = '';
    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _accountService: AccountService) { 
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
    }

    login({ value, valid }: { value: any, valid: boolean }) {
        if(!valid) {
            return;
        }
        
        this._accountService.login(this.mail, this.password, this.remember)
        .toPromise()
        .then(() => {
            this._router.navigate(['/home']);
        })
        .catch((err: any) => {
        });
    }
}