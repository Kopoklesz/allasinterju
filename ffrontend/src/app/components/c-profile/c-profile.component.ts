import { Component, ElementRef, ViewChild  } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobCardComponent } from '../../commons/components/job-card/job-card.component';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoCompany } from '../../commons/dtos/DtoCompany';
import { CompanyService } from '../../services/company/company.service';
import { HttpClient } from '@angular/common/http';
import { DtoInvitaion } from '../../commons/dtos/DtoInvitaion';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-c-profile',
  standalone: true,
  imports: [NavbarComponent, JobCardComponent, CommonModule, FormsModule],
  templateUrl: './c-profile.component.html',
  styleUrls: ['./c-profile.component.css']
})

export class CProfileComponent {

  @ViewChild('expinp') expinp: ElementRef | undefined;
  
  invitationCode: string | null = null;
  advertisedJobs?: DtoJobShort[] = [];
  company?: DtoCompany;
  isPopupVisible = false;
  inputValue: string = '';
  code : string = "";
  generated : boolean = false;
 

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private companyService: CompanyService,
    private http: HttpClient
  ) {}

  ngOnInit() {
    const jobIdParam = this.route.snapshot.paramMap.get('id');
    const jobId = jobIdParam ? Number(jobIdParam) : null;
    if (jobId !== null) {
      this.companyService.getAdvertisedJob(jobId).subscribe((data) => {
        this.advertisedJobs = data;
      });
      this.companyService.getUserData(jobId).subscribe((data) => {
        this.company = data;
      });
    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  newJob() {
    this.router.navigate(['/new-job']);
  }

  generateCode() {
   
    if (this.expinp){
    this.companyService.generateRandomCode(this.expinp.nativeElement.value).subscribe({
      next: (response) => {
        this.code = response;
        this.generated = true;
        
      },
      error: (error) => {
        console.error('Error generating code:', error);
      }
    });
    this.expinp.nativeElement.value = "";
  }
  }

  editProfile() {
    this.router.navigate(['/edit-c-profile', this.company?.id]);
  }

  // Show the input popup
  openPopup() {
    this.isPopupVisible = true;
  }

  // Close the input popup
  closePopup() {
    this.isPopupVisible = false;
  }
  generateNew(){
      this.code = "";
      this.generated = false;
  }
}
