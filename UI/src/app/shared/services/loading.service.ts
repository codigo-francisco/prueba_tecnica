import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private bs = new BehaviorSubject<boolean>(false);

  get SendState() {
    return this.bs;
  }

  constructor() { }

  show() {
    this.bs.next(false);
  }

  hide() {
    this.bs.next(true);
  }
}
