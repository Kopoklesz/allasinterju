import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { UserService } from '../../services/user/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DtoUser, DtoUserModify } from '../../commons/dtos/DtoUser';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  profileForm!: FormGroup;
  credentialsForm!: FormGroup;
  isLoading = false;
  currentUser?: DtoUser;
  showCredentialsForm = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForms();
    this.loadUserData();
  }

  private initForms() {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      birthPlace: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]]
    });

    this.credentialsForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.minLength(6)]],
      confirmNewPassword: [''],
      email: ['', [Validators.email]]
    });
  }

  private loadUserData() {
    const userId = this.route.snapshot.paramMap.get('id');
    if (userId) {
      this.userService.getUserData(Number(userId)).subscribe({
        next: (user) => {
          this.currentUser = user;
          this.patchFormWithUserData(user);
        },
        error: (error) => {
          console.error('Error loading user data:', error);
        }
      });
    }
  }

  private patchFormWithUserData(user: DtoUser) {
    const formValue: any = {};
    
    if (user.firstName) formValue.firstName = user.firstName;
    if (user.lastName) formValue.lastName = user.lastName;
    if (user.emailAddress) formValue.emailAddress = user.emailAddress;
    if (user.birthDate) formValue.birthDate = user.birthDate;
    if (user.birthPlace) formValue.birthPlace = user.birthPlace;
    
    this.profileForm.patchValue(formValue);
    this.profileForm.markAsPristine();
    this.profileForm.markAsUntouched();
  }

  onUpdateProfile() {
    if (this.profileForm.valid && this.currentUser) {
      this.isLoading = true;

      const changes: DtoUserModify = {
        firstName: this.profileForm.value.firstName,
        lastName: this.profileForm.value.lastName,
        birthDate: this.profileForm.value.birthDate,
        birthPlace: this.profileForm.value.birthPlace,
        password: '',
        taxNumber: 0,
        mothersName: '',
        leetcodeUsername: '',
        competences: [{ type: '', level: '' }],
        vegzettsegek: [{ rovidleiras: '', hosszuleiras: '' }]
      };

      this.userService.updateUser(changes).subscribe({
        next: (updatedUser) => {
          this.isLoading = false;
          this.router.navigate(['/profile', this.currentUser?.id]);
        },
        error: (error) => {
          console.error('Error updating user:', error);
          this.isLoading = false;
        }
      });
    }
  }

  onUpdateCredentials() {
    if (this.credentialsForm.valid && this.currentUser) {
      this.isLoading = true;
      const credentials = this.credentialsForm.value;

      this.userService.updateCredentials(this.currentUser.id, credentials)
        .subscribe({
          next: () => {
            this.router.navigate(['/profile', this.currentUser?.id]);
          },
          error: (error) => {
            console.error('Error updating credentials:', error);
            this.isLoading = false;
          }
        });
    }
  }

  onCancel() {
    this.router.navigate(['/profile', this.currentUser?.id]);
  }
}