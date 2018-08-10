import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import { saveAs } from 'file-saver';

import { ConfigService } from './config.service';
import { BaseService } from './base.service';
import { SpinnerService } from './spinner.service';

import { measures, measuresResume } from '../models/measures';
import { measuresResult } from '../models/results';

import { Observable, of } from "rxjs";
import { catchError, map, tap } from 'rxjs/operators';
import {throwError} from 'rxjs';
import { error } from '@angular/compiler/src/util';

@Injectable()
export class MeasuresService extends BaseService implements OnInit{
    private baseUrl: string = '';
    public redirectUrl: string;

    constructor(private http: HttpClient, private configService: ConfigService, private spinner: SpinnerService) {
        super(spinner);

        this.redirectUrl = '';
        this.baseUrl = this.configService.getApiURI() + '/measuresapi/';
    }

    ngOnInit(): void {
        
    }

    getMeasures(maxItems: number|null): Observable<measuresResume[]>{
        this.spinner.show();

        return this.http.get(this.baseUrl + 'getMeasures/' + maxItems)
        .pipe(
            map((res: any) => {
                return res;
            })
        );
    }

    getMeasure(id: number): Observable<measures>{
        this.spinner.show();

        return this.http.get(this.baseUrl + id)
        .pipe(
            map((res: any) => {
                return res;
            })
        );
    }

    getResults(id: number): Observable<measuresResult>{
        this.spinner.show();

        return this.http.get(this.baseUrl + 'getresults/' + id)
        .pipe(
            map((res: any) => {
                return res;
            })
        );
    }

    deleteMeasure(id: number): Observable<any>{
        this.spinner.show();

        return this.http.delete(this.baseUrl + id)
        .pipe(
            map((res: any) => {
                return res;
            })
        );
    }

    saveMeasure(measure: measures): Observable<any>{
        this.spinner.show();

        return this.http.post<measures>(this.baseUrl, { 
            id: measure.id,
            patientName: measure.patientName,
            hcNumber: measure.hcNumber,
            teeths: measure.teeths,
            mouth: measure.mouth
        })
        .pipe(
            map((res: any) => {
                return res;
            })
        );;
    }
/*
    public downloadFile() {
        this.http.get('my-api-url', { responseType: 'blob' }).subscribe(blob => {
           saveAs(blob, 'SomeFileDownloadName.someExtensions', {
              type: 'text/plain;charset=windows-1252' // --> or whatever you need here
           }
        }
    }*/

    exportPdf(id: number){
        this.spinner.show();

        return this.http.get(this.baseUrl + 'exportResultsPdf/' + id);
        /*.subscribe(blob => {
            saveAs(blob, 'SomeFileDownloadName.someExtensions', {
               type: 'text/plain;charset=windows-1252' // --> or whatever you need here
            })
         });*/
    }
}
