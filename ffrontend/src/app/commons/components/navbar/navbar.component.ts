import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { SignInComponent } from '../../../components/sign-in/sign-in.component';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, SignInComponent],
  template: `
    <nav class="navbar">
      <button class="back-button" *ngIf="!isHomePage" (click)="goBack()">
        <span>←</span> Back
      </button>
      <div class="navbar-title">{{ title }}</div>
      <div class="navbar-buttons">
        <ng-container *ngIf="(authService.isLoggedIn$ | async); else loginButton">
          <button class="logout-button" (click)="logout()">
            Sign Out
          </button>
          <button class="profile-button" (click)="goToProfile()">
            👤
          </button>
        </ng-container>
        <ng-template #loginButton>
          <button class="logout-button" (click)="signIn.showPopup()">
            Sign In
          </button>
          <app-sign-in #signIn></app-sign-in>
        </ng-template>
      </div>
    </nav>
  `,
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  @Input() title: string = '';
  isHomePage: boolean = false;

  constructor(
    private router: Router, 
    private activatedRoute: ActivatedRoute,
    public authService: AuthService
  ) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isHomePage = this.router.url === '/';
      });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  goBack() {
    this.router.navigate(['..']);
  }

  goToProfile() {
    const userIdParam = this.activatedRoute.snapshot.paramMap.get('id'); 
    const userId = userIdParam ? Number(userIdParam) : null;    
    this.router.navigate(['/profile', 1]);
  }
}