import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {
  constructor(private http: HttpClient) {}
/*
  getJobs(): Observable<any> {
    return this.http.get('your-api-url-here'); // Replace with your actual API URL
  }
   */ 

}