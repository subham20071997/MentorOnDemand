import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-mentorlist',
  templateUrl: './mentorlist.component.html',
  styleUrls: ['./mentorlist.component.scss']
})
export class MentorlistComponent implements OnInit {

  mentors={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getmentorlist();
  }

  getmentorlist(){
    console.log(this.mentors);
    this._auth.getmentorslist().subscribe(
      res=>{
        this.mentors=res['users'];
        console.log(res);
      },
      err=>{
        console.log(err);
      }
    )
  }

  blockmentor(email, data){
    console.log(email);
    this._auth.updateuserstatus(email, data).subscribe(
      res=>{
        this.getmentorlist();
      },
      err=>{
        console.log(err);
      }
    )
  }

}
