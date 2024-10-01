import { Injectable } from '@angular/core';
import { Category } from '../shared/models/category';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
 

  constructor(private http: HttpClient) { }
  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'

    })  };
  getCtegories(){
    return this.http.get<Category[]>(`${environment.getCtegories}`, this.httpHeader);
  }
}
