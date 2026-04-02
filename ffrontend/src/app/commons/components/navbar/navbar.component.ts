import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { SignInComponent } from '../../../components/sign-in/sign-in.component';
import { AuthService } from '../../../services/auth/auth.service';
import { parseJwt } from '../../../utils/cookie.utils';
import * as Cookies from 'js-cookie';

export interface JwtPayload {
  unique_name: string;
  id: string;
  role: string;
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
}

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, SignInComponent],
  template: `
  <nav class="navbar">
    <button class="back-button" *ngIf="!isHomePage" (click)="goBack()">
      <span class="back-arrow">←</span> Back
    </button>
    <div class="navbar-title" (click)="goHome()">{{ title }}</div>
    <div class="navbar-buttons">
      <ng-container *ngIf="isLoggedIn(); else loginButton">
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
    private location: Location,
    private activatedRoute: ActivatedRoute,
    public authService: AuthService
  ) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isHomePage = this.router.url === '/';
      });
  }

  goHome() {
    this.router.navigate(['/']);
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        localStorage.removeItem("JWT_TOKEN");
        Cookies.default.remove("JWT_TOKEN");
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Logout error:', error);
        localStorage.removeItem("JWT_TOKEN");
        Cookies.default.remove("JWT_TOKEN");
        this.router.navigate(['/']);
      }
    });
  }

  goBack() {
    this.location.back();
  }

  isLoggedIn(){
    return Cookies.default.get("JWT_TOKEN");
  }

  goToProfile() {

        const token = localStorage.getItem("JWT_TOKEN");

        if (token) {
          const decodedToken = parseJwt(token);
           if(decodedToken?.role=='Ceg')
           {
            this.router.navigate(['/c-profile', decodedToken?.id]);
           }
           if(decodedToken?.role=='Munkakereso'){
          this.router.navigate(['/profile', decodedToken?.id]);
           }
        
      }
       
  }
}