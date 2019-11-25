import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-mentorprofile',
  templateUrl: './mentorprofile.component.html',
  styleUrls: ['./mentorprofile.component.scss']
})
export class MentorprofileComponent implements OnInit {

  mentordata={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getprofile();
  }

  getprofile(){
    console.log(this._auth.getUserEmail());
    this._auth.getmentorprofile(this._auth.getUserEmail()).subscribe(
      res=>{
        this.mentordata = res;
        console.log(this.mentordata);
      },
      err=>{
        console.log(err);
      }
    )
  }


  updatementorprofile(){
    this.mentordata['gender'] = this.mentordata['gender'] == "1"? 1: 2;
    //console.log(this.mentordata);
    this._auth.updatementorprofile(this.mentordata).subscribe(
      res=>{
        console.log(res);
        alert("Update Successfull !");
      },
      err=>{
        console.log(err);
        alert("Failed to Update !");
      }
    )
  }

}
