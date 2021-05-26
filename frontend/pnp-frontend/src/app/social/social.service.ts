import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SocialService {
  private friendlistBase =
    'https://plug-n-play-backend.herokuapp.com/friendlist';
  constructor(private http: HttpClient) {}

  getFriendlist() {}

  addFriend() {}
  removeFriend() {}
}
