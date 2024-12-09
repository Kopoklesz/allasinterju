import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { DtoApplication } from '../../commons/dtos/DtoApplication';

@Component({
  selector: 'app-submitted-applications',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './submitted-applications.component.html',
  styleUrls: ['./submitted-applications.component.css']
})
export class SubmittedApplicationsComponent implements OnInit {
  applications: DtoApplication[] = [];
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jobApplicationService: JobApplicationService
  ) {}

  ngOnInit() {
    const jobId = this.route.snapshot.paramMap.get('id');
    if (jobId) {
      this.loadSubmittedApplications(Number(jobId));
    }
  }

  private loadSubmittedApplications(jobId: number) {
    this.jobApplicationService.getSubmittedApplications(jobId).subscribe({
      next: (data) => {
        this.applications = data.sort((a: { percentage: number; }, b: { percentage: number; }) => b.percentage - a.percentage);
      },
      error: (error) => {
        console.error('Error loading applications:', error);
      }
    });
  }

  getPercentageColor(percentage: number): string {
    if (percentage >= 80) return '#2ecc71'; // zöld
    if (percentage >= 60) return '#f1c40f'; // sárga
    return '#e74c3c'; // piros
  }

  navigateToUserProfile(userId: number, event: Event) {
    event.stopPropagation();
    this.router.navigate(['/profile', userId]);
  }

  navigateToUserResults(userId: number, event: Event) {
    event.stopPropagation();
    const jobId = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/user-results', userId], {
      queryParams: { jobId: jobId }
    });
  }
}