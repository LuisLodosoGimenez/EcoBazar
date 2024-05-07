import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ComponentNavigationService {
  private alertSource = new Subject();
  alert = this.alertSource.asObservable();

  private bgOpacitySource = new Subject();
  bgOpacity = this.bgOpacitySource.asObservable();

  constructor() {}

  showNotification(message: string, time?: number) {
    this.alertSource.next({ message, time });
  }

  modifyOpacity(lowOpacity: boolean) {
    this.bgOpacitySource.next(lowOpacity);
  }
}
