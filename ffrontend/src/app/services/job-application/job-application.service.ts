import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {
  
  private apiUrl = 'https://localhost:7000';

  constructor(private http: HttpClient) {}

  getJobs(): Observable<Array<DtoJobShort>> {
  
    return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/job/getalljobs`);
  }

  getJob(id: number): Observable<DtoJobShort> {
    return this.http.get<DtoJobShort>(`${this.apiUrl}/job/byid/${id}`);
  }
 
}