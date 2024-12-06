import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DtoTest } from '../../../commons/dtos/DtoTest';
import { JobTestsService } from '../../../services/job-tests/job-tests.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-programming-test-take',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './programming-test-take.component.html',
  styleUrls: ['./programming-test-take.component.css']
})
export class ProgrammingTestTakeComponent implements OnInit {
  test?: DtoTest;
  timeRemaining = 0;
  isSubmitting = false;
  solutionForm: FormGroup;
  private timerInterval: any;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private testService: JobTestsService
  ) {
    this.solutionForm = this.fb.group({
      language: ['python', Validators.required],
      code: ['', Validators.required]
    });
  }

  ngOnInit() {
    const testId = this.route.snapshot.paramMap.get('id');
    if (testId) {
      this.loadTest(Number(testId));
    }
  }

  private loadTest(testId: number) {
    this.testService.getTest(testId).subscribe(
      test => {
        this.test = test;
        this.timeRemaining = test.duration * 60;
        this.startTimer();
        if (test.template) {
          this.solutionForm.patchValue({ code: test.template });
        }
      }
    );
  }

  private startTimer() {
    this.timerInterval = setInterval(() => {
      if (this.timeRemaining > 0) {
        this.timeRemaining--;
      } else {
        this.submitSolution(true);
      }
    }, 1000);
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
  }

  submitSolution(timeExpired: boolean = false) {
    if (!this.test || !this.solutionForm.valid || this.isSubmitting) return;

    this.isSubmitting = true;
    const solution = {
      ...this.solutionForm.value,
      timeExpired
    };

    this.testService.submitSolution(this.test.id, solution).subscribe({
      next: () => {
        this.router.navigate(['/job-tests']);
      },
      error: (error) => {
        console.error('Error submitting solution:', error);
        this.isSubmitting = false;
      }
    });
  }

  ngOnDestroy() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }
}