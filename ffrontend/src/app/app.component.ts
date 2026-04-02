import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router'; // Import RouterOutlet
import { SignInComponent } from './components/sign-in/sign-in.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,SignInComponent], // Import RouterOutlet to enable routing
  template: `
    <nav class="navbar">
      <!-- Navbar will remain empty for now -->
    </nav>
    
    <router-outlet></router-outlet> <!-- This is where routed components will be displayed -->
  `,
  styleUrls: ['./app.component.css']

})
export class AppComponent {}