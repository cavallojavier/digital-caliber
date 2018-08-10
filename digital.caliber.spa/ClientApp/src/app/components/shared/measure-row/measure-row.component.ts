import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { measuresResume } from '../../../models/measures';
import { MeasuresService } from '../../../services/measures.service';

@Component({
    selector: 'measure-row',
    templateUrl: './measure-row.component.html',
    styleUrls: ['./measure-row.component.scss']
})

export class MeasureRowComponent implements OnInit {

    @Input() measure: measuresResume;
    @Output() onDeletingComplete = new EventEmitter<number>();

    constructor(private _router: Router, private _measureService: MeasuresService) { }

    ngOnInit(): void {
    }

    edit(id:number){
        this._router.navigate(['./orthodontics/measures', id]);  
    }

    viewResults(id:number){
        this._router.navigate(['./orthodontics/results', id]);  
    }

    delete(id:number){

        this._measureService.deleteMeasure(id).toPromise()
        .then(() => {
            this.onDeletingComplete.emit(id);
        })
        .catch(err => {
            this.onDeletingComplete.emit(-1);
        });
    }
}