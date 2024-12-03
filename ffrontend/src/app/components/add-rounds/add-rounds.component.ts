import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CdkDragDrop, moveItemInArray, DragDropModule } from '@angular/cdk/drag-drop';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { parseJwt } from '../../utils/cookie.utils';

export interface Turn {
  id: number;
  name: string;
  type: string;
}

@Component({
  selector: 'app-add-rounds',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, DragDropModule, NavbarComponent],
  templateUrl: './add-rounds.component.html',
  styleUrls: ['./add-rounds.component.css']
})
export class AddRoundsComponent implements OnInit {
  jobId: number | null = null;
  turns: Turn[] = [];
  turnForm: FormGroup;

  turnTypes = ['Programming', 'Design', 'Algorithms', 'Testing', 'DevOps'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private jobService: JobApplicationService
  ) {
    this.turnForm = this.fb.group({
      selectedTurn: ['', Validators.required]
    });
  }

  ngOnInit() {
    const jobIdParam = this.route.snapshot.paramMap.get('id');
    if (!jobIdParam) {
      this.router.navigate(['/']);
      return;
    }
    this.jobId = parseInt(jobIdParam, 10);
  }

  addTurn() {
    if (this.turnForm.valid && this.turns.length < 5 && this.jobId) {
      const selectedType = this.turnForm.get('selectedTurn')?.value;
      console.log('Selected turn type:', selectedType);
      
      if (selectedType) {
        
        const newTurn: Turn = {
          id: Date.now(),
          name: `${selectedType} Round ${this.turns.length + 1}`,
          type: selectedType
        };
        
        this.turns.push(newTurn);
        console.log('Current turns:', this.turns);

        const turnRoute = selectedType.toLowerCase();
        console.log('Navigating to:', `/turns/${turnRoute}/${this.jobId}`); 
        this.router.navigate([`/turns/${turnRoute}`, this.jobId]);
      }
    } else {
      console.log('Form validation failed:', { 
        isFormValid: this.turnForm.valid,
        turnsLength: this.turns.length,
        jobId: this.jobId
      });
    }
  }

  onDrop(event: CdkDragDrop<Turn[]>) {
    moveItemInArray(this.turns, event.previousIndex, event.currentIndex);
  }

  editTurn(turn: Turn) {
    if (this.jobId) {
      this.router.navigate([`/turns/${turn.type.toLowerCase()}`, this.jobId]);
    }
  }

  removeTurn(turn: Turn) {
    this.turns = this.turns.filter(t => t.id !== turn.id);
  }

  cancel() {
    const token = localStorage.getItem("JWT_TOKEN");
    if (token) {
      const decodedToken = parseJwt(token);
      if (decodedToken?.id) {
        this.router.navigate(['/c-profile', decodedToken.id]);
      }
    }
  }

  finish() {
    if (this.turns.length > 0) {
      const token = localStorage.getItem("JWT_TOKEN");
      if (token) {
        const decodedToken = parseJwt(token);
        if (decodedToken?.id) {
          this.router.navigate(['/add-rounds', this.jobId]);
        }
      }
    }
  }
}