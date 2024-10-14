import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ItemDetailsResult } from '../shared/models/items';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  constructor(private http: HttpClient) { }

  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'

    })};

    getItems(PageSize: number, PageNumber: number, skip: number): Observable <ItemDetailsResult> {
      return this.http.get<ItemDetailsResult>(`${environment.getItems}?PageNumber=${PageNumber}&PageSize=${PageSize}&skip=${skip}`, this.httpHeader);
    }
}
