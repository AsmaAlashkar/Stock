import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ItemDetailsResult } from '../shared/models/items';

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'
    })
  };

  getItemsByKeyword(keyword: string): Observable<ItemDetailsResult> {
    return this.http.get<ItemDetailsResult>(`${environment.getItemsByKeyword}?keyword=${encodeURIComponent(keyword)}`, this.httpHeader);
  }
}
