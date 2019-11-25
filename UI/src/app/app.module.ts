import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { FormsModule }   from '@angular/forms';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { MaterialModule } from './material.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

import { MatchValueDirective } from './directives/match-value.directive';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CoursesComponent } from './courses/courses.component';
import { HomeComponent } from './home/home.component';
import { ContactusComponent } from './contactus/contactus.component';
import { FooterComponent } from './footer/footer.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { StudentdashboardComponent } from './studentdashboard/studentdashboard.component';
import { MentordashboardComponent } from './mentordashboard/mentordashboard.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { AdminnotificationComponent } from './dashboard/adminnotification/adminnotification.component';
import { AdminprofileComponent } from './dashboard/adminprofile/adminprofile.component';
import { MentorlistComponent } from './dashboard/mentorlist/mentorlist.component';
import { StudentlistComponent } from './dashboard/studentlist/studentlist.component';
import { AdminpaymentComponent } from './dashboard/adminpayment/adminpayment.component';
import { AdminaddcourseComponent } from './dashboard/adminaddcourse/adminaddcourse.component';
import { AdmincomplainlistComponent } from './dashboard/admincomplainlist/admincomplainlist.component';
import { StudentprofileComponent } from './studentdashboard/studentprofile/studentprofile.component';
import { StudentcourseComponent } from './studentdashboard/studentcourse/studentcourse.component';
import { StudentpaymentComponent } from './studentdashboard/studentpayment/studentpayment.component';
import { StudentnotificationComponent } from './studentdashboard/studentnotification/studentnotification.component';
import { MentorprofileComponent } from './mentordashboard/mentorprofile/mentorprofile.component';
import { MentorcourseComponent } from './mentordashboard/mentorcourse/mentorcourse.component';
import { MentorpaymentComponent } from './mentordashboard/mentorpayment/mentorpayment.component';
import { MentornotificationComponent } from './mentordashboard/mentornotification/mentornotification.component';
import { MentoraddskillComponent } from './mentordashboard/mentoraddskill/mentoraddskill.component';
import { StatsComponent } from './dashboard/stats/stats.component';
import { MentorstatsComponent } from './mentordashboard/mentorstats/mentorstats.component';
import { StudentstatsComponent } from './studentdashboard/studentstats/studentstats.component';
import { TokenInterceptorService } from './service/token-interceptor.service';

@NgModule({
  declarations: [
    MatchValueDirective,
    AppComponent,
    NavbarComponent,
    CoursesComponent,
    HomeComponent,
    ContactusComponent,
    FooterComponent,
    AboutusComponent,
    LoginComponent,
    RegisterComponent,
    DashboardComponent,
    StudentdashboardComponent,
    MentordashboardComponent,
    PagenotfoundComponent,
    AdminnotificationComponent,
    AdminprofileComponent,
    MentorlistComponent,
    StudentlistComponent,
    AdminpaymentComponent,
    AdminaddcourseComponent,
    AdmincomplainlistComponent,
    StudentprofileComponent,
    StudentcourseComponent,
    StudentpaymentComponent,
    StudentnotificationComponent,
    MentorprofileComponent,
    MentorcourseComponent,
    MentorpaymentComponent,
    MentornotificationComponent,
    MentoraddskillComponent,
    StatsComponent,
    MentorstatsComponent,
    StudentstatsComponent
  ],
  imports: [
    BrowserModule,
    Ng2SearchPipeModule,
    FormsModule,
    MaterialModule,
    HttpClientModule,
    AppRoutingModule,
    MDBBootstrapModule.forRoot(),
    BrowserAnimationsModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
