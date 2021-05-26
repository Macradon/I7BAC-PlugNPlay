import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-list-element',
  templateUrl: './list-element.component.html',
  styleUrls: ['./list-element.component.scss'],
})
export class ListElementComponent implements OnInit {
  @Input()
  public username: string = '';
  @Input()
  public online: boolean = false;

  @Output()
  public challengeEvent: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  public removeEvent: EventEmitter<string> = new EventEmitter<string>();

  constructor() {}

  ngOnInit(): void {}

  challenge() {
    this.challengeEvent.emit(this.username);
  }

  remove() {
    this.removeEvent.emit(this.username);
  }
}
