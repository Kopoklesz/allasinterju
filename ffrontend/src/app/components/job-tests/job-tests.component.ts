import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { ActivatedRoute, Router } from '@angular/router';
import { JobTestsService } from '../../services/job-tests/job-tests.service';
import { DtoTest } from '../../commons/dtos/DtoTest';
import { DtoTestState } from '../../commons/dtos/DtoTestState';
import { Subject } from 'rxjs';
import { takeUntil, finalize, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-job-tests',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './job-tests.component.html',
  styleUrls: ['./job-tests.component.css']
})
export class JobTestsComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private jobId: number | null = null;
  
  tests: DtoTest[] = [];
  testStates: DtoTestState[] = [];
  currentTest: DtoTest | null = null;
  currentTestIndex: number = 0;
  
  isLoading: boolean = false;
  isSaving: boolean = false;
  
  private autoSaveInterval: any;
  private lastDraft: any = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jobTestsService: JobTestsService
  ) {}

  ngOnInit() {
    this.jobId = Number(this.route.snapshot.paramMap.get('id'));
    if (!this.jobId) {
      this.router.navigate(['/']);
      return;
    }

    this.loadTests();
    this.setupAutoSave();
    window.addEventListener('beforeunload', this.handleBeforeUnload.bind(this));
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
    
    if (this.autoSaveInterval) {
      clearInterval(this.autoSaveInterval);
    }
    
    window.removeEventListener('beforeunload', this.handleBeforeUnload.bind(this));
    this.saveDraft();
  }
  
  setCurrentTest(index: number) {
    if (this.canAccessTest(index) && index >= 0 && index < this.tests.length) {
      this.currentTest = this.tests[index];
      this.currentTestIndex = index;
      this.loadDraft();
    }
  }

  canAccessTest(index: number): boolean {
    if (index === 0) return true;
    return this.tests
      .slice(0, index)
      .every(test => this.getTestState(test.id)?.isCompleted);
  }

  getTestState(testId: number): DtoTestState | undefined {
    return this.testStates.find(state => state.testId === testId);
  }

  private loadDraft() {
    if (!this.jobId || !this.currentTest) return;

    const state = this.getTestState(this.currentTest.id);
    if (state && state.answers) {
      try {
        this.lastDraft = JSON.parse(state.answers);
        this.updateEditorContent(this.lastDraft);
      } catch (e) {
        console.error('Error parsing draft answers:', e);
      }
    } else {
      this.lastDraft = null;
      this.updateEditorContent(null);
    }
  }

  private updateEditorContent(content: any) {
    if (!this.currentTest) return;

    switch(this.currentTest.type) {
      case 'programming':
        // TODO: Update code editor content
        break;
      case 'algorithm':
        // TODO: Update algorithm editor content
        break;
      case 'design':
        // TODO: Update design editor content
        break;
    }
  }

  onAnswerChange(answer: any) {
    this.lastDraft = answer;
    this.saveDraft();
  }

  private saveDraft() {
    if (!this.jobId || !this.currentTest || !this.lastDraft || this.isSaving) return;

    this.isSaving = true;
    this.jobTestsService.saveDraftAnswer(
      this.jobId,
      this.currentTest.id,
      this.lastDraft
    ).pipe(
      takeUntil(this.destroy$),
      finalize(() => this.isSaving = false),
      catchError(error => {
        console.error('Error saving draft:', error);
        // TODO: Error a felhasználónak
        throw error;
      })
    ).subscribe();
  }

  private loadTests() {
    if (!this.jobId) return;

    this.isLoading = true;
    this.jobTestsService.getTestsForJob(this.jobId)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.isLoading = false),
        catchError(error => {
          console.error('Error loading tests:', error);
          // TODO: Error a felhasználónak
          throw error;
        })
      )
      .subscribe(tests => {
        this.tests = tests;
        this.loadTestStates();
      });
  }

  private loadTestStates() {
    if (!this.jobId) return;

    this.isLoading = true;
    this.jobTestsService.getTestStates(this.jobId)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.isLoading = false),
        catchError(error => {
          console.error('Error loading test states:', error);
          // TODO: Error a felhasználónak
          throw error;
        })
      )
      .subscribe(states => {
        this.testStates = states;
        const lastIncompleteIndex = this.tests.findIndex(test => 
          !states.find(state => state.testId === test.id)?.isCompleted
        );
        this.setCurrentTest(lastIncompleteIndex >= 0 ? lastIncompleteIndex : 0);
      });
  }

  private setupAutoSave() {
    this.autoSaveInterval = setInterval(() => {
      this.saveDraft();
    }, 30000);
  }

  private handleBeforeUnload(event: BeforeUnloadEvent) {
    this.saveDraft();
    event.returnValue = 'Are you sure you want to leave? Your progress will be saved.';
    return event.returnValue;
  }

  completeTest(testId: number) {
    if (!this.jobId || this.isSaving) return;

    this.isSaving = true;
    this.jobTestsService.saveTestState(this.jobId, testId, {
      isCompleted: true,
      answers: JSON.stringify(this.lastDraft)
    }).pipe(
      takeUntil(this.destroy$),
      finalize(() => this.isSaving = false),
      catchError(error => {
        console.error('Error completing test:', error);
        // TODO: Error a felhasználónak
        throw error;
      })
    ).subscribe(newState => {
      const test = this.tests.find(t => t.id === testId);
      if (test) {
        const state = this.testStates.find(s => s.testId === testId);
        if (state) {
          state.isCompleted = true;
        } else {
          this.testStates.push(newState);
        }
      }
    });
  }

  get isLastTest(): boolean {
    return this.currentTestIndex === this.tests.length - 1;
  }

  get canNavigateNext(): boolean {
    return !!this.currentTest && 
           !!this.getTestState(this.currentTest.id)?.isCompleted && 
           !this.isLastTest;
  }

  onNext() {
    if (this.canNavigateNext) {
      this.setCurrentTest(this.currentTestIndex + 1);
    }
  }

  onFinish() {
    if (!this.jobId) return;
    const allCompleted = this.tests.every(test => 
      this.getTestState(test.id)?.isCompleted
    );

    if (!allCompleted) {
      // TODO: figyelmeztetés a felhasználónak
      return;
    }

    this.router.navigate(['/job-application', this.jobId]);
  }
}