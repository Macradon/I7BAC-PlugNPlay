import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { LoginDTO } from './models/login-dto';
import { RegisterDTO } from './models/register-dto';
import { User } from './models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //private baseUrl = 'https://plug-n-play-backend.herokuapp.com/auth';
  private baseUrl = 'https://localhost:5001/auth';
  private guestUser = { username: 'Guest' };
  public $userLoggedIn = new BehaviorSubject<boolean>(false);
  public $currentUser = new BehaviorSubject<User>(this.guestUser);
  constructor(private http: HttpClient) {}

  register(registerDTO: RegisterDTO) {
    return this.http.post<any>(`${this.baseUrl}/register`, registerDTO);
  }

  login(loginDTO: LoginDTO) {
    return this.http.post<any>(`${this.baseUrl}/login`, loginDTO, {
      observe: 'response',
    });
  }

  loginSuccessful(user: User) {
    this.$currentUser.next(user);
    this.$userLoggedIn.next(true);
  }

  logout() {
    this.$currentUser.next(this.guestUser);
    this.$userLoggedIn.next(false);
  }
}
