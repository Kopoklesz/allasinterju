import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationComponent } from '../registration/registration.component';
import { SignInService } from '../../services/sign-in/sign-in.service';
import { DtoLogin } from '../../commons/dtos/DtoUser';
import { AuthService } from '../../services/auth/auth.service';
import { response } from 'express';
import { error } from 'console';
import { ActivatedRoute, Router } from '@angular/router';

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

  /*onLogin(): void {
    this.emailErrorVisible = !this.validateEmail(this.email);
    this.passwordErrorVisible = this.password.length < 5;

    if (this.emailErrorVisible || this.passwordErrorVisible) return;

    this.loading = true;
    this.signInService.login(this.email, this.password).subscribe({
      next: (response) => {
        if (response.success) {
          // Sikeres login kezelés
          this.hidePopup();
        } else {
          // Sikertelen login kezelés
        }
      },
      error: (error) => {
        console.error('Login failed', error);
      },
      complete: () => {
        this.loading = false;
      }
    });
  }*/

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
    let token : string | null;
    token = null;
    this.authService.login(this.email, this.password).subscribe({
      
      next: ( response) => {
       // localStorage.setItem('authToken', response.token);
       //homenavigate helyett vissza dob a bejelentkezésre
       // this.router.navigate(['']);
        
       token = this.authService.getToken();
       if(token){
          localStorage.setItem('JWT_TOKEN', token);
       }
       console.log('Token:', token);
      
      },
      error: () => {
        console.log('error');
      },
    });
   
  }
}