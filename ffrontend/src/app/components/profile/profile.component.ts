import { Component } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobCardComponent } from '../../commons/components/job-card/job-card.component';
import { CommonModule } from '@angular/common';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user/user.service';

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

  constructor(
    private route: ActivatedRoute,
    private userService:  UserService
  ) {}
 

  ngOnInit() {
    const userIdParam = this.route.snapshot.paramMap.get('id'); 
    const userId = userIdParam ? Number(userIdParam) : null;       
    if (userId !== null) {
      this.userService.getAppliedJobs(userId).subscribe(data => {
        this.jobs = data;
      });
    } else {
      console.error('Job ID is missing or invalid');
    }
  }
 }
