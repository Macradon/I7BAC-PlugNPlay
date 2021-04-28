import { Injectable } from '@angular/core';

import { SignalRService } from '../shared/signal-r.service';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  public gameActive = false;
  public gameRoomId = '';

  constructor(private socket: SignalRService) {}
}
