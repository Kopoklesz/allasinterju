import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoCompany } from '../../commons/dtos/DtoCompany';
import { DtoInvitaion } from '../../commons/dtos/DtoInvitaion';

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

  generateCode(data : DtoInvitaion){
      return this.http.post(`${this.apiUrl}/CreateInvite`,{data},{withCredentials : true});
  }

  updateCompany(id: number, changes: Partial<DtoCompany>) {
    // Csak akkor küldjük el a kérést, ha vannak változtatások
    if (Object.keys(changes).length === 0) {
      return of(null); // Ha nincs változtatás, azonnal visszatérünk
    }
    
    return this.http.put(`${this.apiUrl}/update/${id}`, changes, {
      withCredentials: true
    });
  }

  updateCredentials(id: number, credentials: { 
    currentPassword: string, 
    email?: string, 
    newPassword?: string 
  }) {
    return this.http.put(`${this.apiUrl}/updateCredentials/${id}`, credentials, {
      withCredentials: true
    });
  }
}