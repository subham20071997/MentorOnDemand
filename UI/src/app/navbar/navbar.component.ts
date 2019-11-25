import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  dashboard:string;
  constructor(private _authservice:AuthService) { 
    this._authservice.getLoggedInStatus.subscribe(isLoggedIn=>{console.log(isLoggedIn); this.setDashboard();}) 
  }

  ngOnInit() {
    this.setDashboard();
  }

  setDashboard(){
    if(this._authservice.loggedIn()){
      console.log('Role' + this._authservice.getRole());
      switch(this._authservice.getRole()){
        case '2':this.dashboard='studentdashboard';break;
        case '3':this.dashboard='mentordashboard';break;
        case '1':this.dashboard='dashboard';break;
      }
    }
    console.log(this.dashboard);
  }

}
