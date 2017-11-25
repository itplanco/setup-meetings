import { Component, Input, SimpleChanges } from '@angular/core';
import { Payment } from '../../model/payments';
import { MeetingSevice } from '../../service/meeting.service';

@Component({
    selector: 'payment-pagearea',
    templateUrl: './payment-pagearea.component.html',
    styleUrls: ['./payment-pagearea.component.css']
})
export class PaymentPageAreaComponent {
    @Input() meetingId:string = "";
    payment:Payment = new Payment();
    displayPayment:boolean = false;

    constructor(private meetingSevice:MeetingSevice){

    }

    //画面初期表示処理
    ngOnChanges(changes:SimpleChanges){
        if(changes && changes['meetingId'] && this.meetingId != null ){
            this.getPayment(this.meetingId);
        }
    }

    getPayment(id:string){
        this.meetingSevice.getPayment(id).then(response => {
            this.payment = response;
            if(!this.payment){
                this.payment = new Payment();
                this.displayPayment = false;
            }else{
                this.displayPayment = true;
            }
        })
    }

    onClick(){
        this.meetingSevice.putPayment(this.payment);
        this.getPayment(this.meetingId);
    }
}
