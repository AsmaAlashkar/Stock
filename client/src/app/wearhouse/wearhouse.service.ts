import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { IMainWearhouse } from '../shared/models/wearhouse';

@Injectable({
  providedIn: 'root'
})
export class WearhouseService {
  baseUrl = 'http://localhost:5050/api/'

  constructor(private http: HttpClient) { }

  getmainwearhouse(){
    return this.http.get<IMainWearhouse[]>(this.baseUrl+'MainWearhouse/GetMainWearhouse');
  }

  
}
