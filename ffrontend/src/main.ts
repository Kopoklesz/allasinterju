import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { HttpInterceptor } from '@angular/common/http'; 
import { JwtInterceptor } from './app/services/auth/HtttpInterceptor';
import { routes } from './app/app.routes';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { authInterceptor } from './app/services/auth/authInterceptor';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withFetch(),withInterceptors([authInterceptor])),
    provideRouter(routes),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      useFactory: () => authInterceptor,
      multi: true,
    },
  ]
}).catch(err => console.error(err));