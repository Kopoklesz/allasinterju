import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoCompanyRegister } from '../../commons/dtos/DtoCompany';
import { DtoUserRegister } from '../../commons/dtos/DtoUser';
import { DtoLogin } from '../../commons/dtos/DtoUser';
import { getCookie } from '../../utils/cookie.utils';
import { setCookie } from '../../utils/cookie.utils';


export interface AuthResponse {
  token: string;
}

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

  login(email: string, password: string): Observable<any> {
    const loginData: DtoLogin = {
      userName: email,
      password: password
    };
    
    return this.http.post<any>(`${this.apiUrl}/Login`, loginData, {
      withCredentials: true,
      observe: 'response'  // Teljes HTTP response kérése
    });
  }



  getToken(): string | null {
    return getCookie("JWT_TOKEN");
  }

  logout() {
    return this.http.post(`${this.apiUrl}/Logout`, {
      withCredentials: true,
    });
  }

  checkLoginStatus(): boolean {
    // Add token check logic here
    return this.isLoggedInSubject.value;

  
}
}
