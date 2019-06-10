import { Injectable, InjectionToken, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { ImagePostRequest } from '../models/ImagePostRequest';

@Injectable()
export class AzureToolkitService {
    private originUrl: string;

    constructor(private http: HttpClient, @Inject('BASE_URL')originUrl: string) {
        //console.log(`13 -- ${originUrl}`)
        this.originUrl = originUrl;
    }

    public saveImage(imagePostRequest: ImagePostRequest): Observable<boolean> {
        //console.log(imagePostRequest);
        //console.log(`${this.originUrl}/api/images`);
        return this.http.post(`${this.originUrl}api/images`, imagePostRequest)            
            .map(response => {
               //console.log(response);
               return true;
            })
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}
