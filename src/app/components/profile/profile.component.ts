import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { JobCardComponent } from '../job-card/job-card.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavbarComponent,JobCardComponent,CommonModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  profile = {
    image: 'https://via.placeholder.com/100',
    name: 'John Doe',
    email: 'john@example.com',
    bio: 'This is a short bio about the user.'
  };

  appliedJobs = [
    { id: 1, company_name: 'Company A', short_desc: 'Job description A', job_position: 'Developer' },
    { id: 2, company_name: 'Company B', short_desc: 'Job description B', job_position: 'Designer' }
  ];
}
