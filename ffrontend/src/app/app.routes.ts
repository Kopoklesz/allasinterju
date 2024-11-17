import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { JobApplicationComponent } from './components/job-application/job-application.component';
import { ProfileComponent } from './components/profile/profile.component';
import { CProfileComponent } from './components/c-profile/c-profile.component';
import { NewJobComponent } from './components/new-job/new-job.component';
import { EditTurnComponent } from './components/edit-turn/edit-turn.component';
import { ProgrammingTurnComponent } from './components/turns/programming-turn/programming-turn.component';
import { DesignTurnComponent } from './components/turns/design-turn/design-turn.component';
import { AlgorithmsTurnComponent } from './components/turns/algorithms-turn/algorithms-turn.component';
import { TestingTurnComponent } from './components/turns/testing-turn/testing-turn.component';
import { DevOpsTurnComponent } from './components/turns/devops-turn/devops-turn.component';
import { JobTestsComponent } from './components/job-tests/job-tests.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'job-application/:id', component: JobApplicationComponent },
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'c-profile/:id', component: CProfileComponent },
  { path: 'new-job', component: NewJobComponent } , //ide kell a felhasználó id pluszba
  { path: 'edit-turn/:id', component: EditTurnComponent }, //Ide késöbb kell a felhasználó id pluszba
  { path: 'turns/programming/:id', component: ProgrammingTurnComponent },
  { path: 'turns/design/:id', component: DesignTurnComponent },
  { path: 'turns/algorithms/:id', component: AlgorithmsTurnComponent },
  { path: 'turns/testing/:id', component: TestingTurnComponent },
  { path: 'turns/devops/:id', component: DevOpsTurnComponent },
  { path: 'job-tests/:id', component: JobTestsComponent }
];
