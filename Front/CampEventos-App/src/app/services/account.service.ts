import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, take } from 'rxjs';

import { User } from '../models/identity/User';
import { UserUpdate } from '../models/identity/UserUpdate';
import { API_CONFIG } from '../core/config/api.config';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSource.asObservable();

  baseUrl = `${API_CONFIG.apiUrl}/account/`;

  constructor(private http: HttpClient) {
    this.loadCurrentUser();
  }

  login(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      take(1),
      map((user: User) => {
        if (user) this.setCurrentUser(user);
      })
    );
  }

  register(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'register', model).pipe(
      take(1),
      map((response: User) => {
        
        if(response) {
          this.setCurrentUser(response);
        }
      })
    );
  }

  getUser(): Observable<UserUpdate> {
    return this.http.get<UserUpdate>(this.baseUrl + 'getUser').pipe(take(1));
  }

  updateUser(model: UserUpdate): Observable<void> {
    return this.http.put<User>(this.baseUrl + 'updateUser', model).pipe(
      take(1),
      map((user: User) => {
        if (user) this.setCurrentUser(user);
      })
    );
  }

  public setCurrentUser(user: User): void {

    if(user) {
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
    }
  }
  
    public loadCurrentUser(): void {

    const userJson = localStorage.getItem('user');

    if(!userJson) return;

    const user: User = JSON.parse(userJson);

    this.currentUserSource.next(user);
  }


    logout(): void {

    localStorage.removeItem('user');

    this.currentUserSource.next(null);
  }
}