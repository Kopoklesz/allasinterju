import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { JobApplicationService } from '../../services/job-application/job-application.service'; // Uncomment when backend is ready
import { NavbarComponent } from '../../commons/components/navbar/navbar.component'; // Import the Navbar component
import { JobCardComponent } from '../../commons/components/job-card/job-card.component'; // Import the JobCardComponent
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, NavbarComponent, JobCardComponent], 
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  jobs: DtoJobShort[] = [];

  jobsteszt: DtoJobShort[] = [
    { Id: 1, JobTitle: 'Software Developer', JobType: 'Full-Time', CompanyName: 'Tech Co', City: 'New York' },
    { Id: 2, JobTitle: 'Graphic Designer', JobType: 'Part-Time', CompanyName: 'Creative Inc', City: 'San Francisco' },
    { Id: 3, JobTitle: 'Project Manager', JobType: 'Contract', CompanyName: 'Business Solutions', City: 'Chicago' },
    { Id: 4, JobTitle: 'Data Analyst', JobType: 'Full-Time', CompanyName: 'Analytics Corp', City: 'Los Angeles' },
    { Id: 5, JobTitle: 'Web Developer', JobType: 'Freelance', CompanyName: 'Web Works', City: 'Austin' }
  ];
 
  pageTitle = 'Home';

  
  constructor(private router: Router, private jobService: JobApplicationService) {}

  ngOnInit() {
   
    this.jobService.getJobs().subscribe(data => {
      this.jobs = data; 
      console.log(this.jobs);
    });
  
  }

  
}
