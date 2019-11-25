import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {

  counts={};
  constructor(private _auth:AuthService) { }

  ngOnInit() {
    this.getcount();
  }

  getcount(){
    this._auth.getcountlist().subscribe(
      res=>{
        console.log(res);
        this.counts=res['users'];
        console.log(this.counts);
      },
      err=>{
        console.log(err);
      }
    )
  }
}
