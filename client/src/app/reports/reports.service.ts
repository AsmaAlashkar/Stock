import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reports } from '../shared/models/reports';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(private http: HttpClient) { }

  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'
    })
  };

  getAllItemsQuantitiesInAllSubsReports(): Observable <Reports[]> {
    return this.http.get<Reports[]>(`${environment.getAllItemsQuantitiesInAllSubsReports}`, this.httpHeader);
  }

  getAllItemsQuantitiesBySubIdReports(subId: number): Observable <Reports[]> {
    return this.http.get<Reports[]>(`${environment.getAllItemsQuantitiesBySubIdReports}?subId=${subId}`, this.httpHeader);
  }

}
