import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }
  

  localCurrentUser(token: string){

    if( token === null){

      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl + 'account/GetCurrentUser', {headers}).pipe(
      map((user: IUser) => {
        if(user){
        localStorage.setItem('token', user.token); 
        this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: any){
    return this.http.post(this.baseUrl +  'account/login', values).pipe(
      map((user: IUser) => {
        if(user){
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          console.log(user);
        }
      })
    );
  }
  register(values: any){
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: IUser) => {
        if(user){
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }
  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
  checkEmailExits(email: string){
    return this.http.get(this.baseUrl + 'account/emailexits?email=' + email );

  }
  
}
