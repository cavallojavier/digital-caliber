import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Configuration } from '../models/configuration';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ConfigService {
    private readonly _configUrlPath: string = 'ClientConfiguration';
    private _configData: Configuration = new Configuration();
    _apiURI: string = '/api';
    constructor(private _http: Http) {
    }

    // Call the ClientConfiguration endpoint, deserialize the response and store it in this.configData.
    loadConfigurationData() {
        return this._http.get(`${this._apiURI}/${this._configUrlPath}`)
        .toPromise()
        .then((response: Response) => {
            this._configData = response.json();
            return this._configData;
        });
    }

    // A helper property to return the config object
    get config(): Configuration {
        return this._configData;
    }

    getApiURI() {
        return this._apiURI;
    }
}