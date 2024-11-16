import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoCompanyRegister } from '../../commons/dtos/DtoCompany';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/User'; // Update this URL to match your backend
  //company ?: DtoCompanyReg;
  constructor(private http: HttpClient) {}

  register(userData: DtoCompanyRegister){
    console.log("futott");
    console.log(userData);
     return this.http.post(`${this.apiUrl}/RegisterCompany`, userData);
     
  }
}