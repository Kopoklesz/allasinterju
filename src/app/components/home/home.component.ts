import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { JobApplicationService } from '../../services/job-application.service'; // Uncomment when backend is ready
import { NavbarComponent } from '../navbar/navbar.component'; // Import the Navbar component
import { JobCardComponent } from '../job-card/job-card.component'; // Import the JobCardComponent

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, NavbarComponent, JobCardComponent], // Add JobCardComponent here
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  jobs = [
    { id: 1, company_name: 'Company A', short_desc: 'This is a short description.', job_position: 'Developer' },
    { id: 2, company_name: 'Company B', short_desc: 'Another job description.', job_position: 'Designer' }
  ];

  pageTitle = 'Home'; // Set the page title

  // Uncomment the JobApplicationService when backend is ready
  constructor(private router: Router, private jobService: JobApplicationService) {}

  ngOnInit() {
    // Uncomment this section when backend is ready
    /*
    this.jobService.getJobs().subscribe(data => {
      this.jobs = data; // Update the jobs array with actual data
    });
    */
  }

  onCardClick(job: any) {
    this.router.navigate(['/job-application', job.id]);
  }
}
