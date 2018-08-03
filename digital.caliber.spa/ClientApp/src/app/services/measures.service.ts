import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import { ConfigService } from './config.service';
import { BaseService } from './base.service';
import { SpinnerService } from './spinner.service';

import { measures } from '../models/measures';

import { Observable, of } from "rxjs";
import { catchError, map, tap } from 'rxjs/operators';
import {throwError} from 'rxjs';
import { error } from '../../../node_modules/@angular/compiler/src/util';

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

    getMeasures(): Observable<measures[]>{
        this.spinner.show();

        return this.http.get(this.baseUrl)
        .pipe(
            map((res: any) => {
                return res.json();
            })
        );
    }

    getMeasure(id: number): Observable<measures>{
        this.spinner.show();

        return this.http.get(this.baseUrl + id)
        .pipe(
            map((res: any) => {
                return res.json();
            })
        );
    }

    deleteMeasure(id: number): Observable<measures>{
        this.spinner.show();

        return this.http.delete(this.baseUrl + id)
        .pipe(
            map((res: any) => {
                return res.json();
            })
        );
    }

    saveMeasure(measure: measures): Observable<any>{
        this.spinner.show();

        return this.http.post<measures>(this.baseUrl, { patientName: measure.patientName,
            hcNumber: measure.hcNumber,
            teeths: measure.teeth,
            mouth: measure.mouth
        });
    }
}
