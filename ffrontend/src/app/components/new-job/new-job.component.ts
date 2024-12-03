import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';

@Component({
  selector: 'app-new-job',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './new-job.component.html',
  styleUrls: ['./new-job.component.css']
})
export class NewJobComponent implements OnInit {
  jobForm!: FormGroup;
  isSubmitting = false;
  showConfirmDialog = false;
  createdJobId: number | null = null;

  workScheduleOptions = [
    { value: 'full', label: 'Full Time' },
    { value: 'part', label: 'Part Time' },
    { value: 'remote', label: 'Remote Work' },
    { value: 'hybrid', label: 'Hybrid' }
  ];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private jobService: JobApplicationService
  ) {}

  ngOnInit() {
    this.initForm();
  }

  private initForm() {
    this.jobForm = this.fb.group({
      cim: ['', [Validators.required, Validators.minLength(3)]],
      leiras: ['', [Validators.required, Validators.minLength(10)]],
      munkarend: ['', Validators.required],
      rovidleiras: ['', [Validators.required]],
      telepely: ['', Validators.required],
      kitoltesihatarido: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.jobForm.valid) {
      this.isSubmitting = true;
      const newJob = {
        jobTitle: this.jobForm.get('cim')?.value,
        jobType: 'string',
        workOrder: this.jobForm.get('munkarend')?.value,
        description: this.jobForm.get('leiras')?.value,
        shortDescription: this.jobForm.get('rovidleiras')?.value,
        location: this.jobForm.get('telepely')?.value,
        deadline: this.jobForm.get('kitoltesihatarido')?.value,
      };

      this.jobService.addJob(newJob).subscribe({
        next: (response: {id: number}) => {
          this.createdJobId = response.id;
          this.isSubmitting = false;
          this.showConfirmDialog = true;
        },
        error: (error) => {
          console.error('Error creating job:', error);
          this.isSubmitting = false;
        }
      });
    }
  }

  addRounds() {
    if (this.createdJobId) {
      this.router.navigate(['/add-rounds', this.createdJobId]);
    }
  }

  skipRounds() {
    this.router.navigate(['/c-profile']);
  }

  getFormattedDate(): string {
    const today = new Date();
    return today.toISOString().split('T')[0];
  }
}