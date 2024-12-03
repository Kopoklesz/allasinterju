import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

const protectedRoutes = ['/dashboard', '/profile/3', '/jobs']; // Define your routes

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((error) => {
      if (error.status === 401) {
        // Extract the relative path from the full URL
        const url = new URL(req.url, window.location.origin); // Use browser's `URL` object
        const relativePath = url.pathname; // Extract the path (e.g., '/profile/3')

        if (protectedRoutes.some(route => relativePath === route)) {
          // Redirect to the homepage
          router.navigate(['/']);
        }
      }
      return throwError(() => error); // Propagate the error
    })
  );
};
