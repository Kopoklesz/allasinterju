import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignInService {
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    // Dummy method to simulate a backend response
    if (email === 'a@b.com' && password === 'password123') {
      return of({ success: true, message: 'Login successful' }); // Simulated successful response
    }
    return of({ success: false, message: 'Invalid email or password' }); // Simulated failure response

    /*
    // Uncomment this when backend is ready
    const loginData = { email, password };
    return this.http.post<any>('your-backend-url/login', loginData);
    */
  }
}