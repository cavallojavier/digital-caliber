import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Form } from '@angular/forms';
import { SpinnerService } from '../../services/spinner.service';
import { MeasuresService } from '../../services/measures.service';

import { measures } from '../../models/measures';

@Component({
    selector: 'measures',
    templateUrl: './measures.component.html',
    styleUrls: ['./measures.component.scss']
})
/** candidate-home component*/
export class MeasureComponent {
    errors: string;
    measures: measures;
    step: number = 1;
    @ViewChild('f') form: Form;
    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _spinner: SpinnerService,
                private _measureService: MeasuresService) { 
                    this.errors = '';
                    this.measures = new measures();
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        
    }

    //Steps: 1: (teeth) - 2: (mouth) - 3: (results)
    increaseStep(e): void{
        e.preventDefault();
        if(!this.form.valid){
            this.form.submitted = true;
            return;
        }

        this.form.submitted = false;
        this.step++;
    }
    decreaseStep(e): void{
        e.preventDefault();
        this.step--;
    }

    save({ value, valid }: { value: any, valid: boolean }) {
        if(!valid) {
           return;
        }
        
        this._measureService.saveMeasure(this.measures)
        .toPromise()
        .then(() => {
            console.log('measure successfully saved!');
        })
        .catch((err: any) => {
            this.errors = err.error;
        });
    }
}