import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { IMainWearhouse } from '../shared/models/wearhouse';
import { IViewWearhouseItem } from '../shared/models/IViewWearhouseItem';
import { Observable } from 'rxjs';
import { ISubWearhouse } from '../shared/models/subwearhouse';

@Injectable({
  providedIn: 'root'
})
export class WearhouseService {
  baseUrl = 'http://localhost:5050/api/'

  constructor(private http: HttpClient) { }

  getmainwearhouse(){
    return this.http.get<IMainWearhouse[]>(this.baseUrl+'MainWearhouse/GetMainWearhouse');
  }
  getmainwearhousebyid(id:number){
    return this.http.get<IViewWearhouseItem[]>(this.baseUrl + 'MainWearhouse/GetMainWearhouseById/' + id)
  }
 
  getSubWearhouseByMainId(mainId: number) {
    return this.http.get<IViewWearhouseItem[]>(this.baseUrl + 'SubWearhouse/GetSubWearhouseByMainId/' + mainId);
  }
  getSubWearhouseById(id: number): Observable<IViewWearhouseItem> {
    return this.http.get<IViewWearhouseItem>(this.baseUrl + 'SubWearhouse/GetSubWearhouseById/' + id);
  }
 // Create a new main warehouse
 createNewMainWearhouse(mainwearhouse: IMainWearhouse): Observable<any> {
  return this.http.post(this.baseUrl + 'MainWearhouse/CreateNewMainWearhouse', mainwearhouse, { responseType: 'text' });
}

  // Update an existing main warehouse
  updateMainWearhouse(id: number, mainwearhouse: IMainWearhouse): Observable<any> {
  return this.http.put(this.baseUrl + 'MainWearhouse/UpdateMainWearHouse/' + id, mainwearhouse, { responseType: 'text' });
}

updateSubWearhouse(id: number, subwearhouse: ISubWearhouse): Observable<any> {
  return this.http.put(this.baseUrl + 'SubWearhouse/UpdateSubWearHouse/' + id, subwearhouse, { responseType: 'text' });
}
}
