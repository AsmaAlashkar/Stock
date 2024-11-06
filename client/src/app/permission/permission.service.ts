import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPermissionType } from '../shared/models/permissiontype';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { DisplayAllPermission, DisplayAllPermissionVM, Permissionaction } from '../shared/models/permissionaction';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor(private http: HttpClient) { }

  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'
    })
  };

  getPermissionTypes() {
    return this.http.get<IPermissionType[]>(`${environment.getpermissiontype}`, this.httpHeader);
  }

  permissionAction(permAction: Permissionaction): Observable <Permissionaction> {
    return this.http.post<Permissionaction>(`${environment.permissionAction}`, permAction,
      {headers:this.httpHeader.headers, responseType: 'text' as 'json'});
  }

  getAllPermissions(pageNumber: number, pageSize: number): Observable <DisplayAllPermission> {
    return this.http.get<DisplayAllPermission>(`${environment.getAllPermissions}?pageNumber=${pageNumber}&pageSize=${pageSize}`, this.httpHeader);
  }

  getPermissionsByTypeId(typeId: number): Observable <DisplayAllPermissionVM[]> {
    return this.http.get<DisplayAllPermissionVM[]>(`${environment.getPermissionsByTypeId}${typeId}`, this.httpHeader);
  }

  getPermissionsByDate(date: string): Observable <DisplayAllPermissionVM[]> {
    return this.http.get<DisplayAllPermissionVM[]>(`${environment.getPermissionsByDate}${date}`, this.httpHeader);
  }
}
