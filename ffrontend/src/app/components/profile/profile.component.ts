import { Component } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobCardComponent } from '../../commons/components/job-card/job-card.component';
import { CommonModule } from '@angular/common';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavbarComponent,JobCardComponent,CommonModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
  profile = {
    image: 'https://via.placeholder.com/100',
    name: 'John Doe',
    email: 'john@example.com',
    bio: 'This is a short bio about the user.'
  };
  jobs: DtoJobShort[] = [];


  jobsteszt: DtoJobShort[] = [
    { Id: 1, JobTitle: 'Software Developer', JobType: 'Full-Time', CompanyName: 'Tech Co', City: 'New York' },
    { Id: 2, JobTitle: 'Graphic Designer', JobType: 'Part-Time', CompanyName: 'Creative Inc', City: 'San Francisco' },
    { Id: 3, JobTitle: 'Project Manager', JobType: 'Contract', CompanyName: 'Business Solutions', City: 'Chicago' },
    { Id: 4, JobTitle: 'Data Analyst', JobType: 'Full-Time', CompanyName: 'Analytics Corp', City: 'Los Angeles' },
    { Id: 5, JobTitle: 'Web Developer', JobType: 'Freelance', CompanyName: 'Web Works', City: 'Austin' }
  ];
 }
