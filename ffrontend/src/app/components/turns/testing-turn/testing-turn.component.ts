import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../../commons/components/navbar/navbar.component';
import { DtoKerdoivLetrehozas } from '../../../commons/dtos/DtoJob';
import { JobApplicationService } from '../../../services/job-application/job-application.service';

interface TestingTask {
  type: string;
  description: string;
}

@Component({
  selector: 'app-testing-turn',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './testing-turn.component.html',
  styleUrls: ['./testing-turn.component.css']
})
export class TestingTurnComponent implements OnInit {
  pageTitle: string = '';
  turnForm!: FormGroup;

  testingTypes = [
    'Functional Testing',
    'Integration Testing',
    'UI/UX Testing',
    'Performance Testing',
    'Security Testing',
    'API Testing',
    'Mobile Testing',
    'Accessibility Testing',
    'Regression Testing',
    'Usability Testing'
  ];

  testingTools = [
    'Selenium',
    'Postman',
    'JMeter',
    'Cypress',
    'Jest',
    'TestNG',
    'JUnit',
    'Manual Testing',
    'Chrome DevTools',
    'Lighthouse'
  ];

  priorityLevels = [
    { value: 'critical', label: 'Critical' },
    { value: 'high', label: 'High' },
    { value: 'medium', label: 'Medium' },
    { value: 'low', label: 'Low' }
  ];

  defaultTestScenarios: TestingTask[] = [
    { type: 'Happy Path', description: 'Test the main successful flow' },
    { type: 'Edge Cases', description: 'Test boundary conditions' },
    { type: 'Error Handling', description: 'Test error scenarios' },
    { type: 'Performance', description: 'Test system performance' }
  ];

  constructor(
    private jobApplicationService: JobApplicationService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForm();

    const turnName = this.route.snapshot.queryParamMap.get('name');
    if (turnName) {
      this.pageTitle = turnName;
    } else {
      const turnType = this.getTurnTypeFromRoute();
      this.pageTitle = `${turnType} Turn`;
    }

    const turnId = this.route.snapshot.paramMap.get('id');
    if (turnId) {
      this.loadTurnData(turnId);
    } else {
      this.defaultTestScenarios.forEach(() => this.addTestScenario());
    }
  }

  private getTurnTypeFromRoute(): string {
    const currentRoute = this.router.url;
    if (currentRoute.includes('programming')) return 'Programming';
    if (currentRoute.includes('design')) return 'Design';
    if (currentRoute.includes('algorithms')) return 'Algorithms';
    if (currentRoute.includes('testing')) return 'Testing';
    if (currentRoute.includes('devops')) return 'DevOps';
    return 'Turn';
  }

