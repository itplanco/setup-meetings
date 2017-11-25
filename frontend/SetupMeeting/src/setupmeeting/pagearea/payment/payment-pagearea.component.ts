import { Component, Input } from '@angular/core';
import { Payments } from '../../model/payments';

@Component({
    selector: 'payment-pagearea',
    templateUrl: './payment-pagearea.component.html',
    styleUrls: ['./payment-pagearea.component.css']
})
export class PaymentPageAreaComponent {
    @Input() payment:Payments = new Payments();

    //画面初期表示処理
    ngOnChanges(){

    }

    onClick(){
        
    }
}
