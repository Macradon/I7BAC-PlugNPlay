import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-fried-list',
  templateUrl: './fried-list.component.html',
  styleUrls: ['./fried-list.component.scss'],
})
export class FriedListComponent implements OnInit {
  public friendControl: FormControl;
  public onlineFriends: string[] = ['Guest'];
  public offlineFriends: string[] = ['God', 'Goat'];

  public requests: string[] = ['Satan'];

  constructor() {
    this.friendControl = new FormControl('');
  }

  ngOnInit(): void {}

  addFriend() {}

  onRemove() {}

  onChallenge() {}
  onAccept() {}
  onReject() {}
}
