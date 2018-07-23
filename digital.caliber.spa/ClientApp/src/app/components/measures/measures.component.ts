import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { SpinnerService } from '../../services/spinner.service';

@Component({
    selector: 'measures',
    templateUrl: './measures.component.html',
    styleUrls: ['./measures.component.scss']
})
/** candidate-home component*/
export class MeasureComponent {
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