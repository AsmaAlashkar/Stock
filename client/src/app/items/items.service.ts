import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ItemDetailsDtoVM, ItemDetailsResult, Item, ItemDetailsPerTab } from '../shared/models/items';
import { Observable, skip } from 'rxjs';

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

    getItemsVM(): Observable <ItemDetailsDtoVM[]> {
      return this.http.get<ItemDetailsDtoVM[]>(`${environment.getItemsVM}`, this.httpHeader);
    }

    getItemsByCategoryId(id:number,PageSize: number, PageNumber: number, skip: number): Observable <ItemDetailsResult> {
      return this.http.get<ItemDetailsResult>(`${environment.getItemsByCategoryId}${id}?PageNumber=${PageNumber}&PageSize=${PageSize}`, this.httpHeader);
    }

    getItemsBySubId(id:number,PageSize: number, PageNumber: number, skip: number): Observable <ItemDetailsResult> {
      return this.http.get<ItemDetailsResult>(`${environment.getItemsBySubId}${id}?PageNumber=${PageNumber}&PageSize=${PageSize}&skip=${skip}`, this.httpHeader);
    }

    getItemsBySubIdVM(id: number): Observable <ItemDetailsDtoVM[]> {
      return this.http.get<ItemDetailsDtoVM[]>(`${environment.getItemsBySubIdVM}${id}`, this.httpHeader);
    }

    getItemsBySubIdItemId(subId: number, itemId: number): Observable <ItemDetailsPerTab> {
      // console.log(`${environment.getItemsBySubIdVM}${subId}/${itemId}`)
      return this.http.get<ItemDetailsPerTab>(`${environment.getItemsBySubIdItemId}${subId}/${itemId}`, this.httpHeader);

    }

    createItem(item: Item) :Observable<Item>{
        return this.http.post<Item>(`${environment.createItem}`, item,
          {headers:this.httpHeader.headers, responseType: 'text' as 'json'});
    // return this.http.post(`${environment.createItem}`, this.httpHeader);
    }
}
