import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, tap } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class SignInService {
  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  /*login(email: string, password: string): Observable<any> {
    // Dummy method to simulate a backend response
    if (email === 'a@b.com' && password === 'password123') {
      return of({ success: true, message: 'Login successful' }).pipe(
        tap(response => {
          if (response.success) {
            this.authService.login();
          }
        })
      );
    }
    return of({ success: false, message: 'Invalid email or password' });

  
    // Uncomment this when backend is ready
    const loginData = { email, password };
    return this.http.post<any>('your-backend-url/login', loginData).pipe(
      tap(response => {
        if (response.success) {
          this.authService.login();
        }
      })
    );
  
  }*/
}
