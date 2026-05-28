import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, take } from 'rxjs';

import { API_CONFIG } from '../core/config/api.config';
import { User } from '../models/identity/User';
import { UserLogin } from '../models/identity/UserLogin';
import { UserUpdate } from '../models/identity/UserUpdate';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSource.asObservable();

  private readonly baseUrl = `${API_CONFIG.apiUrl}/account/`;

  constructor(private http: HttpClient) {
    this.loadCurrentUser();
  }

  login(model: UserLogin): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      take(1),
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }

        return user;
      })
    );
  }

  register(model: User): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'register', model).pipe(
      take(1),
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }

        return user;
      })
    );
  }

  getUser(): Observable<UserUpdate> {
    return this.http.get<UserUpdate>(this.baseUrl + 'getUser').pipe(take(1));
  }

  updateUser(model: UserUpdate): Observable<User> {
    return this.http.put<User>(this.baseUrl + 'updateUser', model).pipe(
      take(1),
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }

        return user;
      })
    );
  }

  setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  loadCurrentUser(): void {
    const userJson = localStorage.getItem('user');

    if (!userJson) {
      this.currentUserSource.next(null);
      return;
    }

    try {
      const user = JSON.parse(userJson) as User;
      this.currentUserSource.next(user?.token ? user : null);
    } catch {
      localStorage.removeItem('user');
      this.currentUserSource.next(null);
    }
  }

  logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
