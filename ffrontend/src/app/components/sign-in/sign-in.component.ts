import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationComponent } from '../registration/registration.component';
import { SignInService } from '../../services/sign-in/sign-in.service';
import { DtoLogin } from '../../commons/dtos/DtoUser';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';
import * as Cookies from 'js-cookie';
import { parseJwt } from '../../utils/cookie.utils';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [CommonModule, FormsModule, RegistrationComponent],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})

export class SignInComponent {
  @ViewChild('registration') registrationComponent!: RegistrationComponent;
  email: string = '';
  password: string = '';
  emailErrorVisible: boolean = false;
  passwordErrorVisible: boolean = false;
  private popupVisible: boolean = false;
  loading: boolean = false;
  errorMessage: string = '';
  showError: boolean = false;

  constructor(
    private signInService: SignInService,
    private authService: AuthService,
    private router: Router,
  ) {}

  showPopup(): void {
    this.popupVisible = true;
  }

  isVisible2(): boolean {
    return this.popupVisible;
  }

  hidePopup(): void {
    this.popupVisible = false;
    this.resetForm();
  }

  resetForm(): void {
    this.email = '';
    this.password = '';
    this.emailErrorVisible = false;
    this.passwordErrorVisible = false;
  }

  validateEmail(email: string): boolean {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }

  showRegistration(): void {
    this.hidePopup();
    setTimeout(() => {
      if (this.registrationComponent) {
        this.registrationComponent.showPopup();
      }
    }, 300);
  }

  login() {
    this.showError = false;
    this.errorMessage = '';
    this.emailErrorVisible = false;
    this.passwordErrorVisible = false;

    if (!this.email) {
      this.handleLoginError('Email address is required');
      return;
    }

    if (!this.validateEmail(this.email)) {
      this.handleLoginError('Please enter a valid email address');
      return;
    }

    if (!this.password) {
      this.handleLoginError('Password is required');
      return;
    }

    if (this.password.length < 5) {
      this.handleLoginError('Password must be at least 5 characters long');
      return;
    }

    this.loading = true;

    const timeout = setTimeout(() => {
      this.loading = false;
      this.showError = true;
      this.errorMessage = 'Request timeout. Please try again.';
    }, 10000);

    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        
        let token = this.authService.getToken();
        if (token) {
          localStorage.setItem('JWT_TOKEN',token);
          Cookies.default.set("JWT_TOKEN", token, { 
            expires: 7, 
            secure: true, 
            sameSite: 'Strict' 
          });
          this.hidePopup();
  
          const decodedToken = parseJwt(token);
          if (decodedToken?.role === 'Ceg') {
            this.router.navigate(['/c-profile', decodedToken.id]);
          } else if (decodedToken?.role === 'Munkakereso') {
            this.router.navigate(['/profile', decodedToken.id]);
          } else {
            this.router.navigate(['']);
          }
        } else {
          this.handleLoginError('Authentication failed');
        }
        this.loading = false;
      },
      error: (error) => {
        clearTimeout(timeout);
        console.error('Login error:', error);
        
        switch (error.status) {
          case 401:
            this.handleLoginError('Invalid email or password');
            break;
          case 404:
            this.handleLoginError('User not found');
            break;
          case 400:
            this.handleLoginError('Invalid request. Please check your input.');
            break;
          case 429:
            this.handleLoginError('Too many login attempts. Please try again later.');
            break;
          case 503:
            this.handleLoginError('Service is temporarily unavailable. Please try again later.');
            break;
          default:
            this.handleLoginError('An unexpected error occurred. Please try again.');
        }
        this.loading = false;
      }
    });
  }

  private handleLoginError(message: string): void {
    this.showError = true;
    this.errorMessage = message;
    this.loading = false;
    this.emailErrorVisible = false;
    this.passwordErrorVisible = false;
  }
}