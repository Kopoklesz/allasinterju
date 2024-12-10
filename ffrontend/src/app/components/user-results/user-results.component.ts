import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { DtoRound } from '../../commons/dtos/DtoRound';
import { FormsModule } from '@angular/forms';
import { DtoGetGrade } from '../../commons/dtos/DtoSubmissions';

@Component({
  selector: 'app-user-results',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FormsModule],
  templateUrl: './user-results.component.html',
  styleUrls: ['./user-results.component.css']
})
export class UserResultsComponent implements OnInit {
  rounds: DtoRound[] = [];
  selectedRound: DtoRound | null = null;
  jobId: number | null = null;
  userId: number | null = null;
  aiScore: number | null = null;
  showAIScore: boolean = false;
  showManualEvaluation = false;
  manualScore: number | null = null;
  manualScoreError = false;

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
          this.rounds = rounds;
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

  selectRound(round: DtoRound) {
    this.selectedRound = round;
    if(this.userId && this.jobId) {
      this.jobService.viewallsolve(this.userId, this.jobId).subscribe({
        next: (response) => {
          console.log('Response:', response);
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
  }

  getPercentageColor(percentage: number): string {
    if (percentage >= 80) return '#2ecc71';
    if (percentage >= 60) return '#f1c40f';
    return '#e74c3c';
  }

  requestAIEvaluation() {
    if (!this.selectedRound) {
      return;
    }
  
    if (this.jobId) {
      this.jobService.evaluateRoundAI(this.selectedRound.kerdoivId, 1, "ide bármi").subscribe({
        next: (response) => {
          console.log('AI Evaluation successful:', response);
          if (response.MIszazalek !== undefined) {
            this.aiScore = response.MIszazalek;
            this.showAIScore = true;
          }
          this.loadRounds();
        },
        error: (error) => {
          console.error('Error during AI evaluation:', error);
        }
      });
    }
  }

  acceptAIEvaluation() {
    if(this.selectedRound?.kerdoivId && this.aiScore && this.userId){
      this.jobService.giveGrade(this.userId, this.selectedRound?.kerdoivId, this.aiScore).subscribe({
        next: (response) => {
          console.log('Manual evaluation successful:', response);
          this.loadRounds();
        },
        error: (error) => {
          console.error('Error during manual evaluation:', error);
        }
      });
    }

    this.showAIScore = false;
    this.aiScore = null;
  }

  rejectAIEvaluation() {
    this.showAIScore = false;
    this.aiScore = null;
  }

  requestManualEvaluation() {
    this.showManualEvaluation = true;
    this.manualScore = null;
    this.manualScoreError = false;
  }

  submitManualEvaluation() {
    if (this.manualScore === null || this.manualScore < 0 || this.manualScore > 100) {
      this.manualScoreError = true;
      return;
    }

    if(this.selectedRound?.kerdoivId && this.userId){
      this.jobService.giveGrade(this.userId, this.selectedRound?.kerdoivId, this.manualScore).subscribe({
        next: (response) => {
          console.log('Manual evaluation successful:', response);
          this.loadRounds();
        },
        error: (error) => {
          console.error('Error during manual evaluation:', error);
        }
      });
    }
    
    this.showManualEvaluation = false;
    this.manualScoreError = false;
    this.manualScore = null;
  }

  cancelManualEvaluation() {
    this.showManualEvaluation = false;
    this.manualScoreError = false;
    this.manualScore = null;
  }

  onManualScoreChange(event: any) {
    const value = Number(event.target.value);
    this.manualScore = value;
    this.manualScoreError = value < 0 || value > 100;
  }

  submitLastPercentage(): void {
    if (this.userId && this.jobId) {
      this.jobService.getGrade(this.userId, this.jobId).subscribe({
        next: (response: DtoGetGrade) => {
          if (response.grade !== null) {
            this.jobService.giveFinalGRade(this.userId!, this.jobId!, response.grade).subscribe({
              next: (response) => {
                console.log('Final grade submitted successfully:', response);
              },
              error: (error) => {
                console.error('Error submitting final grade:', error);
              }
            });
          }
        },
        error: (error) => {
          console.error('Error getting grade:', error);
        }
      });
    }
  }
  
  calculateAveragePercentage(): void {
    if (this.userId && this.jobId) {
      this.jobService.getGrade(this.userId, this.jobId).subscribe({
        next: (response: DtoGetGrade) => {
          if (response.grade !== null) {
            console.log('Grade:', response.grade);
            return response.grade;
          } else {
            console.log('No grade available.');
            return 0;
          }
        },
        error: (error: any) => {
          console.error('Error getting grade:', error);
          return 0;
        }
      });
    }
  }
}