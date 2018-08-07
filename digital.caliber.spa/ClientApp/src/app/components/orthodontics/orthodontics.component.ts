import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { measuresResume } from '../../models/measures';
import { MeasuresService } from '../../services/measures.service';

@Component({
    selector: 'orthodontics',
    templateUrl: './orthodontics.component.html',
    styleUrls: ['./orthodontics.component.scss']
})
/** candidate-home component*/
export class OrthodonticsComponent implements OnInit {

    measures: measuresResume[];
    filteredMeasures:measuresResume[];
    searchText: string;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _measureService: MeasuresService) {
                    this.measures = []
                }

    /** Called by Angular after candidate.join component initialized */
    ngOnInit(): void {
        this._measureService.getMeasures(10)
        .toPromise()
        .then(data => {
            this.measures = data;
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
}