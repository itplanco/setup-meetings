import { Component } from '@angular/core';
import { MeetingSevice } from '../../service/meeting.service';
import { Meeting, MeetingStatus, Invite, Attende } from '../../model/meeting';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'setupmeeting-page',
    templateUrl: './setupmeeting-page.component.html',
    styleUrls: ['./setupmeeting-page.component.css']
})
export class SetupMeetingPageComponent {
    displayFlg:boolean = false;                       //表示フラグ
    meeting:Meeting = new Meeting();　　　　　　　　　　//会
    meetingId:string = ""; 　　　　　　　　　　　　　　　//会ID
    constructor(private meetingSevice:MeetingSevice,
    private route :ActivatedRoute){

    }

    //画面初期表示処理
    ngOnInit(){
        this.route.params.subscribe(params => {
            this.meetingId = params['meetingid'];
            this.getMeeting();
         });
    }

    //会取得処理
    getMeeting(){
        this.meetingSevice.getMeeting(this.meetingId).then(response => {
            this.meeting = response;
            if(!this.meeting.status){
                this.meeting.status = MeetingStatus.Before;
            }
        })
    }

    
    //クリックイベント
    onClick(){
        if(this.meeting.status == MeetingStatus.Open){
            this.meeting.status = MeetingStatus.Before;
            this.meetingSevice.getInvate(this.meetingId).then(response => {
                this.meeting.invitees =  response;
            })
        }else{
            this.meeting.status = MeetingStatus.Open;
            this.meetingSevice.getAttend(this.meetingId).then(response => {
                this.meeting.attendees =  response;
            })
        }
    }

    onInviteClick(invite:Invite,rsvp:boolean ){
        for(var i in this.meeting.invitees){
            if(this.meeting.invitees[i].userId == invite.userId){
                this.meeting.invitees[i].rsvp = rsvp; 
            }
        }
    }

    onAttendClick(attende:Attende,attend:boolean){
        for(var i in this.meeting.attendees){
            if(this.meeting.attendees[i].userId == attende.userId){
                this.meeting.attendees[i].attend = attend; 
            }
        }
    }

    onPayedClick(attende:Attende,payed:boolean){
        for(var i in this.meeting.attendees){
            if(this.meeting.attendees[i].userId == attende.userId){
                this.meeting.attendees[i].payed = payed; 
            }
        }
    }
}
