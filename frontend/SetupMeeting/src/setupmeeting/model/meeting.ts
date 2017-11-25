export class Meeting{
    ID:string;
    Name:string;
    Member:Member[] = [];
    Status:MeetingStatus =0;
}

export class Member{
    MemberID:string;
    Name:string;
    PresenceOrAbsence:boolean =false;     //出欠
    Attendance:boolean = false;　　　　　　//出席
    Collection:number = 0;                //徴収
}

export enum MeetingStatus{
    Before=0,
    Open=1    
}