import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { IMainWearhouse } from '../shared/models/wearhouse';
import { IViewWearhouseItem } from '../shared/models/IViewWearhouseItem';
import { Observable } from 'rxjs';

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
  
  // Create a new main warehouse
  createMainWearhouse(mainwearhouse: IMainWearhouse): Observable<any> {
    return this.http.post(this.baseUrl + 'MainWearhouse/CreateNewMainWearhouse', mainwearhouse);
  }

  // Update an existing main warehouse
  updateMainWearhouse(id: number, mainwearhouse: IMainWearhouse): Observable<any> {
  return this.http.put(this.baseUrl + 'MainWearhouse/UpdateMainWearHouse/' + id, mainwearhouse, { responseType: 'text' });
}


}
