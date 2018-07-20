import { Observable } from 'rxjs';

import { SpinnerService } from './spinner.service';

export abstract class BaseService {

    constructor(public baseSpinner: SpinnerService) { }

    protected handleError(error: any) {
        debugger;
        this.baseSpinner.hide();
        if(error.headers)
        {
            var applicationError = error.headers.get('Application-Error');
            if (applicationError) {
                return Observable.throw(applicationError);
            }
        }
            
        var modelStateErrors: string = '';
        var serverError = error;

        if (!serverError.type) {
            for (var key in serverError) {
                if (serverError[key])
                    modelStateErrors += serverError[key] + '\n';
            }
        }

        //modelStateErrors = modelStateErrors == '' ? null : modelStateErrors;
        return Observable.throw(modelStateErrors || 'Server error');
    }
}