import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-adminprofile',
  templateUrl: './adminprofile.component.html',
  styleUrls: ['./adminprofile.component.scss']
})
export class AdminprofileComponent implements OnInit {

  admindata={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getprofile();
  }

  getprofile(){
    console.log(this._auth.getUserEmail());
    this._auth.getadminprofile(this._auth.getUserEmail()).subscribe(
      res=>{
        this.admindata = res;
        console.log(this.admindata);
        //alert("Successfully Updated");
      },
      err=>{
        console.log(err);
        //alert("Update failed");
      }
    )
  }


  updateadminprofile(){
    this.admindata['gender'] = this.admindata['gender'] == "1"? 1: 2;
    console.log(this.admindata);
    this._auth.updateadminprofile(this.admindata).subscribe(
      res=>{
        console.log(res);
        alert("Successfully Updated");
      },
      err=>{
        console.log(err);
        alert("Update failed");
      }
    )
  }
}
