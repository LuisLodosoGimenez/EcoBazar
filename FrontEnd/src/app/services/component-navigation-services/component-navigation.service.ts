import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ComponentNavigationService {
  private notificationSource = new Subject();
  notification = this.notificationSource.asObservable();

  private confirmationDialogSource = new Subject();
  confirmationDialog = this.confirmationDialogSource.asObservable();

  private bgOpacitySource = new Subject();
  bgOpacity = this.bgOpacitySource.asObservable();

  constructor() {}

  showConfirmationDialog(message: string, buttonMessage: string, func: () => void) {
    console.log('servicio confirmación');
    this.confirmationDialogSource.next({ message, buttonMessage, func });
  }

  showNotification(message: string, time?: number) {
    this.notificationSource.next({ message, time });
  }

  modifyOpacity(lowOpacity: boolean) {
    this.bgOpacitySource.next(lowOpacity);
  }
}
