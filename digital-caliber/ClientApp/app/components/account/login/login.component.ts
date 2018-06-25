import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SpinnerService } from '../../services/spinner.service';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
/** candidate-home component*/
export class LoginComponent {
    errors: string;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _spinner: SpinnerService) { 
                    this.errors = '';
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
    }
}