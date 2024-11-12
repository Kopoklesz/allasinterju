import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DtoJobShort } from '../../dtos/DtoJobShort';

@Component({
  selector: 'app-job-card',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="job-card">
    <h3>{{ job.companyName }}</h3>
    <p>{{ job.jobPosition }}</p>
    <p><strong>Job at: </strong>{{ job.hortDesc }}</p>
</div>
  `,
  styleUrls: ['./job-card.component.css']
})

export class JobCardComponent {
  @Input() job ?: DtoJobShort;

  constructor(private router: Router) {}

  onCardClick(job: any) {
    this.router.navigate(['/job-application', job.id]);
  }
}
