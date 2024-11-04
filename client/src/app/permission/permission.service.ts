import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPermissionType } from '../shared/models/permissiontype';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { DisplayAllPermission, Permissionaction } from '../shared/models/permissionaction';

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

  getAllPermissions(): Observable <DisplayAllPermission[]> {
    return this.http.get<DisplayAllPermission[]>(`${environment.getAllPermissions}`, this.httpHeader);
  }

  // getPermissionsByDate(): Observable <[]> {
  //   return this.http.get<DisplayAllPermission[]>(`${environment.getPermissionsByDate}`, this.httpHeader);
  // }

  // getPermissionsByTypeId(typeId: number): Observable <[]> {
  //   return this.http.get<DisplayAllPermission[]>(`${environment.getPermissionsByTypeId}/${typeId}`, this.httpHeader);
  // }
}
