import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
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