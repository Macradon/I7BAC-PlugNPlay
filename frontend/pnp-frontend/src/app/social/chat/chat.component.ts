import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  public messages = [];

  constructor() {
    this.messages = [
      { username: 'Guest', message: 'Ello' },
      { username: 'God', message: 'Yo mama' },
    ];

    this.messages.push({ username: 'Satan', message: 'No yo mama' });
  }

  ngOnInit(): void {}

  send() {
    this.messages.push({ username: 'Satan', message: 'No yo mama' });
    const elem = document.getElementById('msg');
    elem.scrollTop = elem.scrollHeight + 100;
  }
}
