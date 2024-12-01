import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoJob } from '../../commons/dtos/DtoJob';
import { DtoTest } from '../../commons/dtos/DtoTest';
import { DtoJobAdd } from '../../commons/dtos/DtoJob';
import { parseJwt, setCookie } from '../../utils/cookie.utils';
import { HttpHeaders } from '@angular/common/http';
import { DtoKerdoivLetrehozas } from '../../commons/dtos/DtoJob';

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

  applyForJob(jobId: number){  //Jelentkezés a munkára
    return this.http.post(`${this.apiUrl}/job/apply/${jobId}`,{},{withCredentials: true });
  }

  getJobTests(jobId: number): Observable<DtoTest[]> {  //lekéri a munkához tartozó teszteket
    return this.http.get<DtoTest[]>(`${this.apiUrl}/job/tests/${jobId}`);
  }

  addJob(data: DtoJobAdd){
  let token = localStorage.getItem("JWT_TOKEN");
   let role = parseJwt(localStorage.getItem("JWT_TOKEN"))?.role;
   
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

   console.log(headers)
   console.log( parseJwt(localStorage.getItem("JWT_TOKEN")))
    return this.http.post(`${this.apiUrl}/job/addJob`,data, { headers, withCredentials: true });
    }

    addRound(data : DtoKerdoivLetrehozas){
      return this.http.post(`${this.apiUrl}/job/addRound`,data, {withCredentials: true })
    }
}