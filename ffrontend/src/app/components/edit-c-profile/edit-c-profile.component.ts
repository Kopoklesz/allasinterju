import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { CompanyService } from '../../services/company/company.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DtoCompany } from '../../commons/dtos/DtoCompany';

@Component({
  selector: 'app-edit-c-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './edit-c-profile.component.html',
  styleUrls: ['./edit-c-profile.component.css']
})
export class EditCProfileComponent implements OnInit {
  profileForm!: FormGroup;
  credentialsForm!: FormGroup;
  isLoading = false;
  currentCompany?: DtoCompany;
  showCredentialsForm = false;

  companyTypes = ['BT', 'KFT', 'ZRT'];

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForms();
    this.loadCompanyData();
  }

  private initForms() {
    // Form for general profile data
    this.profileForm = this.fb.group({
      companyName: ['', Validators.required],
      companyType: ['', Validators.required],
      description: [''],
      mailingAddress: ['', Validators.required],
      contactPerson: ['', Validators.required],
      profilePicture: [''],
      mobilePhone: ['', [Validators.pattern('^[+]?[0-9]{8,}$')]],
      phone: ['', [Validators.pattern('^[+]?[0-9]{8,}$')]],
      contactPersonName: ['', Validators.required],
      location: ['', Validators.required]
    });

    // Separate form for credentials
    this.credentialsForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.minLength(6)]],
      confirmNewPassword: ['']
    });
  }

  private loadCompanyData() {
    const companyId = this.route.snapshot.paramMap.get('id');
    if (companyId) {
      this.companyService.getUserData(Number(companyId)).subscribe({
        next: (company) => {
          this.currentCompany = company;
          this.patchFormWithCompanyData(company);
        },
        error: (error) => {
          console.error('Error loading company data:', error);
        }
      });
    }
  }

  private patchFormWithCompanyData(company: DtoCompany) {
    // Minden mezőt betöltünk a formba, ami létezik a company objektumban
    const formValue: any = {};
    
    if (company.companyName) formValue.companyName = company.companyName;
    if (company.companyType) formValue.companyType = company.companyType;
    if (company.description) formValue.description = company.description;
    if (company.mailingAdress) formValue.mailingAddress = company.mailingAdress;
    if (company.outsideCoummunicationEmployeeM) formValue.contactPersonName = company.outsideCoummunicationEmployeeM;
    if (company.mobilePhoneNumber) formValue.mobilePhone = company.mobilePhoneNumber;
    if (company.cablePhoneNumber) formValue.phone = company.cablePhoneNumber;
    if (company.mainAdress) formValue.location = company.mainAdress;
  
    this.profileForm.patchValue(formValue);
    
    // Form állapot alaphelyzetbe állítása, hogy csak a tényleges változtatások legyenek "dirty"
    this.profileForm.markAsPristine();
    this.profileForm.markAsUntouched();
  }

  toggleCredentialsForm() {
    this.showCredentialsForm = !this.showCredentialsForm;
    if (!this.showCredentialsForm) {
      this.credentialsForm.reset();
    }
  }

  onUpdateProfile() {
    if (this.profileForm.valid && this.currentCompany) {
      this.isLoading = true;
      
      // Csak a módosított mezőket küldjük el
      const changes: Partial<DtoCompany> = {};
      Object.keys(this.profileForm.controls).forEach(key => {
        const control = this.profileForm.get(key);
        if (control && control.dirty) {
          changes[key as keyof DtoCompany] = control.value;
        }
      });
  
      // Ha van módosított mező, akkor küldjük el
      if (Object.keys(changes).length > 0) {
        this.companyService.updateCompany(this.currentCompany.id, changes).subscribe({
          next: () => {
            this.router.navigate(['/c-profile', this.currentCompany?.id]);
          },
          error: (error: any) => {
            console.error('Error updating company:', error);
            this.isLoading = false;
          }
        });
      } else {
        // Ha nincs módosítás, egyszerűen navigáljunk vissza
        this.router.navigate(['/c-profile', this.currentCompany?.id]);
      }
    }
  }

  onUpdateCredentials() {
    if (this.credentialsForm.valid && this.currentCompany) {
      this.isLoading = true;
      const credentials = this.credentialsForm.value;

      this.companyService.updateCredentials(this.currentCompany.id, credentials).subscribe({
        next: () => {
          this.router.navigate(['/c-profile', this.currentCompany?.id]);
        },
        error: (error) => {
          console.error('Error updating credentials:', error);
          this.isLoading = false;
        }
      });
    }
  }

  onCancel() {
    this.router.navigate(['/c-profile', this.currentCompany?.id]);
  }
}