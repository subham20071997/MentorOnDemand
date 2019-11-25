import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-mentornotification',
  templateUrl: './mentornotification.component.html',
  styleUrls: ['./mentornotification.component.scss']
})
export class MentornotificationComponent implements OnInit {

  coursedata={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getskill();
  }

  getskill(){
    console.log(this.coursedata);
    this._auth.getskills().subscribe(
      res=>{
        this.coursedata=res['skills'];
        console.log(res);
      },
      err=>{
        console.log(err);
      }
    )
  }

}
