import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { JobApplicationComponent } from './components/job-application/job-application.component';
import { ProfileComponent } from './components/profile/profile.component';
import { CProfileComponent } from './components/c-profile/c-profile.component';
import { NewJobComponent } from './components/new-job/new-job.component';
import { EditTurnComponent } from './components/edit-turn/edit-turn.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'job-application/:id', component: JobApplicationComponent },
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'c-profile/:id', component: CProfileComponent },
  { path: 'new-job', component: NewJobComponent } , //ide kell a felhasználó id pluszba
  { path: 'edit-turn/:name', component: EditTurnComponent } //Ide késöbb kell a felhasználó id pluszba
];
