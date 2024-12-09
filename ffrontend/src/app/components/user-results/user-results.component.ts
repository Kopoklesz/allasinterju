import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';

interface Round {
  id: number;
  name: string;
  percentage: number;
}

@Component({
  selector: 'app-user-results',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './user-results.component.html',
  styleUrls: ['./user-results.component.css']
})
export class UserResultsComponent implements OnInit {
  rounds: Round[] = [];
  selectedRound: Round | null = null;
  jobId: number | null = null;
  userId: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private jobService: JobApplicationService
  ) {}

  ngOnInit() {
    this.userId = Number(this.route.snapshot.paramMap.get('userId'));
    this.jobId = Number(this.route.snapshot.queryParamMap.get('jobId'));

    if (this.jobId) {
      this.loadRounds();
    }
  }

  loadRounds() {
    if (this.jobId) {
      this.jobService.getRounds(this.jobId).subscribe({
        next: (rounds) => {
          this.rounds = rounds.map(round => ({
            id: round.id,
            name: round.name,
            percentage: round.percentage ?? 0
          }));
          if (this.rounds.length > 0) {
            this.selectRound(this.rounds[0]);
          }
        },
        error: (error) => {
          console.error('Error loading rounds:', error);
        }
      });
    }
  }

  selectRound(round: Round) {
    this.selectedRound = round;
    // Itt majd meghívjuk a round tartalom betöltő metódust
  }

  getPercentageColor(percentage: number): string {
    if (percentage >= 80) return '#2ecc71';
    if (percentage >= 60) return '#f1c40f';
    return '#e74c3c';
  }

  requestAIEvaluation() {
    // AI értékelés implementációja
  }

  requestManualEvaluation() {
    // Manuális értékelés implementációja
  }
}