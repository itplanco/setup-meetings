import { Component, Input, SimpleChanges } from '@angular/core';
import { Payment } from '../../model/payments';
import { MeetingSevice } from '../../service/meeting.service';
import { MeetingsApi } from '../../index';
import * as models                                           from '../../model/models';
@Component({
    selector: 'payment-pagearea',
    templateUrl: './payment-pagearea.component.html',
    styleUrls: ['./payment-pagearea.component.css']
})
export class PaymentPageAreaComponent {
    @Input() meetingId:string = "";
    // payment:Payment = new Payment();
    payment:models.MeetingPaymentResponse = {};
    displayPayment:boolean = false;

    constructor(private meetingSevice:MeetingSevice,private meetingsApi:MeetingsApi){

    }

    //画面初期表示処理
    ngOnChanges(changes:SimpleChanges){
        if(changes && changes['meetingId'] && this.meetingId != null ){
            this.getPayment(this.meetingId);
        }
    }

    getPayment(id:string){
        // this.meetingSevice.getPayment(id).then(response => {
        //     this.payment = response;
        //     if(!this.payment){
        //         this.payment = new Payment();
        //         this.displayPayment = false;
        //     }else{
        //         this.displayPayment = true;
        //     }
        // })
        this.meetingsApi.getPayment(id).subscribe(response =>{
            this.payment = response;
                if(!this.payment){
                this.payment = {};
                this.displayPayment = false;
            }else{
                this.displayPayment = true;
            }
        });
        
    }

    onClick(){
        // this.meetingSevice.putPayment(this.payment);
        // this.getPayment(this.meetingId);
        var requestModel = {"totalPrice": this.payment.totalPrice}
        this.meetingsApi.updatePaymentInfo(this.meetingId,requestModel).subscribe(response => {
            this.getPayment(this.meetingId);
        });
    }
}
