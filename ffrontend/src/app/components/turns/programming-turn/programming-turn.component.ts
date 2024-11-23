import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../../commons/components/navbar/navbar.component';

@Component({
  selector: 'app-programming-turn',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './programming-turn.component.html',
  styleUrls: ['./programming-turn.component.css']
})
export class ProgrammingTurnComponent implements OnInit {
  pageTitle: string = '';
  turnForm!: FormGroup;
  programmingLanguages = ['Python', 'JavaScript', 'Java', 'C#', 'C++'];
  
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
      description: ['', [Validators.required, Validators.minLength(10)]],
      programmingLanguage: ['Python', Validators.required],
      timeLimit: [60, [Validators.required, Validators.min(1)]],
      testCases: this.fb.array([]),
      codeTemplate: ['', Validators.required]
    });
  }

  private loadTurnData(turnId: string) {
    // Itt töltjük be a meglévő turn adatait (ha folytatjuk)
    console.log('Loading turn data for ID:', turnId);
  }

  get testCases() {
    return this.turnForm.get('testCases') as FormArray;
  }

  addTestCase() {
    const testCase = this.fb.group({
      input: ['', Validators.required],
      expectedOutput: ['', Validators.required]
    });
    this.testCases.push(testCase);
  }

  removeTestCase(index: number) {
    this.testCases.removeAt(index);
  }

  onSubmit() {
    if (this.turnForm.valid) {
      console.log(this.turnForm.value);
      // Itt mentjük az állást
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