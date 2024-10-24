import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMainWearhouse } from '../shared/models/wearhouse';
import { IViewWearhouseItem } from '../shared/models/IViewWearhouseItem';
import { Observable } from 'rxjs';
import { ISubWearhouse, subWearhouseVM } from '../shared/models/subwearhouse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WearhouseService {

  constructor(private http: HttpClient) { }

  getmainwearhouse() {
    return this.http.get<IMainWearhouse[]>(`${environment.getMainWearhouse}`);
  }

  getmainwearhousebyid(id: number) {
    return this.http.get<IViewWearhouseItem[]>(`${environment.getMainWearhouseById}${id}`);
  }

  getsubWearhouse(): Observable<subWearhouseVM[]> {
    return this.http.get<subWearhouseVM[]>(`${environment.getSubwarehouse}`);
  }

  getSubWearhouseByMainId(mainId: number) {
    return this.http.get<IViewWearhouseItem[]>(`${environment.apiUrl}SubWearhouse/GetSubWearhouseByMainId/${mainId}`);
  }

  getSubWearhouseById(id: number): Observable<IViewWearhouseItem> {
    return this.http.get<IViewWearhouseItem>(`${environment.apiUrl}SubWearhouse/GetSubWearhouseById/${id}`);
  }
  
  // Create a new main warehouse
  createNewMainWearhouse(mainwearhouse: IMainWearhouse): Observable<any> {
    return this.http.post(`${environment.apiUrl}MainWearhouse/CreateNewMainWearhouse`, mainwearhouse, { responseType: 'text' });
  }

  // Update an existing main warehouse
  updateMainWearhouse(id: number, mainwearhouse: IMainWearhouse): Observable<any> {
    return this.http.put(`${environment.apiUrl}MainWearhouse/UpdateMainWearHouse/${id}`, mainwearhouse, { responseType: 'text' });
  }

  // Create a new main warehouse
  createNewSubWearhouse(subwearhouse: ISubWearhouse): Observable<any> {
    return this.http.post(`${environment.apiUrl}SubWearhouse/CreateNewSubWearhouse`, subwearhouse, { responseType: 'text' });
  }

  getSubNamesAndParentIdsByMainFk(mainId: number): Observable<ISubWearhouse[]> {
    return this.http.get<ISubWearhouse[]>(`${environment.apiUrl}SubWearhouse/GetSubWearhouseByMainId/${mainId}`);
  }

  updateSubWearhouse(id: number, subwearhouse: ISubWearhouse): Observable<any> {
    return this.http.put(`${environment.apiUrl}SubWearhouse/UpdateSubWearHouse/${id}`, subwearhouse, { responseType: 'text' });
  }

}
