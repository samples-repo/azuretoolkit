import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { AzureHttpClient } from './azureHttpClient';
import { BingSearchResponse } from '../models/bingSearchResponse';
import { ComputerVisionRequest, ComputerVisionResponse } from '../models/computerVisionResponse';

@Injectable()
export class CognitiveService {
    bingSearchAPIKey = 'fefff709a8884af39815a5eb56a39b4b';
    computerVisionAPIKey = '10569ab36bd04aed98a84d4d2cb36f5c';

    constructor(private http: AzureHttpClient) { }
    
    searchImages(searchTerm: string): Observable<BingSearchResponse> {
        var results = this.http.get(`https://api.cognitive.microsoft.com/bing/v7.0/images/search?q=${searchTerm}`, this.bingSearchAPIKey);        
        //results.subscribe((data: any) => {console.log(data)});
        return results
                .map(response => response as BingSearchResponse)
                .catch(this.handleError);
    }

    analyzeImage(request: ComputerVisionRequest): Observable<ComputerVisionResponse> {
        return this.http.post('https://westus2.api.cognitive.microsoft.com/vision/v2.0/analyze?visualFeatures=Description,Tags', this.computerVisionAPIKey, request)
            .map(response => response as ComputerVisionResponse)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}