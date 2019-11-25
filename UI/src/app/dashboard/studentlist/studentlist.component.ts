import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-studentlist',
  templateUrl: './studentlist.component.html',
  styleUrls: ['./studentlist.component.scss']
})
export class StudentlistComponent implements OnInit {

  students={}
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getstudentslist();
  }

  getstudentslist(){
    console.log(this.students);
    this._auth.getstudentslist().subscribe(
      res=>{
        console.log(res);
        this.students=res['users'];
      },
      err=>{
        console.log(err);
      }
    )
  }

  blockstudent(email, data){
    console.log(email);
    this._auth.updateuserstatus(email, data).subscribe(
      res=>{
        this.getstudentslist();
      },
      err=>{
        console.log(err);
      }
    )
  }

}
