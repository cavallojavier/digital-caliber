import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgForm } from '@angular/forms';

import { MeasuresService } from '../../../services/measures.service';

import { measures } from '../../../models/measures';

@Component({
    selector: 'measures',
    templateUrl: './measures.component.html',
    styleUrls: ['./measures.component.scss']
})
/** candidate-home component*/
export class MeasureComponent implements OnInit {
    errors: string;
    measures: measures;
    step: number = 1;
    @ViewChild(NgForm) form;
    private sub: any;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _measureService: MeasuresService) { 
                    this.errors = '';
                    this.measures = new measures();
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        this.sub = this._routeQuery.params.subscribe((params: Params) => {
            
            let measureId = +params['id'];
            if(measureId){
                this._measureService.getMeasure(measureId)
                .toPromise()
                .then(data => {
                    this.measures = data;
                });
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
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
        .then((data: number) => {
            console.log('measure successfully saved!');
            this._router.navigate(['./orthodontics/results', data]);  
        })
        .catch((err: any) => {
            this.errors = err.error;
        });
    }
}