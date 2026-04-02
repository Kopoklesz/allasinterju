import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { DtoJob, DtoJobAdd } from '../../commons/dtos/DtoJob';

@Component({
  selector: 'app-edit-job',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './edit-job.component.html',
  styleUrls: ['./edit-job.component.css']
})
export class EditJobComponent implements OnInit {
  jobForm!: FormGroup;
  isLoading = false;
  currentJob?: DtoJob;

  workScheduleOptions = [
    { value: 'full', label: 'Full Time' },
    { value: 'part', label: 'Part Time' },
    { value: 'remote', label: 'Remote Work' },
    { value: 'hybrid', label: 'Hybrid' }
  ];

  constructor(
    private fb: FormBuilder,
    private jobService: JobApplicationService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForm();
    this.loadJobData();
  }

  private initForm() {
    this.jobForm = this.fb.group({
      jobTitle: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      workOrder: ['', Validators.required],
      shortDescription: ['', Validators.required],
      location: ['', Validators.required],
      deadline: ['', Validators.required]
    });
  }

  private loadJobData() {
    const jobId = this.route.snapshot.paramMap.get('id');
    if (jobId) {
      this.jobService.getJob(Number(jobId)).subscribe({
        next: (job) => {
          this.currentJob = job;
          this.patchFormWithJobData(job);
        },
        error: (error) => {
          console.error('Error loading job data:', error);
        }
      });
    }
  }

  private patchFormWithJobData(job: DtoJob) {
    this.jobForm.patchValue({
      jobTitle: job.jobTitle,
      description: job.description,
      workOrder: job.jobType,
      shortDescription: job.shortDescription,
      location: job.city,
      deadline: job.deadline
    });
  }

  onSubmit() {
    if (this.jobForm.valid && this.currentJob) {
      this.isLoading = true;
      
      const updatedJob: DtoJobAdd = {
        jobTitle: this.jobForm.get('jobTitle')?.value,
        jobType: this.jobForm.get('shortDescription')?.value,
        description: this.jobForm.get('description')?.value,
        shortDescription: this.jobForm.get('shortDescription')?.value,
        location: this.jobForm.get('location')?.value,
        deadline: this.jobForm.get('deadline')?.value,
        workOrder: this.jobForm.get('workOrder')?.value,
        competences: [] 
      };

      this.jobService.updateJob(this.currentJob.id, updatedJob).subscribe({
        next: () => {
          this.router.navigate(['/job-application', this.currentJob?.id]);
        },
        error: (error) => {
          console.error('Error updating job:', error);
          this.isLoading = false;
        }
      });
      this.router.navigate(['/job-application', this.currentJob.id]);
    }
  }

  onCancel() {
    if (this.currentJob) {
      this.router.navigate(['/job-application', this.currentJob.id]);
    }
  }
}