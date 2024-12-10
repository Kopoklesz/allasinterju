import { Component } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobCardComponent } from '../../commons/components/job-card/job-card.component';
import { CommonModule } from '@angular/common';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user/user.service';
import { DtoUser, DtoUserLeetStats } from '../../commons/dtos/DtoUser';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavbarComponent ,JobCardComponent,CommonModule,FormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
  jobs: DtoJobShort[] = [];
  user?: DtoUser;
  leetcodeStats: DtoUserLeetStats | undefined;
  leetCodeUsername: string = '';
  isConnecting: boolean = false;
  connectionMessage: string = '';
  

  constructor(
    private route: ActivatedRoute,
    private userService:  UserService,
    private router: Router
  ) {}
  ngOnInit() {
    const userIdParam = this.route.snapshot.paramMap.get('id'); 
    const userId = userIdParam ? Number(userIdParam) : null; 
    console.log(userId);    
    if (userId !== null) {
      this.userService.getUserData(userId).subscribe(data =>{
        this.user = data;
      });
      this.userService.getAppliedJob(userId).subscribe(data => {
        this.jobs = data;
        console.log(this.jobs);
      });
      if (userId) {
        this.userService.getLeetcodeStats(userId).subscribe(data => {
          this.leetcodeStats = data;
        });
      }
    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  editProfile() {
    if (this.user) {
      this.router.navigate(['/edit-profile', this.user.id]);
    }
  }

  connectLeet() {
    if (!this.leetCodeUsername || this.isConnecting) return;

    this.isConnecting = true;
    this.connectionMessage = '';

    this.userService.connectLeetCode(this.leetCodeUsername).subscribe({
      next: (response) => {
        this.connectionMessage = 'LeetCode account successfully connected!';
        this.leetCodeUsername = '';
        this.loadUserData();
      },
      error: (error) => {
        this.connectionMessage = 'Failed to connect LeetCode account. Please try again.';
        console.error('Error connecting LeetCode account:', error);
        this.isConnecting = false;
      },
      complete: () => {
        this.isConnecting = false;
      }
    });
  }

  loadUserData() {
    if (this.user?.id) {
      this.userService.getLeetcodeStats(this.user.id).subscribe(data => {
        this.leetcodeStats = data;
      });
    }
  }
 }
