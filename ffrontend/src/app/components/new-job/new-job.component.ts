import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule, FormBuilder, FormGroup, Validators, ReactiveFormsModule, ValidationErrors, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { CdkDragDrop, moveItemInArray, DragDropModule } from '@angular/cdk/drag-drop';
import { DtoJobAdd } from '../../commons/dtos/DtoJob';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { response } from 'express';
import { error } from 'console';

interface Turn {
  id: number;
  name: string;
  type: string;
  count: number;
}

@Component({
  selector: 'app-new-job',
  standalone: true,
  imports: [
    NavbarComponent,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    DragDropModule
  ],
  templateUrl: './new-job.component.html',
  styleUrls: ['./new-job.component.css']
})



export class NewJobComponent implements OnInit {
  
  jobForm!: FormGroup;
  selectedTurn: string = '';
  turns: Turn[] = [];
  turnTypes: string[] = ['Programming', 'Design', 'Algorithms', 'Testing', 'DevOps'];
  
  workScheduleOptions = [
    { value: 'full', label: 'Full Time' },
    { value: 'part', label: 'Part Time' },
    { value: 'remote', label: 'Remote Work' },
    { value: 'hybrid', label: 'Hybrid' }
  ];

  isSubmitting = false;
  isLoadingTurns = false;
  isLoadingForm = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private jobService : JobApplicationService
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.isLoadingForm = false;
    }, 1000);
    
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
    this.isLoadingTurns = true;
    setTimeout(() => {
      // Itt később implementálhatjuk a mentett körök betöltését
      console.log('Loading saved turns...');
      this.isLoadingTurns = false;
    }, 1500);
  }

  showError(controlName: string): boolean {
    const control = this.jobForm.get(controlName);
    return control ? control.invalid && (control.dirty || control.touched) : false;
  }

  getErrorMessage(controlName: string): string {
    const control = this.jobForm.get(controlName);
    if (!control?.errors) return '';

    if (control.errors['required']) {
      return 'This field is required';
    }
    if (control.errors['minlength']) {
      return `Min ${control.errors['minlength'].requiredLength} caracter needed`;
    }
    if (control.errors['maxlength']) {
      return `Max ${control.errors['maxlength'].requiredLength} caracter lenght`;
    }
    if (control.errors['pastDate']) {
      return 'The date cant bee earlier than today';
    }
    
    return '';
  }

  onSubmit(): void {
    if (this.jobForm.valid) {
      this.isSubmitting = true;
      const formData = {
        ...this.jobForm.value,
        turns: this.turns
      };
      
      // backend hívás
        let newJob : DtoJobAdd = {
          jobTitle: this.jobForm.get('cim')?.value,
          jobType: 'string',
          workOrder: this.jobForm.get('munkarend')?.value,
          description: this.jobForm.get('leiras')?.value,
          shortDescription: this.jobForm.get('rovidleiras')?.value,
          location: this.jobForm.get('telepely')?.value,
          deadline: this.jobForm.get('kitoltesihatarido')?.value,


        }

        this.jobService.addJob(newJob).subscribe({
          next: (response) =>{

          },
          error: (error) => {
              console.log(error)
          }

        });
        console.log('Job submitted:', formData);
        this.isSubmitting = false;
     //   this.router.navigate(['/jobs']);
     
    } else {
      Object.keys(this.jobForm.controls).forEach(key => {
        const control = this.jobForm.get(key);
        if (control?.invalid) {
          control.markAsTouched();
        }
      });
    }
  }

  getFormattedDate(): string {
    const today = new Date();
    return today.toISOString().split('T')[0];
  }

  addNewTurn(): void {
    if (this.selectedTurn && this.turns.length < 10) { 
      this.isLoadingTurns = true;
      setTimeout(() => {
        const existingTurns = this.turns.filter(t => 
          t.type.toLowerCase() === this.selectedTurn.toLowerCase()
        );
        
        const usedNumbers = existingTurns.map(turn => {
          const numberMatch = turn.name.match(/\d+$/);
          return numberMatch ? parseInt(numberMatch[0]) : 0;
        }).sort((a, b) => a - b);

        let nextNumber = 1;
        for (const num of usedNumbers) {
          if (num !== nextNumber) {
            break;
          }
          nextNumber++;
        }

        const newTurn: Turn = {
          id: Date.now(),
          name: `${this.selectedTurn} ${nextNumber}`,
          type: this.selectedTurn.toLowerCase(),
          count: nextNumber
        };
        
        this.turns.push(newTurn);
        this.selectedTurn = '';
        this.isLoadingTurns = false;
      }, 800);
    }
  }

  editTurn(turn: Turn): void {
    if (turn) {
      const turnType = turn.type.toLowerCase();
      this.router.navigate([`/turns/${turnType}`, turn.id], {
        queryParams: {
          name: turn.name
        }
      });
    }
  }

  removeTurn(index: number): void {
    this.isLoadingTurns = true;
    setTimeout(() => {
      this.turns.splice(index, 1);
      this.isLoadingTurns = false;
    }, 500);
  }

  getRemainingTurns(): number {
    return 5 - this.turns.length;
  }

  onDrop(event: CdkDragDrop<Turn[]>) {
    moveItemInArray(this.turns, event.previousIndex, event.currentIndex);
  }
}