import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { JobTestsService } from '../../../services/job-tests/job-tests.service';
import { DtoTest } from '../../../commons/dtos/DtoTest';
import { error } from 'console';
import { RSolveP } from '../../../commons/dtos/DtoProgrammingAdd';
import { finishProg } from '../../../commons/dtos/DtoAlgorithmAdd';

//import test from 'node:test';

interface TestCase {
  input: string;
  expectedOutput: string;
}

/*interface Test {
  id: number;
  title: string;
  description: string; 
  type: string;
  duration: number;
  isCompleted: boolean;
  testCases?: TestCase[];
  template?: string;
}*/



@Component({
  selector: 'app-programming-test-take',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './programming-test-take.component.html',
  styleUrls: ['./programming-test-take.component.css']
})
export class ProgrammingTestTakeComponent implements OnInit {
  @Input() test ?: RSolveP;
  //test: Test | null = null;
  code: string = '';
  isSubmitting = false;

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
    
    console.log(this.test);
  
  }

  submitSolution() {
    if (!this.test || !this.code.trim() || this.isSubmitting) return;


    this.isSubmitting = true;
    let tAtrea = document.getElementById("codeArea") as HTMLInputElement;

    let finishData : finishProg = {
        kerdoivId: this.test.kerdoivId,
        programkod: tAtrea.value
    }
    this.testService.finishProg(finishData).subscribe({
      next: () => {
        this.router.navigate(['/job-tests']);
      },
      error: (error) => {
        console.error('Error submitting solution:', error);
        this.isSubmitting = false;
      }
    });

    /*this.testService.submitSolution(this.test.id, solution).subscribe({
      next: () => {
        this.router.navigate(['/job-tests']);
      },
      error: (error) => {
        console.error('Error submitting solution:', error);
        this.isSubmitting = false;
      }
    });*/
  }

  goBack() {
    this.router.navigate(['/job-tests']);
  }
}