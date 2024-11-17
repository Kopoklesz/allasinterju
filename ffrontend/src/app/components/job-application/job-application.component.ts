import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { DtoJob } from '../../commons/dtos/DtoJob';
import { DtoTest } from '../../commons/dtos/DtoTest';




@Component({
  selector: 'app-job-application',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './job-application.component.html',
  styleUrls: ['./job-application.component.css']
})

export class JobApplicationComponent {
  job: DtoJob | null = null;
  isApplied: boolean = false;
  isLoading: boolean = false;
  jobTests: DtoTest[] = [];
  totalDuration: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jobApplicationService: JobApplicationService 
  ) {}

  pageTitle = 'Job-Application';

  ngOnInit() {
  
    const jobIdParam = this.route.snapshot.paramMap.get('id'); 
    const jobId = jobIdParam ? Number(jobIdParam) : null;       
    if (jobId !== null) {
      this.jobApplicationService.getJob(jobId).subscribe(data => {
        this.job = data;
        this.checkApplicationStatus(jobId);
      });
    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  checkApplicationStatus(jobId: number) {
    this.jobApplicationService.checkApplicationStatus(jobId).subscribe({
      next: (status) => {
        this.isApplied = status;
        if (status) {
          this.loadJobTests(jobId);
        }
      },
      error: (error) => {
        console.error('Error checking application status:', error);
      }
    });
  }

  loadJobTests(jobId: number) {
    this.jobApplicationService.getJobTests(jobId).subscribe({
      next: (tests) => {
        this.jobTests = tests;
        this.totalDuration = tests.reduce((sum, test) => sum + test.duration, 0);
      },
      error: (error) => {
        console.error('Error loading job tests:', error);
      }
    });
  }

  applyForJob() {
    if (!this.job?.id) return;
    
    this.isLoading = true;
    this.jobApplicationService.applyForJob(this.job.id).subscribe({
      next: (response) => {
        this.isApplied = true;
        this.loadJobTests(this.job!.id);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error applying for job:', error);
        this.isLoading = false;
      }
    });
  }

  startTests() {
    if (!this.job?.id) return;
    this.router.navigate(['/job-tests', this.job.id]);
  }

  getCompletedTestsCount(): number {
    return this.jobTests.filter(test => test.isCompleted).length;
  }
}