import { NgModule }              from '@angular/core';
import { RouterModule, Routes }  from '@angular/router';
import { SetupMeetingPageComponent } from '../setupmeeting/page/setupmeeting-page/setupmeeting-page.component';
import { PassWordPageComponent } from '../setupmeeting/page/password-page/password-page.component';
 
const appRoutes: Routes = [
  { path: 'possword', component: PassWordPageComponent },
  { path: 'meetings/:meetingid',        component: SetupMeetingPageComponent },
  { path: '',   redirectTo: '/possword', pathMatch: 'full' }
];
 
@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}