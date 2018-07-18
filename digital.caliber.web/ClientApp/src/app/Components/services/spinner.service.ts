import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface SpinnerState {
    show: boolean;
}

@Injectable()
export class SpinnerService {
    private spinnerSubject = new Subject<SpinnerState>()
    private requestOpen = 0;
    spinnerState = this.spinnerSubject.asObservable();

    constructor() { }

    show() {
        this.requestOpen++;
        this.spinnerSubject.next(<SpinnerState>{ show: true });
    }

    hide() {
        this.requestOpen--;

        if(this.requestOpen <= 0){
            let that = this;
            setTimeout(function(): any {
                that.spinnerSubject.next(<SpinnerState>{ show: false });
            }, 300);
        }
    }
}