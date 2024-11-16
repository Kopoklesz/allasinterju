import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { DtoTest } from '../../commons/dtos/DtoTest';
import { DtoTestState } from '../../commons/dtos/DtoTestState';

@Injectable({
  providedIn: 'root'
})
export class JobTestsService {
  private apiUrl = 'http://localhost:5000/api';
  private testStatesSubject = new BehaviorSubject<DtoTestState[]>([]);
  
  testStates$ = this.testStatesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTestsForJob(jobId: number): Observable<DtoTest[]> {
    return this.http.get<DtoTest[]>(`${this.apiUrl}/jobs/${jobId}/tests`);
  }

  getTestStates(jobId: number): Observable<DtoTestState[]> {
    return this.http.get<DtoTestState[]>(`${this.apiUrl}/jobs/${jobId}/test-states`)
      .pipe(
        tap(states => this.testStatesSubject.next(states))
      );
  }

  saveTestState(jobId: number, testId: number, state: Partial<DtoTestState>): Observable<DtoTestState> {
    return this.http.post<DtoTestState>(
      `${this.apiUrl}/jobs/${jobId}/tests/${testId}/state`,
      state
    ).pipe(
      tap(newState => {
        const currentStates = this.testStatesSubject.value;
        const index = currentStates.findIndex(s => s.testId === testId);
        if (index >= 0) {
          currentStates[index] = newState;
        } else {
          currentStates.push(newState);
        }
        this.testStatesSubject.next(currentStates);
      })
    );
  }

  checkAllTestsCompleted(jobId: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/jobs/${jobId}/tests/completed`);
  }

  saveDraftAnswer(jobId: number, testId: number, answer: any): Observable<void> {
    return this.http.post<void>(
      `${this.apiUrl}/jobs/${jobId}/tests/${testId}/draft`,
      { answer }
    );
  }
}