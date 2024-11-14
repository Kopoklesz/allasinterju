import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationComponent } from '../registration/registration.component';
import { SignInService } from '../../services/sign-in/sign-in.service'; // Javított import útvonal

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
  emailErrorVisible: boolean = false; // Track email error visibility
  passwordErrorVisible: boolean = false; // Track password error visibility
  private popupVisible: boolean = false;

  constructor(private signInService: SignInService) {}

  showPopup(): void {
    this.popupVisible = true;
  }

  isVisible2(): boolean {
    return this.popupVisible;
  }

  hidePopup(): void {
    this.popupVisible = false;
  }

  onCreateAccount(): void {
    console.log('Navigate to create account page');
  }

  onLogin(): void {
    // Validate inputs before sending to the service
    this.emailErrorVisible = !this.validateEmail(this.email);
    this.passwordErrorVisible = this.password.length < 5;

    if (this.emailErrorVisible || this.passwordErrorVisible) {
      return; // Stop the login process if there are errors
    }

    this.signInService.login(this.email, this.password).subscribe({
      next: (response) => {
        if (response === null) {
          console.error('Email and password are required.');
          return;
        }
        console.log('Login successful', response);
      },
      error: (error) => {
        console.error('Login failed', error);
      },
    });
  }

  public validateEmail(email: string): boolean {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
  }

  public clearError(field: string): void {
    // Reset error visibility when input is focused
    if (field === 'email') {
      this.emailErrorVisible = false;
    } else if (field === 'password') {
      this.passwordErrorVisible = false;
    }
  }

  // Method to check error on blur
  checkErrorOnBlur(field: string): void {
    if (field === 'email') {
      this.emailErrorVisible = !this.validateEmail(this.email);
    } else if (field === 'password') {
      this.passwordErrorVisible = this.password.length < 5;
    }
  }
  
  showRegistration(): void {
    this.hidePopup(); // Elrejtjük a bejelentkezési ablakot
    this.registrationComponent.showPopup(); // Megjelenítjük a regisztrációs ablakot
  }
}
