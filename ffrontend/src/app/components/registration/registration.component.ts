import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DtoCompanyRegister } from '../../commons/dtos/DtoCompany';
import { AuthService } from '../../services/auth/auth.service';
import { DtoUserRegister } from '../../commons/dtos/DtoUser';
import { SignInComponent } from '../sign-in/sign-in.component';

@Component({
    selector: 'app-registration',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})

export class RegistrationComponent {
    isVisible = false;
    registrationType: 'user' | 'company' = 'user';
    @ViewChild('signIn') signInComponent!: SignInComponent;

    constructor(
        private authService:  AuthService,
    ) {}

    firstNameErrorVisible = false;
    lastNameErrorVisible = false;
    userEmailErrorVisible = false;
    userPasswordErrorVisible = false;
    userConfirmPasswordErrorVisible = false;
    passwordMatchErrorVisible = false;
    birthDateErrorVisible = false;
    birthPlaceErrorVisible = false;
    postalCodeErrorVisible = false;
    cityErrorVisible = false;
    companyNameErrorVisible = false;
    companyPasswordErrorVisible = false;
    companyConfirmPasswordErrorVisible = false;
    companyTypeErrorVisible = false;
    companyEmailErrorVisible = false;
    companyPhoneErrorVisible = false;
    companyAddressErrorVisible = false;
    contactPersonErrorVisible = false;
    companyNameTouched = false;
    companyPasswordTouched = false;
    companyConfirmPasswordTouched = false;
    companyEmailTouched = false;
    companyTypeTouched = false;
    phoneTouched = false;
    addressTouched = false;
    contactPersonTouched = false;
    companyPasswordMatchErrorVisible = false;
    phoneErrorVisible = false;
    addressErrorVisible = false;
    isLoading = false;
    invitationCodeErrorVisible = false;

