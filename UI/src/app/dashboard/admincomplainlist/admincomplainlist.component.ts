import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-admincomplainlist',
  templateUrl: './admincomplainlist.component.html',
  styleUrls: ['./admincomplainlist.component.scss']
})
export class AdmincomplainlistComponent implements OnInit {

  courses={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getcourses();
  }

  getcourses(){
    this._auth.getcourselist().subscribe(
      res=>{
       this.courses=res;
        console.log(res);
      },
      err=>{
        console.log(err);
      }
    )
  }
}
