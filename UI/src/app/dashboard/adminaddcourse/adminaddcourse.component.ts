import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminaddcourse',
  templateUrl: './adminaddcourse.component.html',
  styleUrls: ['./adminaddcourse.component.scss']
})
export class AdminaddcourseComponent implements OnInit {

  coursedata={};
  constructor(private _auth:AuthService,private _router:Router) { }

  ngOnInit() {
  }

  submitcourse(){
    this.coursedata['UserEmail'] = this._auth.getUserEmail();
    console.log(this.coursedata);
    this._auth.addcourse(this.coursedata).subscribe(
      res => {
        console.log(res);
        //this._router.navigateByUrl('adminaddcourse');
        alert("Course Successfully Added");
      },
      err => {
        console.log(err);
        alert("Failed to Add course");
      }
    )
  }
}
