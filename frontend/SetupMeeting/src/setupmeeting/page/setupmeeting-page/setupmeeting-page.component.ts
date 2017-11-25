import { Component } from '@angular/core';
import { MeetingSevice } from '../../service/meeting.service';
import { Meeting, MeetingStatus } from '../../model/meeting';
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
        })
    }

    
    //クリックイベント
    onClick(){
        if(this.meeting.Status == MeetingStatus.Open){
            this.meeting.Status = MeetingStatus.Before;
        }else{
            this.meeting.Status = MeetingStatus.Open;
        }
    }
}
