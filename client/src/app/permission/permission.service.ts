import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPermissionType } from '../shared/models/permissiontype';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor(private http: HttpClient) { }

  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'

    })  };

    getPermissionTypes(){
      
      return this.http.get<IPermissionType[]>(`${environment.getpermissiontype}`, this.httpHeader);

    }
}
