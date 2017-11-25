import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'password-page',
    templateUrl: './password-page.component.html',
    styleUrls: ['./password-page.component.css']
})
export class PassWordPageComponent {

    constructor(private router:Router){

    }
    //クリックイベント
    onClick() {
        this.router.navigate(['/meetings',1]);
    }
}
