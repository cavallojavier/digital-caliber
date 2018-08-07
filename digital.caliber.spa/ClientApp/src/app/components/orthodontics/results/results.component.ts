import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { MeasuresService } from '../../../services/measures.service';
import { measuresResult } from '../../../models/results';

@Component({
    selector: 'results',
    templateUrl: './results.component.html',
    styleUrls: ['./results.component.scss']
})
/** candidate-home component*/
export class ResultsComponent implements OnInit {
    measureId: number;
    result: measuresResult;
    private sub: any;
    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _location: Location,
                private _measureService: MeasuresService) { 
                    this.result = new measuresResult;
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        this.sub = this._routeQuery.params.subscribe((params: Params) => {
            
            this.measureId = +params['id'];
            if(!this.measureId){
                this._location.back();
            }

            this._measureService.getResults(this.measureId)
            .toPromise()
            .then(data => {
                this.result = data;
            })
            .catch(() => {
                this._location.back();
            })
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    exportPdf(){

    }

    exportExcel(){

    }

    goBack(){
        this._location.back();
    }
}