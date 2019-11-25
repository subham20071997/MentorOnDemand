import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-studentprofile',
  templateUrl: './studentprofile.component.html',
  styleUrls: ['./studentprofile.component.scss']
})
export class StudentprofileComponent implements OnInit {
  studentdata:{};

  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getprofile();
  }

  getprofile(){
    console.log(this._auth.getUserEmail());
    this._auth.getstudentprofile(this._auth.getUserEmail()).subscribe(
      res=>{
        this.studentdata = res;
        console.log(this.studentdata);
      },
      err=>{
        console.log(err);
      }
    )
  }
  updatestudentprofile(){
    //console.log(this.studentdata);
    this.studentdata['gender']=this.studentdata['gender']=='1'? 1 : 2;
    this._auth.updateprofile(this.studentdata).subscribe(
      res=>{
        console.log(res);
        alert("Update Succesfull !");
      },
      err=>{
        console.log(err);
        alert("Failed to Update");
      }
    )
  }

}
