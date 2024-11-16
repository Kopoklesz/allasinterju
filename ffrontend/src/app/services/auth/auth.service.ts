import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoCompanyRegister } from '../../commons/dtos/DtoCompany';
import { DtoUserRegister } from '../../commons/dtos/DtoUser';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private apiUrl = 'http://localhost:5000/User'; // Update this URL to match your backend
  //company ?: DtoCompanyReg;
  constructor(private http: HttpClient) {}

  registerCompany(userData: DtoCompanyRegister){
    console.log("futott");
    console.log(userData);
     return this.http.post(`${this.apiUrl}/RegisterCompany`, userData);
  }
  
  registerUser(userData: DtoUserRegister){
    console.log("futott");
    console.log(userData);
     return this.http.post(`${this.apiUrl}/RegisterUser`, userData);
  }

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  login() {
    this.isLoggedInSubject.next(true);
    // Add token storage logic here

  }

  logout() {
    this.isLoggedInSubject.next(false);
    // Add token removal logic here
  }

  checkLoginStatus(): boolean {
    // Add token check logic here
    return this.isLoggedInSubject.value;

  
}
}
