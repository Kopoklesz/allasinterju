import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DtoJobShort } from '../../dtos/DtoJobShort';
@Component({
  selector: 'app-job-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './job-card.html',
  styleUrls: ['./job-card.component.css']
})

export class JobCardComponent {
  @Input() job ?: DtoJobShort;

  

  constructor(private router: Router) {

  }

  ngOnInit(){    
      
  }
  onCardClick(job: any) {
    if (job) {
      this.router.navigate(['/job-application', job.id]);
    }
  }
}
