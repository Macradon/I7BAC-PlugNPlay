import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../shared/models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public $currentUser = new BehaviorSubject<User>(null);
  constructor() {}
}
