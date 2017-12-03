import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { PassWordPageComponent } from '../setupmeeting/page/password-page/password-page.component';
import { RouterModule, Routes } from '@angular/router';
import { SetupMeetingPageComponent } from '../setupmeeting/page/setupmeeting-page/setupmeeting-page.component';
import { AppRoutingModule } from './app.routing';
import { MeetingSevice } from '../setupmeeting/service/meeting.service';
import { PaymentPageAreaComponent } from '../setupmeeting/pagearea/payment/payment-pagearea.component';
import { BASE_PATH } from '../setupmeeting/variables';
import { APIS } from '../setupmeeting/index';
@NgModule({
  declarations: [
    AppComponent,
    PassWordPageComponent,
    SetupMeetingPageComponent,
    PaymentPageAreaComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule,
    AppRoutingModule
  ],
  providers: [
    MeetingSevice,
    APIS,
    {provide:BASE_PATH,useValue:'http://ekanji.azurewebsites.net/'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
