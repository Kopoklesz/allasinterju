import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';

@Component({
  selector: 'app-edit-turn',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './edit-turn.component.html',
  styleUrls: ['./edit-turn.component.css']
})
export class EditTurnComponent implements OnInit {
  turnForm!: FormGroup;
  turnName: string = '';
  turnType: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.turnName = this.route.snapshot.paramMap.get('name') || '';
    this.turnType = this.turnName.split(' ')[0];
    this.initForm();
  }

  private initForm() {
    this.turnForm = this.fb.group({
      taskType: ['questions'],
      questions: this.fb.array([]),
      programmingTask: this.fb.group({
        description: ['', Validators.required],
        language: ['python'],
        template: ['', Validators.required],
        testCases: ['', Validators.required]
      }),
      textTask: this.fb.group({
        question: ['', Validators.required],
        minWords: [0, [Validators.required, Validators.min(0)]],
        maxWords: [1000, [Validators.required, Validators.min(1)]]
      })
    });
  }

  get questions() {
    return this.turnForm.get('questions') as FormArray;
  }

  addQuestion() {
    const questionGroup = this.fb.group({
      question: ['', Validators.required],
      options: this.fb.array([]),
      correctAnswer: [0, Validators.required]
    });
    this.questions.push(questionGroup);
    this.addOption(this.questions.length - 1);
  }

  getOptions(questionIndex: number): FormArray {
    return this.questions.at(questionIndex).get('options') as FormArray;
  }

  addOption(questionIndex: number) {
    const options = this.getOptions(questionIndex);
    options.push(this.fb.control('', Validators.required));
  }

  setCorrectAnswer(questionIndex: number, optionIndex: number) {
    this.questions.at(questionIndex).patchValue({
      correctAnswer: optionIndex
    });
  }

  onSubmit() {
    if (this.turnForm.valid) {
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