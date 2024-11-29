import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoCompany } from '../../commons/dtos/DtoCompany';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
 
  private apiUrl = 'http://localhost:5000/company';


  constructor(private http: HttpClient) {}

  getUserRole(): 'normal' | 'company' {
    
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user.role; 
  }


  getAdvertisedJob(id: number): Observable<Array<DtoJobShort>> {
    return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/getAdvertisedJobs/${id}`);
  }

 // getAdvertisedJob(id: number): Observable<Array<DtoJobShort>> {
 //   return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/byid/${id}`);
 // }

  getUserData(id: number): Observable<DtoCompany>{
    return this.http.get<DtoCompany>(`${this.apiUrl}/byid/${id}`);
  }
}