import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import { provideHttpClient, withFetch } from '@angular/common/http'; // Ensure this import
import { provideRouter } from '@angular/router'; // Import provideRouter
import { routes } from './app/app.routes'; // Import your routes

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withFetch()), // Set up HttpClient globally
    provideRouter(routes) // Set up the router globally
  ]
 
}).catch(err => console.error(err))