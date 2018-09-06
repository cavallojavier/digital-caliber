import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { MeasuresService } from '../../../services/measures.service';
import { measuresResult } from '../../../models/results';
import { DateFormatPipe } from '../../shared/pipes/date-transform.pipe';

//import html2canvas from 'html2canvas';

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
    @ViewChild('downloadZipLink') private downloadZipLink: ElementRef;

    constructor(private _router: Router,
                private _routeQuery: ActivatedRoute,
                private _location: Location,
                private _measureService: MeasuresService,
                private _datePipe: DateFormatPipe) { 
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

    async exportPdf(){

        const blob = await this._measureService.getPdf(this.measureId);
        const url = window.URL.createObjectURL(blob);
        const link = this.downloadZipLink.nativeElement;
        
        link.href = url;
        let date = this._datePipe.transform(this.result.dateMeasure);
        link.download = this.result.patientName + ' - ' + date.toString() + '.pdf';
        link.click();
        
        window.URL.revokeObjectURL(url);
    }
  
    exportExcel(){

    }

    goBack(){
        this._router.navigate(['./orthodontics']);  
    }
}
