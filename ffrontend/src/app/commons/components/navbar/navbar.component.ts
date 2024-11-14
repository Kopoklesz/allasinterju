import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators'; // Import filter for routing events
import { SignInComponent } from '../../../components/sign-in/sign-in.component'; 

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,SignInComponent],
  template: `
    <nav class="navbar">
      <button class="back-button" *ngIf="!isHomePage" (click)="goBack()">←</button>
      <div class="navbar-title">{{ title }}</div>
      <div class="navbar-buttons">
        <button class="logout-button" (click)="signIn.showPopup()">Logout</button>
        <app-sign-in #signIn></app-sign-in>
        <button class="profile-button" (click)="goToProfile()">👤</button>
      </div>
    </nav>
  `,
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  @Input() title: string = '';
  isHomePage: boolean = false; // Property to check if on the homepage

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isHomePage = this.router.url === '/'; // Check if current URL is the homepage
      });
  }

  goBack() {
    this.router.navigate(['..']);
  }

  goToProfile() {
    const userIdParam = this.activatedRoute.snapshot.paramMap.get('id'); 
    const userId = userIdParam ? Number(userIdParam) : null;    
    this.router.navigate(['/profile', 1/*userId*/]); // Navigate to the profile page
  }
}
