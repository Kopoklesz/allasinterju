import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface AlgorithmExample {
  input: string;
  output: string;
  explanation: string;
}

interface AlgorithmConstraint {
  description: string;
  value: string;
}

@Component({
  selector: 'app-algorithm-test',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="algorithm-container">
      <!-- Problem Description Section -->
      <section class="problem-section">
        <div class="problem-header">
          <h3>Problem Description</h3>
          <div class="difficulty" [class]="difficultyClass">
            {{ difficulty }}
          </div>
        </div>
        <div class="description" [innerHTML]="description"></div>
      </section>

      <!-- Input/Output Format Section -->
      <section class="format-section">
        <div class="input-format">
          <h4>Input Format</h4>
          <pre>{{ inputFormat }}</pre>
        </div>
        <div class="output-format">
          <h4>Output Format</h4>
          <pre>{{ outputFormat }}</pre>
        </div>
      </section>

      <!-- Constraints Section -->
      <section class="constraints-section">
        <h4>Constraints</h4>
        <ul class="constraints-list">
          <li *ngFor="let constraint of constraints" class="constraint-item">
            <span class="constraint-desc">{{ constraint.description }}</span>
            <code class="constraint-value">{{ constraint.value }}</code>
          </li>
        </ul>
      </section>

      <!-- Examples Section -->
      <section class="examples-section">
        <h4>Examples</h4>
        <div class="examples-container">
          <div *ngFor="let example of examples; let i = index" class="example-card">
            <div class="example-header">Example {{ i + 1 }}</div>
            <div class="example-content">
              <div class="example-input">
                <strong>Input:</strong>
                <pre>{{ example.input }}</pre>
              </div>
              <div class="example-output">
                <strong>Output:</strong>
                <pre>{{ example.output }}</pre>
              </div>
              <div class="example-explanation" *ngIf="example.explanation">
                <strong>Explanation:</strong>
                <p>{{ example.explanation }}</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- Solution Section -->
      <section class="solution-section">
        <div class="solution-header">
          <h4>Your Solution</h4>
          <div class="complexity-hints">
            <span class="complexity-item">
              Expected Time: <code>{{ expectedTimeComplexity }}</code>
            </span>
            <span class="complexity-item">
              Expected Space: <code>{{ expectedSpaceComplexity }}</code>
            </span>
          </div>
        </div>
        
        <div class="solution-editor">
          <select [(ngModel)]="selectedLanguage" (change)="onLanguageChange()" class="language-select">
            <option *ngFor="let lang of availableLanguages" [value]="lang.value">
              {{ lang.label }}
            </option>
          </select>
          
          <div #editorContainer class="code-editor"></div>
        </div>

        <!-- Test Cases Results -->
        <div class="test-results" *ngIf="testResults.length > 0">
          <h4>Test Results</h4>
          <div class="results-summary">
            <div class="result-stat passed">
              {{ getPassedTestCount() }} Passed
            </div>
            <div class="result-stat failed">
              {{ getFailedTestCount() }} Failed
            </div>
            <div class="result-stat">
              {{ testResults.length }} Total
            </div>
          </div>
          
          <div class="results-detail">
            <div *ngFor="let result of testResults" class="result-item" [class.passed]="result.passed">
              <div class="result-header">
                <span class="result-status">{{ result.passed ? '✓' : '✗' }}</span>
                <span class="result-title">Test Case {{ result.testCase }}</span>
                <span class="result-time" *ngIf="result.executionTime">
                  {{ result.executionTime }}ms
                </span>
              </div>
              <div class="result-details" *ngIf="!result.passed">
                <div class="result-expected">
                  <strong>Expected:</strong>
                  <pre>{{ result.expected }}</pre>
                </div>
                <div class="result-actual">
                  <strong>Got:</strong>
                  <pre>{{ result.actual }}</pre>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- Action Buttons -->
      <section class="action-section">
        <button 
          class="run-button" 
          [disabled]="isRunning"
          (click)="runSolution()">
          {{ isRunning ? 'Running...' : 'Run Tests' }}
        </button>
        <button 
          class="submit-button" 
          [disabled]="!canSubmit"
          (click)="submitSolution()">
          Submit Solution
        </button>
      </section>
    </div>
  `,
  styleUrls: ['./algorithm-test.component.css']
})
export class AlgorithmTestComponent implements OnInit {
  @Input() description: string = '';
  @Input() difficulty: 'easy' | 'medium' | 'hard' = 'medium';
  @Input() inputFormat: string = '';
  @Input() outputFormat: string = '';
  @Input() constraints: AlgorithmConstraint[] = [];
  @Input() examples: AlgorithmExample[] = [];
  @Input() expectedTimeComplexity: string = 'O(n)';
  @Input() expectedSpaceComplexity: string = 'O(1)';
  @Output() answerChange = new EventEmitter<any>();

  selectedLanguage: string = 'python';
  isRunning: boolean = false;
  testResults: any[] = [];
  canSubmit: boolean = false;

  availableLanguages = [
    { value: 'python', label: 'Python' },
    { value: 'javascript', label: 'JavaScript' },
    { value: 'java', label: 'Java' },
    { value: 'cpp', label: 'C++' }
  ];

  get difficultyClass(): string {
    return `difficulty-${this.difficulty}`;
  }

  ngOnInit() {
    this.initializeEditor();
  }

  private initializeEditor() {
    // TODO: Initialize Monaco editor
    console.log('Editor initialized');
  }

  onLanguageChange() {
    // Update editor language and template
    this.emitCurrentSolution();
  }

  runSolution() {
    this.isRunning = true;
    // Mock test execution
    setTimeout(() => {
      this.testResults = [
        {
          testCase: 1,
          passed: true,
          executionTime: 5
        },
        {
          testCase: 2,
          passed: false,
          expected: '5',
          actual: '4',
          executionTime: 3
        }
      ];
      this.isRunning = false;
      this.canSubmit = this.getPassedTestCount() === this.testResults.length;
    }, 1500);
  }

  submitSolution() {
    if (!this.canSubmit) return;
    
    const solution = {
      language: this.selectedLanguage,
      code: 'editor.getValue()', // TODO: Get actual code
      testResults: this.testResults
    };
    this.answerChange.emit(solution);
  }

  getPassedTestCount(): number {
    return this.testResults.filter(result => result.passed).length;
  }

  getFailedTestCount(): number {
    return this.testResults.length - this.getPassedTestCount();
  }

  private emitCurrentSolution() {
    const solution = {
      language: this.selectedLanguage,
      code: 'editor.getValue()', // TODO: Get actual code
      lastModified: new Date()
    };
    this.answerChange.emit(solution);
  }
}