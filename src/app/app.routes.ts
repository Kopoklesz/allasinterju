import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component'; // Adjust path as needed
import { JobApplicationComponent } from './components/job-application/job-application.component'; // Adjust path as needed
import { ProfileComponent } from './components/profile/profile.component'; // Add this for profile component

export const routes: Routes = [
  { path: '', component: HomeComponent }, // Homepage route
  { path: 'job-application/:id', component: JobApplicationComponent }, // Job application page with ID parameter
  { path: 'profile', component: ProfileComponent } // Profile page route
];
