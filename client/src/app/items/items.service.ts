import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item, ItemDetailsResult } from '../shared/models/items';
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
    })
  };

    getItems(PageSize: number, PageNumber: number, skip: number): Observable <ItemDetailsResult> {
      return this.http.get<ItemDetailsResult>(`${environment.getItems}?PageNumber=${PageNumber}&PageSize=${PageSize}&skip=${skip}`, this.httpHeader);
    }
    getItemsByCategoryId(id:number,PageSize: number, PageNumber: number, skip: number): Observable <ItemDetailsResult> {
      return this.http.get<ItemDetailsResult>(`${environment.getItemsByCategoryId}${id}?PageNumber=${PageNumber}&PageSize=${PageSize}&skip=${skip}`, this.httpHeader);
    }
    getItemsBySubId(id:number,PageSize: number, PageNumber: number, skip: number): Observable <ItemDetailsResult> {
      return this.http.get<ItemDetailsResult>(`${environment.getItemsBySubId}${id}?PageNumber=${PageNumber}&PageSize=${PageSize}&skip=${skip}`, this.httpHeader);
    }
    createItem(item: Item) :Observable<Item>{
        return this.http.post<Item>(`${environment.createItem}`, item, 
          {headers:this.httpHeader.headers, responseType: 'text' as 'json'});
    // return this.http.post(`${environment.createItem}`, this.httpHeader);
    }
}
