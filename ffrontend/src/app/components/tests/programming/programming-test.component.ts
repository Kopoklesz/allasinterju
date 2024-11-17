import { Component, Input, Output, EventEmitter, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { editor } from 'monaco-editor';
import { MonacoEditorService } from '../../../services/monaco/monaco-editor.service';
import { Subscription } from 'rxjs';
import { PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

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
              [disabled]="isRunning"
              (click)="runCode()">
              {{ isRunning ? 'Running...' : 'Run Code' }}
            </button>
          </div>
        </div>

        <div class="editor-container">
          <div #editorContainer class="monaco-editor"></div>
          
          <div class="test-cases-panel" *ngIf="showTestCases">
            <h4>Test Cases</h4>
            <div class="test-case" *ngFor="let testCase of testCases; let i = index">
              <div class="test-case-header">
                <span>Test Case {{i + 1}}</span>
                <span [class]="getTestResultClass(i)">{{getTestResultText(i)}}</span>
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
                <div class="actual" *ngIf="testResults[i]?.output">
                  <strong>Your Output:</strong>
                  <pre>{{testResults[i].output}}</pre>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="output-panel" *ngIf="output">
          <div class="output-header">
            <h4>Output</h4>
            <button class="clear-button" (click)="clearOutput()">Clear</button>
          </div>
          <pre [class.error]="hasError">{{output}}</pre>
        </div>
      </div>
    </div>
  `,
  styleUrls: ['./programming-test.component.css']
})
export class ProgrammingTestComponent implements OnInit, OnDestroy {
  @Input() description: string = '';  
  @Input() template: string = '';     
  @Input() testCases: TestCase[] = []; 
  @Output() answerChange = new EventEmitter<any>();

  @ViewChild('editorContainer') editorContainer!: ElementRef;

  private editor: editor.IStandaloneCodeEditor | null = null;
  private monacoSubscription?: Subscription;
  
  language: 'python' | 'javascript' | 'java' | 'csharp' = 'python';
  output: string = '';
  hasError: boolean = false;
  isRunning: boolean = false;
  showTestCases: boolean = false;
  testResults: Array<{ passed: boolean, output: string }> = [];

  private languageTemplates: Record<typeof this.language, string> = {
    python: '# Write your Python code here\n\ndef solution():\n    pass\n',
    javascript: '// Write your JavaScript code here\nfunction solution() {\n    \n}\n',
    java: 'public class Solution {\n    public static void main(String[] args) {\n        \n    }\n}',
    csharp: 'public class Solution {\n    public static void Main() {\n        \n    }\n}'
  };

  constructor(
    private monacoService: MonacoEditorService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.monacoSubscription = this.monacoService.monacoLoaded$.subscribe(loaded => {
        if (loaded && this.editorContainer) {
          requestAnimationFrame(() => {
            this.initMonacoEditor();
          });
        }
      });
    }
  }

  private initMonacoEditor() {
    if (!isPlatformBrowser(this.platformId)) return;

    try {
      if (!this.editorContainer) return;

      const editor = this.monacoService.createEditor(
        this.editorContainer.nativeElement,
        {
          value: this.template || this.languageTemplates[this.language],
          language: this.language,
          minimap: {
            enabled: false
          },
          scrollBeyondLastLine: false,
          fontSize: 14,
          padding: {
            top: 10,
            bottom: 10
          }
        }
      );

      if (editor) {
        this.editor = editor;
        this.editor.onDidChangeModelContent(() => {
          this.emitAnswer();
        });
      }
    } catch (error) {
      console.error('Failed to initialize Monaco Editor:', error);
    }
  }

  ngOnDestroy() {
    if (isPlatformBrowser(this.platformId) && this.editor) {
      this.editor.dispose();
    }
    if (this.monacoSubscription) {
      this.monacoSubscription.unsubscribe();
    }
  }

  onLanguageChange() {
    if (this.editor) {
      const model = this.editor.getModel();
      if (model) {
        this.monacoService.setModelLanguage(model, this.language);
        this.editor.setValue(this.languageTemplates[this.language]);
      }
      this.emitAnswer();
    }
  }

  private emitAnswer() {
    if (this.editor) {
      const code = this.editor.getValue();
      this.answerChange.emit({
        language: this.language,
        code
      });
    }
  }

  private async executeCode(code: string, input: string): Promise<string> {
    try {
      // TODO: Implement actual code execution through backend
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve('Mock output');
        }, 1000);
      });
    } catch (error) {
      const errorMessage = error instanceof Error ? error.message : 'Unknown error occurred';
      throw new Error(errorMessage);
    }
  }

  async runCode() {
    if (!this.editor || this.isRunning) return;

    this.isRunning = true;
    this.clearOutput();
    this.testResults = [];

    const code = this.editor.getValue();

    try {
      for (const testCase of this.testCases) {
        const result = await this.executeCode(code, testCase.input);
        this.testResults.push({
          passed: result.trim() === testCase.expectedOutput.trim(),
          output: result
        });
      }

      const passedTests = this.testResults.filter(r => r.passed).length;
      this.output = `Passed ${passedTests} of ${this.testCases.length} test cases`;
      this.hasError = passedTests !== this.testCases.length;
    } catch (error) {
      const errorMessage = error instanceof Error ? error.message : 'Unknown error occurred';
      this.output = `Error: ${errorMessage}`;
      this.hasError = true;
    } finally {
      this.isRunning = false;
    }
  }

  toggleTestCases() {
    this.showTestCases = !this.showTestCases;
  }

  clearOutput() {
    this.output = '';
    this.hasError = false;
  }

  getTestResultClass(index: number): string {
    if (!this.testResults[index]) return '';
    return this.testResults[index].passed ? 'passed' : 'failed';
  }

  getTestResultText(index: number): string {
    if (!this.testResults[index]) return '';
    return this.testResults[index].passed ? 'Passed' : 'Failed';
  }
}