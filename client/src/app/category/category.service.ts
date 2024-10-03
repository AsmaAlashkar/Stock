import { Injectable } from '@angular/core';
import { Category } from '../shared/models/category';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

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

  getCtegoryById(id:number){
    return this.http.get<Category>(`${environment.getCategoryById}${id}`, this.httpHeader);
  }

  createCategory(category: Category) :Observable<Category>{
    return this.http.post<Category>(`${environment.createCtegory}`, category, 
      {headers:this.httpHeader.headers, responseType: 'text' as 'json'});
  }

  updateCategory(catId: number, updatedCategory: Partial<Category>): Observable<Category> {
    return this.http.put<Category>(`${environment.updateCategory}${catId}`, updatedCategory, 
      {headers:this.httpHeader.headers, responseType: 'text' as 'json'}
    );
  }

}
