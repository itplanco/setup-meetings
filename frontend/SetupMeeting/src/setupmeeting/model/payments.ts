export class Payment{
    totalPrice:number = 0;
    details:PaymentDetails[] = [];
}

export class PaymentDetails{
    userId:string;
    userName:string;
    organizationId:string;
    organizationName:string;
    price:number = 0;
}