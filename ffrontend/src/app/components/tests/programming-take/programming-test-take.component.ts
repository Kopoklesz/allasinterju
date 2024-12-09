import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { JobTestsService } from '../../../services/job-tests/job-tests.service';
import { DtoTest } from '../../../commons/dtos/DtoTest';
import { error } from 'console';
import { RSolveP } from '../../../commons/dtos/DtoProgrammingAdd';
import test from 'node:test';

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
  @Input() test ?: DtoTest;
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
    /*this.testService.getTest(testId).subscribe({
      next: (test: DtoTest) => { 
        this.test = {
          id: test.id,
          title: test.name, 
          description: test.description || '',
          type: test.type,
          duration: test.duration,
          isCompleted: test.isCompleted,
          testCases: test.testCases,
          template: test.template
        };
        
        // Ha van template, beállítjuk a kód kezdeti értékét
        if (test.template) {
          this.code = test.template;
        }
      },
      error: (error) => {
        console.error('Error loading test:', error);
      }
    });*/
    console.log(test);
    let kerdoivId = 0;
    this.route.params.subscribe(params => {
      kerdoivId = +params['kerdoivId']; // '+' converts string to number
      
    });
  
    this.testService.getProgrammingSolve(kerdoivId).subscribe({
        next: (respose: RSolveP) => {
            console.log(respose)

        },
        error: (error) => {

        }
    });
  }

  submitSolution() {
    if (!this.test || !this.code.trim() || this.isSubmitting) return;

    this.isSubmitting = true;
    const solution = {
      testId: this.test.id,
      code: this.code
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

  goBack() {
    this.router.navigate(['/job-tests']);
  }
}