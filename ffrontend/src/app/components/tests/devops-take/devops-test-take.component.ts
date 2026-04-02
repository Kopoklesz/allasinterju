import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { JobTestsService } from '../../../services/job-tests/job-tests.service';
import { BDevOpsAdd } from '../../../commons/dtos/DtoDevOpsAdd';

@Component({
  selector: 'app-devops-test-take',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './devops-test-take.component.html',
  styleUrls: ['./devops-test-take.component.css']
})
export class DevOpsTestTakeComponent implements OnInit, OnDestroy {
  test: BDevOpsAdd | null = null;
  solution: string = '';
  isSubmitting = false;
  timerInterval: any;
  remainingTime: number = 0;
  expirationTime: Date | null = null;  // Ezt kapjuk a backendtől

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
      next: (test: any) => {
        this.test = test as BDevOpsAdd;
        this.expirationTime = new Date(test.timeLimit) || null;
        this.startTimer();
      },
      error: (error) => {
        console.error('Error loading test:', error);
      }
    });
}

  private startTimer() {
    this.timerInterval = setInterval(() => {
      if (this.expirationTime) {
        const now = new Date();
        this.remainingTime = Math.floor((this.expirationTime.getTime() - now.getTime()) / 1000);
        
        if (this.remainingTime <= 0) {
          clearInterval(this.timerInterval);
          this.autoSubmit();
        }
      }
    }, 1000);
  }

  private autoSubmit() {
    if (!this.isSubmitting) {
      this.submitSolution();
    }
  }

  formatTime(seconds: number): string {
    if (seconds <= 0) return '0:00';
    
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
  }

  submitSolution() {
    if (!this.test || !this.solution.trim() || this.isSubmitting) return;
    
    this.isSubmitting = true;
    this.testService.submitSolution(this.test.jobId, this.solution).subscribe({
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
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
    this.router.navigate(['/job-tests']);
  }
}