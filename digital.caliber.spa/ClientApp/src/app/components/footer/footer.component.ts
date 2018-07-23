import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'shared-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss' ]
})

export class FooterComponent {

    constructor(private _router: Router) {
    }

    goToTerms(){
        this._router.navigate(['/home']);
    }
}
