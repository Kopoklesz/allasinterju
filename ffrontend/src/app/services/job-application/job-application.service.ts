import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoJob } from '../../commons/dtos/DtoJob';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {
  
  private apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) {}

  getJobs(): Observable<Array<DtoJobShort>> {
    return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/Job/GetAllJobs`);
  }

  getJob(id: number): Observable<DtoJob> {
    return this.http.get<DtoJob>(`${this.apiUrl}/job/byid/${id}`);
  }

  checkApplicationStatus(jobId: number): Observable<boolean> {  //Megadja hogy a munkavállaló már jelentkezett-e erre a munkára
    return this.http.get<boolean>(`${this.apiUrl}/job/application-status/${jobId}`);
  }

  applyForJob(jobId: number): Observable<any> {  //Jelentkezés a munkára
    return this.http.post(`${this.apiUrl}/job/apply/${jobId}`, {});
  }
}