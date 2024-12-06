import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CdkDragDrop, moveItemInArray, DragDropModule } from '@angular/cdk/drag-drop';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { parseJwt } from '../../utils/cookie.utils';

export interface Turn {
  id: number;
  name: string;
  type: string;
  isCompleted: boolean;
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
  selectedTurnType = new FormControl('');
  
  turnTypes = ['Programming', 'Design', 'Algorithms', 'Testing', 'DevOps'];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private jobService: JobApplicationService
  ) {}

  ngOnInit() {
    const jobIdParam = this.route.snapshot.paramMap.get('id');
    if (!jobIdParam) {
      this.router.navigate(['/']);
      return;
    }
    this.jobId = parseInt(jobIdParam, 10);
    
    // Itt lehetne betölteni a már létező köröket ha vannak
    // this.loadExistingTurns();
  }

  addTurn() {
    const selectedType = this.selectedTurnType.value;
    if (selectedType && this.turns.length < 5) {
      const newTurn: Turn = {
        id: Date.now(),
        name: `${selectedType} Round ${this.turns.length + 1}`,
        type: selectedType,
        isCompleted: false
      };
      
      this.turns.push(newTurn);
      this.selectedTurnType.reset();
    }
  }

  onDrop(event: CdkDragDrop<Turn[]>) {
    moveItemInArray(this.turns, event.previousIndex, event.currentIndex);
  }

  editTurn(turn: Turn) {
    if (this.jobId) {
      // Mentsük el az aktuális turns listát localStorage-ba vagy service-be
      localStorage.setItem('currentTurns', JSON.stringify(this.turns));
      this.router.navigate([`/turns/${turn.type.toLowerCase()}`, this.jobId], {
        queryParams: { returnUrl: `/add-rounds/${this.jobId}` }
      });
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