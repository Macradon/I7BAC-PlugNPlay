import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss'],
})
export class RequestComponent implements OnInit {
  @Input()
  public username: string = '';

  @Output()
  public acceptedEvent: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  public rejectedEvent: EventEmitter<string> = new EventEmitter<string>();

  constructor() {}

  ngOnInit(): void {}

  accept() {
    this.acceptedEvent.emit(this.username);
  }

  reject() {
    this.rejectedEvent.emit(this.username);
  }
}
