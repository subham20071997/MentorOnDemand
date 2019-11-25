import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerData={}
  constructor(private _auth: AuthService,private _router:Router) { }

  ngOnInit() {
  }

  register(){
    this.registerData['role'] = this.registerData['role'] == "1"? 1 : this.registerData['role'] == "2"? 2 : 3;
    this._auth.register(this.registerData)
      .subscribe(
        res=> {
          //console.log(res);
          console.log("Register successfull !");
          alert("Registered Successfull!");
          //localStorage.setItem('token',res.token);
          this._router.navigateByUrl('/login');
        },
        err=> {
          console.log(err);
          alert("Failed to registered !");
        }
      )
  }

}
