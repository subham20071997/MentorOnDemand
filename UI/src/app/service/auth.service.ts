import { Injectable,EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { EmailValidator } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  getLoggedInStatus:EventEmitter<any>=new EventEmitter();

  private _register="https://localhost:44351/authservice/register";

  private _login="https://localhost:44351/authservice/login";

  private _addcourse="https://localhost:44351/adminservice/addtech";

  private _getcourse="https://localhost:44351/adminservice/getTechnologies";

  private _getstudentprofile="https://localhost:44351/authservice/";

  private _updatestudentprofile="https://localhost:44351/authservice/updateprofile/";

  private _getmentorprofile="https://localhost:44351/authservice/getmentor/";

  private _updatementorprofile="https://localhost:44351/authservice/updatementorprofile/";

  private _getadminprofile="https://localhost:44351/authservice/getadmin/";

  private _updateadminprofile="https://localhost:44351/authservice/updateadminprofile/";

  private _getmentors="https://localhost:44351/adminservice/getmentors";

  private _getstudents="https://localhost:44351/adminservice/getstudents";

  private _getcounts="https://localhost:44351/adminservice/getcount";

  private _updateuser="https://localhost:44351/adminservice/updateuser/";

  private _getCourses="https://localhost:44351/adminservice/getcourses";

  private _applycourse="https://localhost:44351/studentservice/addCourse";

  private _getskills="https://localhost:44351/mentorservice/getskills/";

  constructor(private http: HttpClient , private _router:Router) { }

  register(data){
    console.log(data);
    return this.http.post<any>(this._register,data);
  }

  login(data){
    return this.http.post<any>(this._login,data);
  }

  loggedIn(){
    return !!localStorage.getItem('token');
  }

  getToken(){
    return localStorage.getItem('token');
  }

  getRole(){
    return localStorage.getItem('role');
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.getLoggedInStatus.emit(false);
    this._router.navigate(['/home']);
  }

  addcourse(data){
    return this.http.post(this._addcourse, data);
  }
  getUserEmail() {
    return localStorage.getItem('userEmail');
  }
  setUserEmail(email) {
    localStorage.setItem('userEmail', email);
  }

  getcourse(){
    return this.http.get(this._getcourse);
  }

  getstudentprofile(email){
    return this.http.get(this._getstudentprofile + email);
  }

  updateprofile(data){
    console.log(data);
    return this.http.put(this._updatestudentprofile + this.getUserEmail(),data);
  }

  getmentorprofile(email){
    return this.http.get(this._getmentorprofile + email);
  }

  updatementorprofile(data){
    console.log(data);
    return this.http.put(this._updatementorprofile + this.getUserEmail(),data);
  }

  getadminprofile(email){
    return this.http.get(this._getadminprofile + email);
  }

  updateadminprofile(data){
    console.log(data);
    return this.http.put(this._updateadminprofile + this.getUserEmail(),data);
  }

  getmentorslist(){
    return this.http.get(this._getmentors);
  }

  getstudentslist(){
    return this.http.get(this._getstudents);
  }

  getcountlist(){
    return this.http.get(this._getcounts);
  }

  updateuserstatus(email, data){
    return this.http.put(this._updateuser + email,data);
  }

  getcourselist(){
    return this.http.get(this._getCourses);
  }

  applycourse(data){
    data = {email: this.getUserEmail(), skillId: data};
    console.log(data);
    return this.http.post(this._applycourse,data);
  }

  getskills(){
    return this.http.get(this._getskills+this.getUserEmail());
  }
}
