import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { SpinnerState, SpinnerService } from '../../services/spinner.service';

@Component({
    selector: 'spinner',
    templateUrl: './spinner.component.html',
    styleUrls: ['./spinner.component.scss']
})
/** spinner component*/
export class SpinnerComponent implements OnDestroy, OnInit {
    visible = false;
    private _spinnerStateChanged: Subscription;

    /** spinner ctor */
    constructor(private _spinnerService: SpinnerService) {
        this._spinnerStateChanged = new Subscription();
    }

    ngOnInit(): void {
        console.log(this.visible);
        this._spinnerStateChanged = this._spinnerService.spinnerState
            .subscribe((state: SpinnerState) => {
                this.visible = state.show;
            });
    }

    ngOnDestroy(): void {
        this._spinnerStateChanged.unsubscribe();
    }
}