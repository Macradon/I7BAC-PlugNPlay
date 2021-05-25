import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ChatMessage } from '../models/chat-message';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  @Input()
  public messages: ChatMessage[] = [];

  @Input()
  public disableSend: boolean = false;

  @Output()
  public sendMessage = new EventEmitter<string>();

  public chatControl: FormControl;

  constructor() {
    this.chatControl = new FormControl('');
  }

  ngOnInit(): void {}

  send() {
    if (this.chatControl.value !== '') {
      this.sendMessage.emit(this.chatControl.value);
      this.chatControl.setValue('');
      const elem = document.getElementById('msg');
      elem.scrollTop = elem.scrollHeight + 100;
    }
  }
}
