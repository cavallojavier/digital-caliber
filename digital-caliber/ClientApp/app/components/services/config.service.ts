import { Injectable, Inject } from '@angular/core';

import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { Configuration } from '../models/configuration';

@Injectable()
export class ConfigService {
    private readonly _configUrlPath: string = 'ClientConfiguration';
    private _configData: Configuration;
    _apiURI: string;

    constructor(private _http: Http) {
        this._apiURI = '/api';
    }

    // Call the ClientConfiguration endpoint, deserialize the response and store it in this.configData.
    loadConfigurationData(): Promise<Configuration> {
        return this._http.get(`${this._apiURI}/${this._configUrlPath}`)
            .toPromise()
            .then((response: Response) => {
                this._configData = response.json();
                return this._configData;
            })
            .catch(err => {
                return Promise.reject(err);
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