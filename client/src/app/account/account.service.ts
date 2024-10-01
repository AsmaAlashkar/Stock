import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, map, of, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient, private router:Router) { }

  getCurrentUserValue(): IUser | null {
    return this.currentUserSource.value;
  }

  loadCurrentUser(token: string) {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    // Include the headers in the HTTP request
    return this.http.get<IUser>(this.baseUrl + 'account/GetCurrentUser/GetCurrentUser', { headers }).pipe(
      map((user: IUser) => {
        
        if (user && typeof user === 'object') {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        } else {
          console.error('Invalid user loaded from token:', user);
          return null;
        }
      }),
      catchError(error => {
        
        return of(null);
      })
    );
  }

  
  login(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'account/Login/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      }),
      catchError(error => {
        // Handle the error here (e.g., display a message to the user)
        console.error('Login failed:', error);
        // You can also return a user-friendly message
        return throwError(() => new Error('Login failed. Please check your credentials and try again.console'));
      })
    );
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !!token;  // Returns true if a token exists
  }



  register(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'account/Register/register', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          
          // Automatically log in the user after successful registration
          this.login(values).subscribe({
            next: () => {
              console.log('User logged in automatically after registration');
            },
            error: (error) => {
              console.error('Auto-login error after registration:', error);
            }
          });
        }
      })
    );
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

//this for Register
checkEmailExists(email:string){
  return this.http.get(this.baseUrl + 'account/CheckEmail/emailexists?email=' +email);
}

}