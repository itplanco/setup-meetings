import { Component } from '@angular/core';
import { MeetingSevice } from '../../service/meeting.service';
import { Meeting, MeetingStatus, Invite, Attende } from '../../model/meeting';
import { ActivatedRoute } from '@angular/router';
import { MeetingsApi } from '../../api/api';
import * as models                                           from '../../model/models';
@Component({
    selector: 'setupmeeting-page',
    templateUrl: './setupmeeting-page.component.html',
    styleUrls: ['./setupmeeting-page.component.css']
})
export class SetupMeetingPageComponent {
    displayFlg:boolean = false;                       //表示フラグ
    meeting:models.MeetingResponse = {};　　　　　　　 //会
    meetingId:string = ""; 　　　　　　　　　　　　　　 //会ID
    status:MeetingStatus = MeetingStatus.Before;     //`会ステータス
    constructor(private meetingSevice:MeetingSevice,
    private route :ActivatedRoute,
    private meetingsApi:MeetingsApi){

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
        // this.meetingSevice.getMeeting(this.meetingId).then(response => {
        //     this.meeting = response;
        //     if(!this.meeting.status){
        //         this.meeting.status = MeetingStatus.Before;
        //     }
        // })

        this.meetingsApi.getMeetingById(this.meetingId).subscribe( response => {
            this.meeting = response;
                if(!this.status){
                    this.status = MeetingStatus.Before;
                }
        } );
    }

    
    //クリックイベント
    onClick(){
        if(this.status == MeetingStatus.Open){
            this.status = MeetingStatus.Before;
            // this.meetingSevice.getInvate(this.meetingId).then(response => {
            //     this.meeting.invitees =  response;
            // })
            this.meetingsApi.getInvitees(this.meetingId).subscribe(response => {
                this.meeting.invitees =  response.invitees;
            });
        }else{
            this.status = MeetingStatus.Open;
            // this.meetingSevice.getAttend(this.meetingId).then(response => {
            //     this.meeting.attendees =  response;
            // })
            this.meetingsApi.getAttendees(this.meetingId).subscribe(response => {
                this.meeting.attendees =  response.attendees;
            });

        }
    }

    onInviteClick(invite:Invite,rsvp:boolean ){
        // for(var i in this.meeting.invitees){
        //     if(this.meeting.invitees[i].userId == invite.userId){
        //         this.meeting.invitees[i].rsvp = rsvp; 
        //     }
        // }
        var rsvpModle = {"response": rsvp};

        this.meetingsApi.updateInviteeRsvp(this.meetingId,invite.userId,rsvpModle).subscribe(response =>{
            this.meetingsApi.getInviteesById(response['meetingId'],response['userId']).subscribe(response =>{
                for(var i in this.meeting.invitees){
                    if(this.meeting.invitees[i].userId == response.userId){
                        this.meeting.invitees[i].rsvp = response.rsvp; 
                    }
                }    
            });
        });
    }

    onAttendClick(attende:Attende,attend:boolean){
        for(var i in this.meeting.attendees){
            if(this.meeting.attendees[i].userId == attende.userId){
                this.meeting.attendees[i].attend = attend; 
            }
        }

        // var attendModle = {"response": attend};
    }

    onPayedClick(attende:Attende,paid:boolean){
        for(var i in this.meeting.attendees){
            if(this.meeting.attendees[i].userId == attende.userId){
                this.meeting.attendees[i].paid = paid; 
            }
        }
    }
}
