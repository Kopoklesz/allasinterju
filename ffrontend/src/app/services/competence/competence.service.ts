import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BCompetence } from '../../commons/dtos/DtoCompetence';

@Injectable({
  providedIn: 'root'
})
export class CompetenceService {
  
  private apiUrl = 'http://localhost:5000/Competence';

  constructor(private http: HttpClient) {}

  addToUser(data : BCompetence){
    return this.http.post(`${this.apiUrl}/addToUser`, data, 
        {withCredentials: true });
  }
  getForUser():Observable<BCompetence[]>{
    return this.http.get<BCompetence[]>(`${this.apiUrl}/GetForUser`,{withCredentials: true});
  }
}