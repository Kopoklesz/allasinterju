import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { DtoJob } from '../../commons/dtos/DtoJob';
import { DtoTest } from '../../commons/dtos/DtoTest';
import { DtoJobAdd } from '../../commons/dtos/DtoJob';
import { HttpHeaders } from '@angular/common/http';
import { DtoKerdoivLetrehozas } from '../../commons/dtos/DtoJob';
import { BAlgorithmAdd } from '../../commons/dtos/DtoAlgorithmAdd';
import * as Cookies from 'js-cookie';
import { BDesignAdd } from '../../commons/dtos/DtoDesignAdd';
import { BDevOpsAdd } from '../../commons/dtos/DtoDevOpsAdd';
import { DtoRounds } from '../../commons/dtos/DtoRounds';
import { BBProgrammingAdd } from '../../commons/dtos/DtoProgrammingAdd';

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

  addJob(data: DtoJobAdd): Observable<number> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<number>(`${this.apiUrl}/job/addJob`, data, 
      { headers, withCredentials: true });
  }

    addRound(data : DtoKerdoivLetrehozas){
      return this.http.post(`${this.apiUrl}/Algorithm/add`,data, {withCredentials: true })
    }
    addAlgorithm(data : BAlgorithmAdd){
      const token = Cookies.default.get("JWT_TOKEN"); // Get the JWT token from cookies
  const headers = {
    Authorization: `Bearer ${token}`, // Attach the token
  };
  return this.http.post(`${this.apiUrl}/Algorithm/add`, data, {
    headers: headers,
    withCredentials: true,
  });
    }
    
    addDesign(data : BDesignAdd){
 
  return this.http.post(`${this.apiUrl}/Design/add`, data, {
    withCredentials: true,
  });
  }

  addDevOps(data : BDevOpsAdd){
    return this.http.post(`${this.apiUrl}/DevOps/add`, data, {
      withCredentials: true,
    });
  }

  updateJob(id: number, jobData: DtoJobAdd): Observable<any> {
    return this.http.put(`${this.apiUrl}/job/update/${id}`, jobData, {
      withCredentials: true
    });
  }

  getSubmittedApplications(jobId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/job/getallapplications/${jobId}`, {
      withCredentials: true
    });
  }

  getRounds(jobId: number): Observable<DtoRounds[]> {
    return this.http.get<DtoRounds[]>(`${this.apiUrl}/job/getrounds/${jobId}`);
  }

  addProgramming(data : BBProgrammingAdd){
    return this.http.post(`${this.apiUrl}/Programming/add`,data, {
      withCredentials: true,
    });
  }
}