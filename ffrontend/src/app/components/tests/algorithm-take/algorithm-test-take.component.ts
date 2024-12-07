import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { JobTestsService } from '../../../services/job-tests/job-tests.service';
import { DtoTest } from '../../../commons/dtos/DtoTest';
import { BAlgorithmAdd } from '../../../commons/dtos/DtoAlgorithmAdd';
import { AlgorithmSolutionSubmission } from '../../../commons/dtos/DtoSubmissions';

@Component({
  selector: 'app-algorithm-test-take',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './algorithm-test-take.component.html',
  styleUrls: ['./algorithm-test-take.component.css']
})
export class AlgorithmTestTakeComponent implements OnInit {
  test: BAlgorithmAdd | null = null;
  solution: string = '';
  isSubmitting = false;
  visibleTestCases: Array<{input: string, output: string}> = [];

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

  private loadTest(testId: number) {
    this.testService.getTest(testId).subscribe({
      next: (test: BAlgorithmAdd) => {
        this.test = test;
        if (test.testCases) {
          this.visibleTestCases = test.testCases
            .filter(tc => !tc.hidden)
            .map(tc => ({
              input: tc.input,
              output: tc.output
            }));
        }
      },
      error: (error) => {
        console.error('Error loading test:', error);
      }
    });
  }

  submitSolution() {
    if (!this.test || !this.solution.trim() || this.isSubmitting) return;

    this.isSubmitting = true;
    
    const submission: AlgorithmSolutionSubmission = {
      testId: this.test.jobId,
      solution: this.solution,
      timeComplexity: this.test.timeComplexity,
      spaceComplexity: this.test.spaceComplexity
    };

    this.testService.submitAlgorithmSolution(submission).subscribe({
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
    this.router.navigate(['/job-tests']);
  }
}