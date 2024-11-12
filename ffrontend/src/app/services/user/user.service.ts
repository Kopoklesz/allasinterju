
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private apiUrl = 'https://localhost:7000';

  constructor(private http: HttpClient) {}

  getAppliedJobs(userId: number): Observable<Array<DtoJobShort>>{
    return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/user/getappliedjobs/${userId}`);
  }
 
}
