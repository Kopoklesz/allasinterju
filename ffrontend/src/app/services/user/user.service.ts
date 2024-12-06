import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoCompany } from '../../commons/dtos/DtoCompany';
import { DtoUser } from '../../commons/dtos/DtoUser';

@Injectable({
  providedIn: 'root',
})
export class UserService {
 
  private apiUrl = 'http://localhost:5000/user';


  constructor(private http: HttpClient) {}

  getUserRole(): 'normal' | 'company' {
    
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user.role; 
  }


  getAppliedJob(id: number): Observable<Array<DtoJobShort>> {
    return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/getAppliedJobs/${id}`);
  }

 // getAdvertisedJob(id: number): Observable<Array<DtoJobShort>> {
 //   return this.http.get<Array<DtoJobShort>>(`${this.apiUrl}/byid/${id}`);
 // }

  getUserData(id: number): Observable<DtoUser>{
    return this.http.get<DtoUser>(`${this.apiUrl}/byid/${id}`,{withCredentials: true });
  }

  updateUser(id: number, changes: Partial<DtoUser>): Observable<DtoUser> {
    if (Object.keys(changes).length === 0) {
      return of({} as DtoUser);
    }
    
    return this.http.put<DtoUser>(`${this.apiUrl}/update/${id}`, changes, {
      withCredentials: true
    });
  }

  updateCredentials(id: number, credentials: {
    currentPassword: string,
    email?: string,
    newPassword?: string
  }): Observable<any> {
    return this.http.put(`${this.apiUrl}/updateCredentials/${id}`, credentials, {
      withCredentials: true
    });
  }
}