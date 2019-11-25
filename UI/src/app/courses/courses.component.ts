import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit {

  courses={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getcourselist();
  }

  getcourselist(){
    console.log(this.courses);
    this._auth.getcourse().subscribe(
      res=>{
        // console.log(res);
        this.courses=res['courses'];
        console.log(this.courses);
      },
      err=>{
        console.log(err);
      }
    )
  }

  applycourse(techId){
    console.log(this.courses);

    this._auth.applycourse(techId).subscribe(
      res=>{
        console.log('res');
        console.log(res);
        alert("Successfully Applied for the course !");
      },
      err=>{
        console.log('err');
        console.log(err);
        alert("Failed to registered for a course and Login as a Student !");
      }
    )
  }
}
