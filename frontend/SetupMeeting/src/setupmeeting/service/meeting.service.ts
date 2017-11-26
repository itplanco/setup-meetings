import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Meeting, Invite, Attende } from '../model/meeting';
import { Http } from '@angular/http';
import { Payment } from '../model/payments';
// import { RequestOptions } from 'http';
import 'rxjs/add/operator/toPromise';
@Injectable()
export class MeetingSevice{
    headers: Headers;
    options: any;

    constructor(private http: Http) {
        this.headers = new Headers({ 'Content-Type': 'application/json'});
        
        // this.options = new RequestOptions({ headers: this.headers });
    }

    getMeeting(id:string): Promise<Meeting> {
        return this.http
            .get('./assets/meeting.json')
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getInvate(id:string): Promise<Invite[]> {
        return this.http
            .get('./assets/invate.json')
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    putInvate(meetingId:string,userId:string,rsvp:boolean){
        
    }

    getAttend(id:string): Promise<Attende[]> {
        return this.http
            .get('./assets/attend.json')
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    postAttend(meetingId:string,attendees:Attende[]){

    }


    getPayment(meetingid:string): Promise<Payment>{
        return this.http
        .get('./assets/payment.json')
        .toPromise()
        .then(response => response.json())
        .catch(this.handleError);
    }

    putPayment(payment:Payment){

    }

    

    

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }

}