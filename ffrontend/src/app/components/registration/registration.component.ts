import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DtoCompanyRegister } from '../../commons/dtos/DtoCompany';
import { AuthService } from '../../services/auth/auth.service';
import { DtoUserRegister } from '../../commons/dtos/DtoUser';
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

    constructor(
        private authService:  AuthService,
      ) {}

    // Error flags
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

    userData = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
        birthDate: '',
        birthPlace: '',
        postalCode: '',
        city: ''
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
            console.log("nem valid")
            return;
        }

        if (this.registrationType === 'user') {
            console.log('User registration:', this.userData);
            let user : DtoUserRegister = {
                firstName: this.userData.firstName,
                lastName: this.userData.lastName,
                emailAddress: this.userData.email,
                password: this.userData.password,
                taxNumber: 1,
                mothersName: '',
                birthDate: new Date(),
                birthPlace: '',
                invitationCode: '',
            };
            console.log(user)
            this.authService.registerUser(user).subscribe({
                next:(response) =>{
                        console.log(response);
                },
                error: (err) => {
                 console.log(err.error.message);
                }

            });
        } else {
            
           let company : DtoCompanyRegister = {
                email : this.companyData.email,
                password : this.companyData.password,
                companyName: this.companyData.name,
                companyType: this.companyData.type,
                description: '',
                place: {zipCode : '1002', city: 'asd3', streetNumber:'21'},
                mailingAddress:  '',
                hrEmployee:  '',
                mobilePhoneNumber:  '',
                cablePhoneNumber:  '',
                pictureBase64:  '',
            };
            console.log(company)
            this.authService.registerCompany(company).subscribe({
                next:(response) =>{
                        console.log(response);
                },
                error: (err) => {
                 console.log(err.error.message);
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
}