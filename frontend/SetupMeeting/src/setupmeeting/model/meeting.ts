export class Meeting{
    meetingId:string;
    name:string;
    startAt:Date;
    endAt:Date;
    organizers:OrganiZer[];
    invitees:Invite[];
    attendees:Attende[];
    sponsers:OrganiZer[];
    status:MeetingStatus =0;

    constructor(){
        this.organizers = [];
        this.invitees = [];
        this.attendees = [];
        this.sponsers = [];
    }
}

export class OrganiZer{
    userId:string;
    userName:string;
    organizationId:string;
    organizationName:string;
}

export class Invite{
    userId:string;
    userName:string;
    organizationId:string;
    organizationName:string;
    rsvp:boolean;

    constructor(){
        this.rsvp = false;
    }
}

export class Attende{
    userId:string;
    userName:string;
    organizationId:string;
    organizationName:string;
    attend:boolean;
    payed:boolean;

    constructor(){
        this.attend = false;
    }
}


export enum MeetingStatus{
    Before=0,
    Open=1    
}