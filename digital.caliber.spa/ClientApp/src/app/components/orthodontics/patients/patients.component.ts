import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { MeasuresService } from '../../../services/measures.service';
import { measuresResume } from '../../../models/measures';


@Component({
    selector: 'patients',
    templateUrl: './patients.component.html',
    styleUrls: ['./patients.component.scss']
})
/** candidate-home component*/
export class PatientsComponent implements OnInit {
    
    measures: measuresResume[];
    filteredMeasures:measuresResume[];
    searchText: string;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _location: Location,
                private _measureService: MeasuresService) { 
                    this.filteredMeasures = this.measures = [];
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        this._measureService.getMeasures(null)
        .toPromise()
        .then(data => {
            this.filteredMeasures = this.measures = data;
        })
    }

    onDeletingComplete(measureId: number){

        if(measureId > 0)
        {
            let itemindex = this.measures.findIndex(x => x.id === measureId);
            this.measures.splice(itemindex, 1);
        }
    }

    filterPatients(event: any){
        this.filteredMeasures = this.measures.filter(x => x.patientName.toLowerCase().includes(this.searchText.toLowerCase()));
    }

    goback(){
        this._location.back();
    }
}