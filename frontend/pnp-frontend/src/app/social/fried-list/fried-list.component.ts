import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-fried-list',
  templateUrl: './fried-list.component.html',
  styleUrls: ['./fried-list.component.scss'],
})
export class FriedListComponent implements OnInit {
  public onlineFriends: string[] = ['Guest'];
  public offlineFriends: string[] = ['God', 'Goat'];

  public requests: string[] = ['Satan'];

  constructor() {}

  ngOnInit(): void {}
}
