import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { ContactusComponent } from './contactus/contactus.component';
import { CoursesComponent } from './courses/courses.component';
import { FooterComponent } from './footer/footer.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { LoginComponent } from './login/login.component';
import { registerLocaleData } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { StudentdashboardComponent } from './studentdashboard/studentdashboard.component';
import { MentordashboardComponent } from './mentordashboard/mentordashboard.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { AdminaddcourseComponent } from './dashboard/adminaddcourse/adminaddcourse.component';
import { AdmincomplainlistComponent } from './dashboard/admincomplainlist/admincomplainlist.component';
import { AdminnotificationComponent } from './dashboard/adminnotification/adminnotification.component';
import { AdminpaymentComponent } from './dashboard/adminpayment/adminpayment.component';
import { AdminprofileComponent } from './dashboard/adminprofile/adminprofile.component';
import { StudentlistComponent } from './dashboard/studentlist/studentlist.component';
import { MentorlistComponent } from './dashboard/mentorlist/mentorlist.component';
import { StudentcourseComponent } from './studentdashboard/studentcourse/studentcourse.component';
import { StudentnotificationComponent } from './studentdashboard/studentnotification/studentnotification.component';
import { StudentpaymentComponent } from './studentdashboard/studentpayment/studentpayment.component';
import { StudentprofileComponent } from './studentdashboard/studentprofile/studentprofile.component';
import { MentoraddskillComponent } from './mentordashboard/mentoraddskill/mentoraddskill.component';
import { MentorcourseComponent } from './mentordashboard/mentorcourse/mentorcourse.component';
import { MentornotificationComponent } from './mentordashboard/mentornotification/mentornotification.component';
import { MentorpaymentComponent } from './mentordashboard/mentorpayment/mentorpayment.component';
import { MentorprofileComponent } from './mentordashboard/mentorprofile/mentorprofile.component';
import { StatsComponent } from './dashboard/stats/stats.component';
import { MentorstatsComponent } from './mentordashboard/mentorstats/mentorstats.component';
import { StudentstatsComponent } from './studentdashboard/studentstats/studentstats.component';


const routes: Routes = [
                        {path:"",redirectTo:"home",pathMatch:"full"},
                        {path:"navbar",component:NavbarComponent},
                        {path:"home",component:HomeComponent},
                        {path:"contactus",component:ContactusComponent},
                        {path:"courses",component:CoursesComponent},
                        {path:"footer",component:FooterComponent},
                        {path:"aboutus",component:AboutusComponent},
                        {path:"login",component:LoginComponent},
                        {path:"register",component:RegisterComponent},
                        {path:"dashboard",component:DashboardComponent,children:[
                          {path:"adminaddcourse",component:AdminaddcourseComponent},
                          {path:"admincomplainlist",component:AdmincomplainlistComponent},
                          {path:"adminnotification",component:AdminnotificationComponent},
                          {path:"adminpayment",component:AdminpaymentComponent},
                          {path:"adminprofile",component:AdminprofileComponent},
                          {path:"studentlist",component:StudentlistComponent},
                          {path:"mentorlist",component:MentorlistComponent},
                          {path:"stats",component:StatsComponent}
                        ]},
                        {path:"studentdashboard",component:StudentdashboardComponent,children:[
                          {path:"studentstats",component:StudentstatsComponent},
                          {path:"studentcourse",component:StudentcourseComponent},
                          {path:"studentnotification",component:StudentnotificationComponent},
                          {path:"studentpayment",component:StudentpaymentComponent},
                          {path:"studentprofile",component:StudentprofileComponent}
                        ]},
                        {path:"mentordashboard",component:MentordashboardComponent,children:[
                          {path:"mentorstats",component:MentorstatsComponent},
                          {path:"mentoraddskill",component:MentoraddskillComponent},
                          {path:"mentorcourse",component:MentorcourseComponent},
                          {path:"mentornotification",component:MentornotificationComponent},
                          {path:"mentorpayment",component:MentorpaymentComponent},
                          {path:"mentorprofile",component:MentorprofileComponent}
                        ]},
                        {path:"**",component:PagenotfoundComponent}
                      ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