    userData = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
        birthDate: '',
        birthPlace: '',
        postalCode: '',
        city: '',
        mothersName: '',
        invitationCode: ''
    };
  
    companyData = {
        name: '',
        password: '',
        confirmPassword: '',
        email: '',
        type: '',
        phone: '',
        address: '',
        contactPerson: ''
    };

    resetForm(): void {

        this.userData = {
            firstName: '',
            lastName: '',
            email: '',
            password: '',
            confirmPassword: '',
            birthDate: '',
            birthPlace: '',
            postalCode: '',
            city: '',
            mothersName: '',
            invitationCode: ''
        };

        this.companyData = {
            name: '',
            password: '',
            confirmPassword: '',
            email: '',
            type: '',
            phone: '',
            address: '',
            contactPerson: ''
        };

        this.firstNameErrorVisible = false;
        this.lastNameErrorVisible = false;
        this.userEmailErrorVisible = false;
        this.userPasswordErrorVisible = false;
        this.userConfirmPasswordErrorVisible = false;
        this.passwordMatchErrorVisible = false;
        this.birthDateErrorVisible = false;
        this.birthPlaceErrorVisible = false;
        this.postalCodeErrorVisible = false;
        this.cityErrorVisible = false;
        this.companyNameErrorVisible = false;
        this.companyPasswordErrorVisible = false;
        this.companyConfirmPasswordErrorVisible = false;
        this.companyTypeErrorVisible = false;
        this.companyEmailErrorVisible = false;
        this.companyPhoneErrorVisible = false;
        this.companyAddressErrorVisible = false;
        this.contactPersonErrorVisible = false;
        this.companyNameTouched = false;
        this.companyPasswordTouched = false;
        this.companyConfirmPasswordTouched = false;
        this.companyEmailTouched = false;
        this.companyTypeTouched = false;
        this.phoneTouched = false;
        this.addressTouched = false;
        this.contactPersonTouched = false;
        this.companyPasswordMatchErrorVisible = false;
        this.phoneErrorVisible = false;
        this.addressErrorVisible = false;
        this.isLoading = false;
        this.invitationCodeErrorVisible = false;
    }

    showPopup(): void {
        this.isVisible = true;
    }

    hidePopup(): void {
        this.isVisible = false;
    }

    //email kritérium
    validateEmail(email: string): boolean {
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailPattern.test(email);
    }

    //kritériumok ellnőrzése
    clearError(field: string): void {
        switch(field) {
            case 'firstName':
                this.firstNameErrorVisible = false;
                break;
            case 'lastName':
                this.lastNameErrorVisible = false;
                break;
            case 'userEmail':
                this.userEmailErrorVisible = false;
                break;
            case 'userPassword':
                this.userPasswordErrorVisible = false;
                this.passwordMatchErrorVisible = false;
                break;
            case 'userConfirmPassword':
                this.userConfirmPasswordErrorVisible = false;
                this.passwordMatchErrorVisible = false;
                break;
            case 'birthDate':
                this.birthDateErrorVisible = false;
                break;
            case 'birthPlace':
                this.birthPlaceErrorVisible = false;
                break;
            case 'postalCode':
                this.postalCodeErrorVisible = false;
                break;
            case 'city':
                this.cityErrorVisible = false;
                break;
            case 'companyName':
                this.companyNameErrorVisible = false;
                break;
            case 'companyPassword':
                this.companyPasswordErrorVisible = false;
                this.passwordMatchErrorVisible = false;
                break;
            case 'companyConfirmPassword':
                this.companyConfirmPasswordErrorVisible = false;
                this.passwordMatchErrorVisible = false;
                break;
            case 'companyType':
                this.companyTypeErrorVisible = false;
                break;
            case 'companyEmail':
                this.companyEmailErrorVisible = false;
                break;
            case 'companyPhone':
                this.companyPhoneErrorVisible = false;
                break;
            case 'companyAddress':
                this.companyAddressErrorVisible = false;
                break;
            case 'contactPerson':
                this.contactPersonErrorVisible = false;
                break;
            case 'invitationCode':
                this.invitationCodeErrorVisible = false;
                break;
        }
    }

    //Egyéb kritériumok ellenőrzése
    checkErrorOnBlur(field: string): void {
        switch(field) {
            case 'firstName':
                this.firstNameErrorVisible = !this.userData.firstName;
                break;
            case 'lastName':
                this.lastNameErrorVisible = !this.userData.lastName;
                break;
            case 'userEmail':
                this.userEmailErrorVisible = !this.validateEmail(this.userData.email);
                break;
            case 'userPassword':
                this.userPasswordErrorVisible = this.userData.password.length < 5;
                if (this.userData.confirmPassword) {
                    this.passwordMatchErrorVisible = this.userData.password !== this.userData.confirmPassword;
                }
                break;
            case 'userConfirmPassword':
                this.userConfirmPasswordErrorVisible = !this.userData.confirmPassword;
                this.passwordMatchErrorVisible = this.userData.password !== this.userData.confirmPassword;
                break;
            case 'birthDate':
                this.birthDateErrorVisible = !this.userData.birthDate || !this.isValidDate(this.userData.birthDate);
                break;
            case 'birthPlace':
                this.birthPlaceErrorVisible = !this.userData.birthPlace;
                break;
            case 'postalCode':
                this.postalCodeErrorVisible = !/^\d{4,5}$/.test(this.userData.postalCode);
                break;
            case 'city':
                this.cityErrorVisible = !this.userData.city;
                break;
            case 'companyName':
                this.companyNameErrorVisible = !this.companyData.name;
                break;
            case 'companyPassword':
                this.companyPasswordErrorVisible = this.companyData.password.length < 5;
                if (this.companyData.confirmPassword) {
                    this.passwordMatchErrorVisible = this.companyData.password !== this.companyData.confirmPassword;
                }
                break;
            case 'companyConfirmPassword':
                this.companyConfirmPasswordErrorVisible = !this.companyData.confirmPassword;
                this.passwordMatchErrorVisible = this.companyData.password !== this.companyData.confirmPassword;
                break;
            case 'companyType':
                this.companyTypeErrorVisible = !this.companyData.type;
                break;
            case 'companyEmail':
                this.companyEmailErrorVisible = !this.validateEmail(this.companyData.email);
                break;
            case 'companyPhone':
                this.companyPhoneErrorVisible = !/^\+?\d{7,15}$/.test(this.companyData.phone);
                break;
            case 'companyAddress':
                this.companyAddressErrorVisible = !this.companyData.address;
                break;
            case 'contactPerson':
                this.contactPersonErrorVisible = !this.companyData.contactPerson;
                break;
            case 'invitationCode':
                this.invitationCodeErrorVisible = false;
                break;
        }
    }

    onBlur(field: string): void {
        switch(field) {
            case 'companyName':
                this.companyNameTouched = true;
                this.companyNameErrorVisible = !this.companyData.name;
                break;
            case 'companyPassword':
                this.companyPasswordTouched = true;
                this.companyPasswordErrorVisible = this.companyData.password.length < 5;
                break;
            case 'companyConfirmPassword':
                this.companyConfirmPasswordTouched = true;
                this.companyConfirmPasswordErrorVisible = this.companyData.password.length < 5;
                break;
            case 'companyEmail':
                this.companyEmailTouched = true;
                this.companyEmailErrorVisible = !this.validateEmail(this.companyData.email);
                break;
            case 'companyType':
                this.companyTypeTouched = true;
                this.companyTypeErrorVisible = !this.companyData.type;
                break;
            case 'phone':
                this.phoneTouched = true;
                this.companyPhoneErrorVisible = !/^\+?\d{7,15}$/.test(this.companyData.phone);
                break;
            case 'address':
                this.addressTouched = true;
                this.companyAddressErrorVisible = !this.companyData.address;
                break;
            case 'contactPerson':
                this.contactPersonTouched = true;
                this.contactPersonErrorVisible = !this.companyData.contactPerson;
                break;
        }
    }

    isFormValid(): boolean {
        if (this.registrationType === 'user') {
            return !!(this.userData.firstName && 
                    this.userData.lastName && 
                    this.validateEmail(this.userData.email) && 
                    this.userData.password.length >= 5 &&
                    this.userData.password === this.userData.confirmPassword &&
                    this.userData.birthDate &&
                    this.userData.birthPlace &&
                    this.userData.postalCode &&
                    this.userData.city);
        } else {
            return !!(this.companyData.name && 
                    this.companyData.type && 
                    this.validateEmail(this.companyData.email) &&
                    this.companyData.phone &&
                    this.companyData.address &&
                    this.companyData.contactPerson);
        }
    }

    isValidDate(date: string): boolean {
        if (!date) return false;

        const parsedDate = new Date(date);

        return !isNaN(parsedDate.getTime());
    }

    
    register(): void {
        if (!this.isFormValid()) {
            return;
        }

        if (this.isLoading || !this.isFormValid()) {
            return;
        }

        this.isLoading = true;

        if (this.registrationType === 'user') {
            const userData: DtoUserRegister = {
                firstName: this.userData.firstName,
                lastName: this.userData.lastName,
                emailAddress: this.userData.email,
                password: this.userData.password,
                taxNumber: 0,  
                mothersName: this.userData.mothersName || '',
                birthDate: new Date(this.userData.birthDate),
                birthPlace: this.userData.birthPlace,
                invitationCode: this.userData.invitationCode || '' 
            };


            this.authService.registerUser(userData).subscribe({
                next: (response) => {
                    this.resetForm();
                    this.hidePopup();
                },
                error: (err) => {
                    console.error('Registration error:', err);
                }
            });
        } else {
            const companyData: DtoCompanyRegister = {
                email: this.companyData.email,
                password: this.companyData.password,
                companyName: this.companyData.name,
                companyType: this.companyData.type,
                description: "",
                place: {
                    zipCode: this.companyData.address.split(',')[0],
                    city: this.companyData.address.split(',')[1],
                    streetNumber: this.companyData.address.split(' ').slice(2).join(' ') || ''
                },
                mailingAddress: this.companyData.address,
                hrEmployee: "", //this.companyData.contactPerson,
                mobilePhoneNumber: this.companyData.phone,
                cablePhoneNumber: '',
                pictureBase64: ''
            };

            this.authService.registerCompany(companyData).subscribe({
                next: (response) => {
                    this.resetForm();
                    this.hidePopup();
                    this.isLoading = false;
                },
                error: (err) => {
                    console.error('Registration error:', err);
                    this.isLoading = false;
                }
            });
        }
    }

    validateCompanyPassword(): void {
        this.companyPasswordErrorVisible = this.companyData.password.length < 5;
        this.companyPasswordTouched = true;
      }
    
      checkPasswordMatch(field: 'userData' | 'companyData'): void {
        if (field === 'companyData') {
          this.companyPasswordMatchErrorVisible = 
            this.companyData.password !== this.companyData.confirmPassword;
          this.companyConfirmPasswordTouched = true;
        }
    }

    backToLogin(): void {
        this.hidePopup();
        setTimeout(() => {
        if (this.signInComponent) {
            this.signInComponent.showPopup();
        }
        }, 300);
    }
}