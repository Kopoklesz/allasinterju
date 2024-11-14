import { Component } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobCardComponent } from '../../commons/components/job-card/job-card.component';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoCompany } from '../../commons/dtos/DtoCompany';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-c-profile',
  standalone: true,
  imports: [NavbarComponent,JobCardComponent,CommonModule],
  templateUrl: './c-profile.component.html',
  styleUrl: './c-profile.component.css'
})

export class CProfileComponent {

 
  advertisedJobs ?: DtoJobShort [] = [];
  company ?: DtoCompany;
 
  
    constructor(
      private router: Router,
      private route: ActivatedRoute,
      private userService: UserService 
    ) {}

  ngOnInit() {
    const jobIdParam = this.route.snapshot.paramMap.get('id'); 
    const jobId = jobIdParam ? Number(jobIdParam) : null;       
    if (jobId !== null) {
    /*  this.userService.getAdvertisedJob(jobId).subscribe(data => {
      this.advertisedJobs = data;
      });
      this.userService.getUserData(jobId).subscribe(data => {
        this.company = data;
        });*/

    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  newJob(){
    this.router.navigate(['/new-job']); 
  }
 
 }
