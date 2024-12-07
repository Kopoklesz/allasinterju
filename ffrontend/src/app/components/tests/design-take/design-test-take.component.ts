import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { JobTestsService } from '../../../services/job-tests/job-tests.service';
import { BDesignAdd } from '../../../commons/dtos/DtoDesignAdd';
import { DesignSolutionSubmission } from '../../../commons/dtos/DtoSubmissions';

@Component({
  selector: 'app-design-test-take',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './design-test-take.component.html',
  styleUrls: ['./design-test-take.component.css']
})
export class DesignTestTakeComponent implements OnInit {
  test: BDesignAdd | null = null;
  solution: string = '';
  isSubmitting = false;
  remainingTime: number = 0;
  timerInterval: any;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private testService: JobTestsService
  ) {}

  ngOnInit() {
    const testId = this.route.snapshot.paramMap.get('id');
    if (testId) {
      this.loadTest(Number(testId));
    }
  }

  ngOnDestroy() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  private loadTest(testId: number) {
    this.testService.getTest(testId).subscribe({
      next: (test: BDesignAdd) => {
        this.test = test;
        this.remainingTime = test.timeLimit * 60; // Convert to seconds
        this.startTimer();
      },
      error: (error) => {
        console.error('Error loading test:', error);
      }
    });
  }

  private startTimer() {
    this.timerInterval = setInterval(() => {
      this.remainingTime--;
      if (this.remainingTime <= 0) {
        clearInterval(this.timerInterval);
        this.autoSubmit();
      }
    }, 1000);
  }

  private autoSubmit() {
    if (!this.isSubmitting) {
      this.submitSolution();
    }
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
  }

  submitSolution() {
    if (!this.test || !this.solution.trim() || this.isSubmitting) return;

    this.isSubmitting = true;
    
    const submission: DesignSolutionSubmission = {
      testId: this.test.jobId,
      solution: this.solution
    };

    this.testService.submitDesignSolution(submission).subscribe({
      next: () => {
        this.router.navigate(['/job-tests']);
      },
      error: (error) => {
        console.error('Error submitting solution:', error);
        this.isSubmitting = false;
      }
    });
  }

  goBack() {
    if (confirm('Are you sure you want to leave? Your progress will be lost.')) {
      clearInterval(this.timerInterval);
      this.router.navigate(['/job-tests']);
    }
  }
}