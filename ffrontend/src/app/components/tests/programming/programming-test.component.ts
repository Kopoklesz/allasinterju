import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface TestCase {
  input: string;
  expectedOutput: string;
}

@Component({
  selector: 'app-programming-test',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="programming-container">
      <div class="description" [innerHTML]="description"></div>
      
      <div class="editor-section">
        <div class="editor-toolbar">
          <select [(ngModel)]="language" (change)="onLanguageChange()">
            <option value="python">Python</option>
            <option value="javascript">JavaScript</option>
            <option value="java">Java</option>
            <option value="csharp">C#</option>
          </select>
          
          <div class="toolbar-actions">
            <button 
              class="action-button" 
              [class.active]="showTestCases"
              (click)="toggleTestCases()">
              Test Cases
            </button>
            <button 
              class="run-button" 
              [disabled]="isSubmitting"
              (click)="submitCode()">
              {{ isSubmitting ? 'Submitting...' : 'Submit Code' }}
            </button>
          </div>
        </div>

        <div class="editor-container">
          <textarea
            class="code-editor"
            [(ngModel)]="code"
            (ngModelChange)="onCodeChange()"
            [placeholder]="getLanguageTemplate()">
          </textarea>
          
          <div class="test-cases-panel" *ngIf="showTestCases">
            <h4>Test Cases</h4>
            <div class="test-case" *ngFor="let testCase of testCases; let i = index">
              <div class="test-case-header">
                <span>Test Case {{i + 1}}</span>
              </div>
              <div class="test-case-content">
                <div class="input">
                  <strong>Input:</strong>
                  <pre>{{testCase.input}}</pre>
                </div>
                <div class="expected">
                  <strong>Expected Output:</strong>
                  <pre>{{testCase.expectedOutput}}</pre>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ProgrammingTestComponent {
  @Input() description: string = '';
  @Input() template: string = '';
  @Input() testCases: TestCase[] = [];
  @Output() submitAnswer = new EventEmitter<any>();

  code: string = '';
  language: 'python' | 'javascript' | 'java' | 'csharp' = 'python';
  isSubmitting: boolean = false;
  showTestCases: boolean = false;

  private languageTemplates: Record<typeof this.language, string> = {
    python: '# Write your Python code here\n\ndef solution():\n    pass\n',
    javascript: '// Write your JavaScript code here\nfunction solution() {\n    \n}\n',
    java: 'public class Solution {\n    public static void main(String[] args) {\n        \n    }\n}',
    csharp: 'public class Solution {\n    public static void Main() {\n        \n    }\n}'
  };

  ngOnInit() {
    this.code = this.template || this.getLanguageTemplate();
  }

  getLanguageTemplate(): string {
    return this.languageTemplates[this.language];
  }

  onLanguageChange() {
    if (!this.code.trim() || this.code === this.getLanguageTemplate()) {
      this.code = this.getLanguageTemplate();
    }
    this.emitCurrentAnswer();
  }

  onCodeChange() {
    this.emitCurrentAnswer();
  }

  toggleTestCases() {
    this.showTestCases = !this.showTestCases;
  }

  submitCode() {
    if (!this.code.trim() || this.isSubmitting) return;

    this.isSubmitting = true;
    this.submitAnswer.emit({
      language: this.language,
      code: this.code
    });
  }

  private emitCurrentAnswer() {
    this.submitAnswer.emit({
      language: this.language,
      code: this.code,
      isDraft: true
    });
  }
}