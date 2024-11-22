import { Component, Input, Output, EventEmitter } from '@angular/core';
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
      <section class="problem-section">
        <div class="problem-header">
          <h3>Problem Description</h3>
          <div class="difficulty" [class]="difficultyClass">
            {{ difficulty }}
          </div>
        </div>
        <div class="description" [innerHTML]="description"></div>
      </section>

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

      <section class="constraints-section">
        <h4>Constraints</h4>
        <ul class="constraints-list">
          <li *ngFor="let constraint of constraints" class="constraint-item">
            <span class="constraint-desc">{{ constraint.description }}</span>
            <code class="constraint-value">{{ constraint.value }}</code>
          </li>
        </ul>
      </section>

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
          
          <textarea
            class="code-editor"
            [(ngModel)]="code"
            (ngModelChange)="onCodeChange()"
            [placeholder]="getLanguageTemplate()">
          </textarea>
        </div>
      </section>

      <section class="action-section">
        <button 
          class="submit-button" 
          [disabled]="isSubmitting || !code.trim()"
          (click)="submitSolution()">
          {{ isSubmitting ? 'Submitting...' : 'Submit Solution' }}
        </button>
      </section>
    </div>
  `,
  styleUrls: ['./algorithm-test.component.css']
})
export class AlgorithmTestComponent {
  @Input() description: string = '';
  @Input() difficulty: 'easy' | 'medium' | 'hard' = 'medium';
  @Input() inputFormat: string = '';
  @Input() outputFormat: string = '';
  @Input() constraints: AlgorithmConstraint[] = [];
  @Input() examples: AlgorithmExample[] = [];
  @Input() expectedTimeComplexity: string = 'O(n)';
  @Input() expectedSpaceComplexity: string = 'O(1)';
  @Output() submitAnswer = new EventEmitter<any>();

  code: string = '';
  selectedLanguage: string = 'python';
  isSubmitting: boolean = false;

  availableLanguages = [
    { value: 'python', label: 'Python' },
    { value: 'javascript', label: 'JavaScript' },
    { value: 'java', label: 'Java' },
    { value: 'cpp', label: 'C++' }
  ];

  private languageTemplates: Record<string, string> = {
    python: '# Write your Python solution here\n\ndef solve(input):\n    # Your code here\n    pass\n',
    javascript: '// Write your JavaScript solution here\nfunction solve(input) {\n    // Your code here\n}\n',
    java: 'public class Solution {\n    public static void solve(String input) {\n        // Your code here\n    }\n}',
    cpp: '#include <iostream>\n\nclass Solution {\npublic:\n    void solve(string input) {\n        // Your code here\n    }\n};'
  };

  get difficultyClass(): string {
    return `difficulty-${this.difficulty}`;
  }

  getLanguageTemplate(): string {
    return this.languageTemplates[this.selectedLanguage] || '';
  }

  onLanguageChange() {
    if (!this.code.trim() || this.code === this.getLanguageTemplate()) {
      this.code = this.getLanguageTemplate();
    }
    this.emitCurrentSolution();
  }

  onCodeChange() {
    this.emitCurrentSolution();
  }

  submitSolution() {
    if (!this.code.trim() || this.isSubmitting) return;

    this.isSubmitting = true;
    this.submitAnswer.emit({
      language: this.selectedLanguage,
      code: this.code,
      timestamp: new Date()
    });
  }

  private emitCurrentSolution() {
    this.submitAnswer.emit({
      language: this.selectedLanguage,
      code: this.code,
      isDraft: true
    });
  }
}