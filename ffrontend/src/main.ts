import { bootstrapApplication } from '@angular/platform-browser';
<<<<<<< HEAD
import { AppComponent } from './app/app.component';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { MonacoEditorService } from './app/services/monaco/monaco-editor.service';
=======
import { authInterceptor } from './app/services/auth/HtttpInterceptor';
import { AppComponent } from './app/app.component';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http'; // Ensure this import
import { provideRouter } from '@angular/router'; // Import provideRouter
import { routes } from './app/app.routes'; // Import your routes
>>>>>>> 6ee5ec83fc4af5f4e81feb340a79c9fbce861d7a

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withFetch()),
    provideRouter(routes),
    MonacoEditorService
  ]
}).catch(err => console.error(err));