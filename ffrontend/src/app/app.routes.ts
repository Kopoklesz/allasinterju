import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component'; // Adjust path as needed
import { JobApplicationComponent } from './components/job-application/job-application.component'; // Adjust path as needed
import { ProfileComponent } from './components/profile/profile.component'; // Add this for profile component
import { CProfileComponent } from './components/c-profile/c-profile.component';
import { NewJobComponent } from './components/new-job/new-job.component';

export const routes: Routes = [
  { path: '', component: HomeComponent }, // Homepage route
  { path: 'job-application/:id', component: JobApplicationComponent }, // Job application page with ID parameter
  { path: 'profile/:id', component: ProfileComponent },// Profile page route
  { path: 'c-profile/:id', component: CProfileComponent }, 
  { path: 'new-job', component: NewJobComponent } 
];
