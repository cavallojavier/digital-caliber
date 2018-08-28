import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { MeasuresService } from '../../../services/measures.service';
import { measuresResult } from '../../../models/results';

import html2canvas from 'html2canvas';

@Component({
    selector: 'result-pdf',
    templateUrl: './result-pdf.component.html',
    styleUrls: ['./result-pdf.component.scss']
})
/** candidate-home component*/
export class ResultPdfComponent implements OnInit {
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

    goBack(){
        this._router.navigate(['./orthodontics']);  
    }

    async exportPdf(){
        /*
        var doc = new jsPDF({
            orientation: 'portrait',
            //unit: 'in',
            //format: [4, 2]
        })
        
        var data = document.getElementById('printable');  
        debugger;
        doc.fromHTML(data);

        doc.save('two-by-four.pdf');
        */
        var data = document.getElementById('printable');  
        html2canvas(data).then(canvas => {  
            // Few necessary setting options  
            var imgWidth = 208;   
            var pageHeight = 295;    
            var imgHeight = canvas.height * imgWidth / canvas.width;  
            var heightLeft = imgHeight;  
        
            const contentDataURL = canvas.toDataURL('image/png')  
            let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
            var position = 0;  
            pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)  
            pdf.save('MYPdf.pdf'); // Generated PDF   
        });  
        /*
        const blob = await this._measureService.exportPdf(this.measureId);
        const url = window.URL.createObjectURL(blob);
        const link = this.downloadZipLink.nativeElement;
        
        debugger;
        link.href = url;
        let date = this._datePipe.transform(this.result.dateMeasure);
        link.download = this.result.patientName + ' - ' + date.toString() + '.pdf';
        link.click();
        
        window.URL.revokeObjectURL(url);
        */
    }

}