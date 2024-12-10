import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { JobTestsService } from '../../services/job-tests/job-tests.service';
import { DtoTest } from '../../commons/dtos/DtoTest';
import { ProgrammingTestTakeComponent } from '../tests/programming-take/programming-test-take.component';

//import { AlgorithmTestComponent } from '../tests/algorithm/algorithm-test.component';
//import { DesignTestComponent } from '../tests/design/design-test.component';
//import { TestingTestComponent } from '../tests/testing/testing-test.component';
//import { DevopsTestComponent } from '../tests/devops/devops-test.component';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DtoRound } from '../../commons/dtos/DtoRound';
import { RSolveP } from '../../commons/dtos/DtoProgrammingAdd';
import { response } from 'express';

@Component({
  selector: 'app-job-tests',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    ProgrammingTestTakeComponent,
    //AlgorithmTestComponent,
    //DesignTestComponent,
    //TestingTestComponent,
    //DevopsTestComponent
  ],
  templateUrl: './job-tests.component.html',
  styleUrls: ['./job-tests.component.css']
})
export class JobTestsComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  
  programmingRoundQuestions : RSolveP | undefined;

  rounds : DtoRound[] = [];
 // tests: DtoTest[] = [];
  currentTest: DtoRound | null = null;
  jobId: number | null = null;
  isLoading = false;
  allTestsCompleted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jobTestsService: JobTestsService
  ) {}

  ngOnInit() {
    
    const jobId = this.route.snapshot.paramMap.get('id');
    if (!jobId) {
      this.router.navigate(['/']);
      return;
    }

    this.jobId = parseInt(jobId);
    this.loadTests();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private loadTests() {
    if (!this.jobId) return;

    this.isLoading = true;
    this.jobTestsService.getTestsForJob(this.jobId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          
          this.rounds = response
          console.log(this.rounds)
          
          //this.tests = tests.sort((a, b) => (a.order || 0) - (b.order || 0));
          this.checkAllTestsCompleted();
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error loading tests:', error);
          this.isLoading = false;
        }
      });
  }

  private checkAllTestsCompleted() {
    //this.allTestsCompleted = this.tests.every(test => test.isCompleted);
  }

  /*canStartTest(index: number): boolean {
    // Első teszt mindig indítható
    if (index === 0) return true;
    // A többi csak akkor, ha az előző már kész
    return this.tests[index - 1]?.isCompleted || false;
  }*/

  startTest(round: DtoRound) {
  
   // if (!this.canStartTest(this.round.findIndex(t => t.id === round.id))) {
     
     // return;
    //}
    
    this.currentTest = round;
    console.log(round.kerdoivId);
    this.jobTestsService.getProgrammingSolve(round.kerdoivId).subscribe({
      next: (response) =>{
        this.programmingRoundQuestions = response;
        console.log(this.programmingRoundQuestions)
      },
      error: (error) => {

      }

    });

  }

  onTestComplete(result: any) {
    if (!this.currentTest || !this.jobId) return;

    this.isLoading = true;
    /*this.jobTestsService.saveTestState(
      this.jobId,
      this.currentTest.kerdoiv,
      {
        isCompleted: true,
        answers: JSON.stringify(result),
        lastModified: new Date()
      }
    ).pipe(takeUntil(this.destroy$))
    .subscribe({
      next: () => {
        const testIndex = this.tests.findIndex(t => t.id === this.currentTest?.id);
        if (testIndex !== -1) {
          this.tests[testIndex].isCompleted = true;
          this.checkAllTestsCompleted();
        }

        if (testIndex < this.tests.length - 1) {
          const nextTest = this.tests[testIndex + 1];
          if (confirm('Would you like to start the next test?')) {
            this.startTest(nextTest);
          } else {
            this.currentTest = null;
          }
        } else {
          this.currentTest = null;
          if (this.allTestsCompleted) {
            alert('Congratulations! You have completed all tests.');
          }
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error saving test result:', error);
        this.isLoading = false;
      }
    });*/
  }
}