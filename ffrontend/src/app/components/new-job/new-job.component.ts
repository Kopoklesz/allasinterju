import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule, FormBuilder, FormGroup, Validators, ReactiveFormsModule, ValidationErrors, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-job',
  standalone: true,
  imports: [
    NavbarComponent,
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './new-job.component.html',
  styleUrls: ['./new-job.component.css']
})
export class NewJobComponent implements OnInit {
  jobForm!: FormGroup;
  selectedTurn: string = '';
  turns: { name: string; count: number }[] = [];
  turnCount: { [key: string]: number } = {};
  turnTypes: string[] = ['Programming', 'Design', 'Algorithms', 'Testing', 'DevOps'];
  
  workScheduleOptions = [
    { value: 'full', label: 'Full Time' },
    { value: 'part', label: 'Part Time' },
    { value: 'remote', label: 'Remote Work' },
    { value: 'hybrid', label: 'Hybrid' }
  ];

  constructor(
    private fb: FormBuilder,
    private router: Router
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    this.loadSavedTurns();
  }

  private initForm(): void {
    this.jobForm = this.fb.group({
      cim: ['', [
        Validators.required, 
        Validators.minLength(3),
        Validators.maxLength(100)
      ]],
      leiras: ['', [
        Validators.required, 
        Validators.minLength(10),
        Validators.maxLength(2000)
      ]],
      munkarend: ['', Validators.required],
      rovidleiras: ['', [
        Validators.required, 
        Validators.maxLength(200)
      ]],
      telepely: ['', [
        Validators.required,
        Validators.minLength(2)
      ]],
      kitoltesihatarido: ['', [
        Validators.required,
        this.futureDateValidator()
      ]]
    });
  }

  private futureDateValidator() {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null;
      }
      const selectedDate = new Date(control.value);
      const today = new Date();
      today.setHours(0, 0, 0, 0);

      return selectedDate < today ? { pastDate: true } : null;
    };
  }

  private loadSavedTurns(): void {
    // Itt később implementálhatjuk a mentett körök betöltését
    console.log('Loading saved turns...');
  }

  showError(controlName: string): boolean {
    const control = this.jobForm.get(controlName);
    return control ? control.invalid && (control.dirty || control.touched) : false;
  }

  getErrorMessage(controlName: string): string {
    const control = this.jobForm.get(controlName);
    if (!control?.errors) return '';

    if (control.errors['required']) {
      return 'Ez a mező kötelező';
    }
    if (control.errors['minlength']) {
      return `Minimum ${control.errors['minlength'].requiredLength} karakter szükséges`;
    }
    if (control.errors['maxlength']) {
      return `Maximum ${control.errors['maxlength'].requiredLength} karakter megengedett`;
    }
    if (control.errors['pastDate']) {
      return 'A dátum nem lehet korábbi a mai napnál';
    }
    
    return '';
  }

  onSubmit(): void {
    if (this.jobForm.valid) {
      const formData = {
        ...this.jobForm.value,
        turns: this.turns
      };
      
      console.log('Job submitted:', formData);
      // Itt később implementálhatjuk a backend hívást
      this.router.navigate(['/jobs']);
    } else {
      Object.keys(this.jobForm.controls).forEach(key => {
        const control = this.jobForm.get(key);
        if (control?.invalid) {
          control.markAsTouched();
        }
      });
    }
  }

  addNewTurn(): void {
    if (this.selectedTurn && this.turns.length < 5) {
      const currentCount = this.turnCount[this.selectedTurn] || 0;
      const newTurn = {
        name: `${this.selectedTurn} ${currentCount + 1}`,
        count: currentCount + 1,
        type: this.selectedTurn
      };
      
      this.turnCount[this.selectedTurn] = currentCount + 1;
      this.turns.push(newTurn);
      this.selectedTurn = '';
    }
  }

  editTurn(turn: any): void {
    // Itt implementálhatjuk a kör szerkesztését
    console.log('Editing turn:', turn);
    this.router.navigate(['/edit-turns', turn.name]);
  }

  removeTurn(index: number): void {
    this.turns.splice(index, 1);
  }
}