  private initForm() {
    this.turnForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', [Validators.required, Validators.minLength(50)]],
      testingType: ['', Validators.required],
      applicationUrl: ['', [Validators.required, Validators.pattern('https?://.+')]],
      timeLimit: [60, [Validators.required, Validators.min(15)]],
      requiredTools: this.fb.array([]),
      testEnvironment: this.fb.group({
        operatingSystem: ['', Validators.required],
        browser: ['', Validators.required],
        resolution: ['', Validators.required],
        additionalRequirements: ['']
      }),
      testScenarios: this.fb.array([]),
      testCases: this.fb.array([]),
      bugReportTemplate: this.fb.group({
        steps: ['', Validators.required],
        expectedResult: ['', Validators.required],
        actualResult: ['', Validators.required],
        severity: ['medium', Validators.required],
        priority: ['medium', Validators.required],
        attachments: [true]
      }),
      evaluationCriteria: this.fb.array([])
    });
  }

  private loadTurnData(turnId: string) {
    console.log('Loading testing turn data for ID:', turnId);
  }

  // Getters for form arrays
  get requiredTools() {
    return this.turnForm.get('requiredTools') as FormArray;
  }

  get testScenarios() {
    return this.turnForm.get('testScenarios') as FormArray;
  }

  get testCases() {
    return this.turnForm.get('testCases') as FormArray;
  }

  get evaluationCriteria() {
    return this.turnForm.get('evaluationCriteria') as FormArray;
  }

  // Methods to add form array items
  addTool() {
    const tool = this.fb.group({
      name: ['', Validators.required],
      version: ['', Validators.required],
      purpose: ['', Validators.required]
    });
    this.requiredTools.push(tool);
  }

  removeTool(index: number) {
    this.requiredTools.removeAt(index);
  }

  addTestScenario() {
    const scenario = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      preconditions: ['', Validators.required],
      priority: ['medium', Validators.required]
    });
    this.testScenarios.push(scenario);
  }

  removeTestScenario(index: number) {
    this.testScenarios.removeAt(index);
  }

  addTestCase() {
    const testCase = this.fb.group({
      scenarioId: ['', Validators.required],
      title: ['', Validators.required],
      steps: this.fb.array([]),
      expectedResult: ['', Validators.required],
      testData: ['', Validators.required],
      automatable: [false],
      points: [10, [Validators.required, Validators.min(1), Validators.max(100)]]
    });
    this.testCases.push(testCase);
  }

  removeTestCase(index: number) {
    this.testCases.removeAt(index);
  }

  addTestStep(testCaseIndex: number) {
    const testCase = this.testCases.at(testCaseIndex);
    const steps = testCase.get('steps') as FormArray;
    steps.push(this.fb.control('', Validators.required));
  }

  removeTestStep(testCaseIndex: number, stepIndex: number) {
    const testCase = this.testCases.at(testCaseIndex);
    const steps = testCase.get('steps') as FormArray;
    steps.removeAt(stepIndex);
  }

  addEvaluationCriterion() {
    const criterion = this.fb.group({
      criterion: ['', Validators.required],
      weight: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      description: ['', Validators.required]
    });
    this.evaluationCriteria.push(criterion);
  }

  removeEvaluationCriterion(index: number) {
    this.evaluationCriteria.removeAt(index);
  }

  calculateTotalWeight(): number {
    return this.evaluationCriteria.controls.reduce((sum, criterion) => 
      sum + (criterion.get('weight')?.value || 0), 0);
  }

  onSubmit() {
    if (this.turnForm.valid) {
      const totalWeight = this.calculateTotalWeight();
      if (totalWeight !== 100) {
        alert('Total evaluation criteria weight must equal 100');
        return;
      }
      let kerdoiv : DtoKerdoivLetrehozas = {
        nev : this.turnForm.get('Title')?.value,
        kor : 0,
        allasId : 6,
        kitoltesPerc :this.turnForm.get('timeLimit')?.value,
        kerdesek : [
          {
            kifejtos: true,       
            program: false,        
            valasztos: false,      
            szoveg: this.turnForm.get('problemDescription')?.value, 
            programozosAlapszoveg:"", 
            tesztesetek: [
              {
                bemenet:"", 
                kimenet: ""  
              }
            ],
            valaszok: [
              {
                valaszSzoveg: "", 
                helyes: true         
              },
              
            ]
          }, 
        ]
    }
    console.log(kerdoiv);
    this.jobApplicationService.addRound(kerdoiv).subscribe({
      next: (response) =>{
          
      },
      error: (error) => {
          console.log(error)
      }

    });
      console.log(this.turnForm.value);
      this.router.navigate(['/new-job']);
    } else {
      this.markFormGroupTouched(this.turnForm);
    }
  }

  private markFormGroupTouched(formGroup: FormGroup | FormArray) {
    Object.values(formGroup.controls).forEach(control => {
      if (control instanceof FormGroup || control instanceof FormArray) {
        this.markFormGroupTouched(control);
      } else {
        control.markAsTouched();
      }
    });
  }

  getStepsFormArray(testCase: AbstractControl): FormArray {
    return testCase.get('steps') as FormArray;
  }

  getTestCaseSteps(testCaseIndex: number): FormArray {
    return (this.testCases.at(testCaseIndex).get('steps') as FormArray);
  }
}