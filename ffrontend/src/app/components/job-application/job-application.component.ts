import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobApplicationService } from '../../services/job-application/job-application.service';

@Component({
  selector: 'app-job-application',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './job-application.component.html',
  styleUrls: ['./job-application.component.css']
})
export class JobApplicationComponent {

  job: any;

  constructor(
    private route: ActivatedRoute,
    private jobApplicationService: JobApplicationService 
  ) {}
  pageTitle = 'Job-Application';
  ngOnInit() {
    const jobIdParam = this.route.snapshot.paramMap.get('id'); 
    const jobId = jobIdParam ? Number(jobIdParam) : null;       
    if (jobId !== null) {
      this.jobApplicationService.getJob(jobId).subscribe(data => {
        this.job = data;
      });
    } else {
      console.error('Job ID is missing or invalid');
    }
  }
}