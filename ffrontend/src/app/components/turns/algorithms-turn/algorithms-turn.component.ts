import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../../commons/components/navbar/navbar.component';

interface ComplexityInfo {
  type: string;
  description: string;
}

@Component({
  selector: 'app-algorithms-turn',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './algorithms-turn.component.html',
  styleUrls: ['./algorithms-turn.component.css']
})
export class AlgorithmsTurnComponent implements OnInit {
  pageTitle: string = '';
  turnForm!: FormGroup;
  
  algorithmCategories = [
    'Sorting',
    'Searching',
    'Dynamic Programming',
    'Graph Algorithms',
    'String Manipulation',
    'Tree Algorithms',
    'Array Manipulation',
    'Recursion',
    'Greedy Algorithms',
    'Backtracking'
  ];

  complexityTypes: ComplexityInfo[] = [
    { type: 'O(1)', description: 'Constant Time' },
    { type: 'O(log n)', description: 'Logarithmic Time' },
    { type: 'O(n)', description: 'Linear Time' },
    { type: 'O(n log n)', description: 'Linearithmic Time' },
    { type: 'O(n²)', description: 'Quadratic Time' },
    { type: 'O(2ⁿ)', description: 'Exponential Time' }
  ];

  difficultyLevels = [
    { value: 'easy', label: 'Easy' },
    { value: 'medium', label: 'Medium' },
    { value: 'hard', label: 'Hard' }
  ];

  constructor(
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
      category: ['', Validators.required],
      difficulty: ['medium', Validators.required],
      timeLimit: [60, [Validators.required, Validators.min(5)]],
      memoryLimit: [256, [Validators.required, Validators.min(16)]],
      problemDescription: ['', [Validators.required, Validators.minLength(50)]],
      inputFormat: ['', Validators.required],
      outputFormat: ['', Validators.required],
      constraints: this.fb.array([]),
      examples: this.fb.array([]),
      expectedTimeComplexity: ['', Validators.required],
      expectedSpaceComplexity: ['', Validators.required],
      hints: this.fb.array([]),
      explanation: ['', Validators.required],
      sampleSolution: ['', Validators.required],
      testCases: this.fb.array([])
    });

    // Add default items
    this.addConstraint();
    this.addExample();
    this.addTestCase();
  }

  private loadTurnData(turnId: string) {
    // Here you would typically load the turn data from your service
    console.log('Loading algorithm turn data for ID:', turnId);
  }

  // Getters for form arrays
  get constraints() {
    return this.turnForm.get('constraints') as FormArray;
  }

  get examples() {
    return this.turnForm.get('examples') as FormArray;
  }

  get hints() {
    return this.turnForm.get('hints') as FormArray;
  }

  get testCases() {
    return this.turnForm.get('testCases') as FormArray;
  }

  // Methods to add/remove form array items
  addConstraint() {
    const constraint = this.fb.control('', Validators.required);
    this.constraints.push(constraint);
  }

  removeConstraint(index: number) {
    this.constraints.removeAt(index);
  }

  addExample() {
    const example = this.fb.group({
      input: ['', Validators.required],
      output: ['', Validators.required],
      explanation: ['']
    });
    this.examples.push(example);
  }

  removeExample(index: number) {
    this.examples.removeAt(index);
  }

  addHint() {
    const hint = this.fb.control('', Validators.required);
    this.hints.push(hint);
  }

  removeHint(index: number) {
    this.hints.removeAt(index);
  }

  addTestCase() {
    const testCase = this.fb.group({
      input: ['', Validators.required],
      output: ['', Validators.required],
      isHidden: [true],
      points: [10, [Validators.required, Validators.min(1), Validators.max(100)]]
    });
    this.testCases.push(testCase);
  }

  removeTestCase(index: number) {
    this.testCases.removeAt(index);
  }

  validateComplexity(complexity: string): boolean {
    return this.complexityTypes.some(c => c.type === complexity);
  }

  calculateTotalPoints(): number {
    return this.testCases.controls.reduce((sum, testCase) => 
      sum + testCase.get('points')?.value || 0, 0);
  }

  onSubmit() {
    if (this.turnForm.valid) {
      const totalPoints = this.calculateTotalPoints();
      if (totalPoints !== 100) {
        alert('Total test case points must equal 100');
        return;
      }

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
}