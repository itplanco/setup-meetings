import { Injectable } from '@angular/core';
// import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
// import { RequestOptions } from 'http';
@Injectable()
class LoginSevice{
    headers: Headers;
    // options: RequestOptions;

    // constructor(private http: Http) {
    //     // this.headers = new Headers({ 'Content-Type': 'application/json', 
    //     // 'Accept': 'q=0.8;application/json;q=0.9' });
        
    //     // this.options = new RequestOptions({ headers: this.headers });
    // }

    // getLoginInfo(url: string): Promise<any> {
    //     return this.http
    //         .get(url, this.options)
    //         .toPromise()
    //         .then(this.extractData)
    //         .catch(this.handleError);
    // }

    

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }

}