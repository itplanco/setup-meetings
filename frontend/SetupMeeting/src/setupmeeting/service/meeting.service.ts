import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
// import { RequestOptions } from 'http';
import { Meeting } from '../model/meeting';
import { Http } from '@angular/http';
@Injectable()
export class MeetingSevice{
    headers: Headers;
    // options: RequestOptions;

    constructor(private http: Http) {
        // this.headers = new Headers({ 'Content-Type': 'application/json', 
        // 'Accept': 'q=0.8;application/json;q=0.9' });
        
        // this.options = new RequestOptions({ headers: this.headers });
    }

    getMeeting(id:string): Promise<any> {
        return this.http
            .get('./assets/meeting.json')
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }

}