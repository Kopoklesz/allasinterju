import { Component } from '@angular/core';
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

@Component({
  selector: 'app-c-profile',
  standalone: true,
  imports: [NavbarComponent,JobCardComponent,CommonModule],
  templateUrl: './c-profile.component.html',
  styleUrl: './c-profile.component.css'
})

export class CProfileComponent {
  invitationCode: string | null = null;
 
  advertisedJobs ?: DtoJobShort [] = [];
  company ?: DtoCompany;
 
    constructor(
      private router: Router,
      private route: ActivatedRoute,
      private companyService: CompanyService,
      private http: HttpClient, 
    ) {}

  ngOnInit() {
    const jobIdParam = this.route.snapshot.paramMap.get('id'); 
    const jobId = jobIdParam ? Number(jobIdParam) : null;       
    if (jobId !== null) {
      this.companyService.getAdvertisedJob(jobId).subscribe(data => {
      this.advertisedJobs = data;
      });
      this.companyService.getUserData(jobId).subscribe(data => {
        this.company = data;
        });

    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  newJob(){
    this.router.navigate(['/new-job']); 
  }

  generateCode() {
    let dto : DtoInvitaion = {
      code : "kod",
      expiration : new Date(),
    }
    this.companyService.generateCode(dto).subscribe({
      next: (response) => {
      
      
      },
      error: (error) => {
        console.error('Error generating code:', error);
      }
    });
  }
}
