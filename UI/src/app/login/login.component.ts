import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginData={};
  constructor(private _auth:AuthService,private _router:Router) { }

  ngOnInit() {
  }

  login(){
    this._auth.login(this.loginData)
      .subscribe(
        res=>{
          //console.log(res);
          console.log("Login Successfull !");
          localStorage.setItem('token',res['token']);
          localStorage.setItem('role',res['role']);
          localStorage.setItem('userEmail', res['email']);
          this._auth.getLoggedInStatus.emit(true);
          alert("Login Successfull !");
          this._router.navigate(['/home']);
        },
        err=> {
          console.log(err);
          alert("Invalid Credentials !");
        }
      )
  }

}
