import { CommonModule } from '@angular/common';
import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header.folder/header/header.component';
import { ComponentNavigationService } from './services/component-navigation-services/component-navigation.service';
import { Comprador } from './domain/interfaces/buyer';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppComponent implements OnInit {
  title = 'userProfileSpringCarrot';
  static usuario?: Comprador;
  notification: boolean = false;
  notificationMessage: string = '';
  confirmationDialog: boolean = false;
  confirmationDialogMessage: string = '';
  confirmationDialogButtonMessage: string = '';
  confirmationDialogFunc: Function = () => {};
  @ViewChild('fullPage') fullPage!: ElementRef;

  constructor(
    private componentNavigationService: ComponentNavigationService,
    private renderer2: Renderer2,
  ) {}

  ngOnInit(): void {
    this.componentNavigationService.notification.subscribe((res: any) => {
      console.log('mostrar panel notificaciónas');
      this.componentNavigationService.modifyOpacity(true);
      this.notification = true;
      this.notificationMessage = res.message;
      if (res.time != undefined) {
        setTimeout(() => this.closeNotification, res.time);
      }
    });

    this.componentNavigationService.confirmationDialog.subscribe((res: any) => {
      console.log('mostrar panel confirmacion');
      this.componentNavigationService.modifyOpacity(true);
      this.confirmationDialog = true;
      this.confirmationDialogMessage = res.message;
      this.confirmationDialogButtonMessage = res.buttonMessage;
      this.confirmationDialogFunc = res.func;
    });

    this.componentNavigationService.bgOpacity.subscribe((lowOpacity: any) => {
      this.modifyBgOpacity(lowOpacity);
    });
  }

  modifyBgOpacity(lowOpacity: boolean) {
    const body = this.fullPage.nativeElement;
    if (lowOpacity) this.renderer2.setStyle(body, 'opacity', '0.5');
    else this.renderer2.setStyle(body, 'opacity', '1');
  }

  haIniciado() {
    if (AppComponent.usuario == null) return false;
    else return true;
  }

  closeNotification() {
    this.componentNavigationService.modifyOpacity(false);
    this.notificationMessage = '';
    this.notification = false;
  }

  closeConfirmationDialog() {
    this.componentNavigationService.modifyOpacity(false);
    this.confirmationDialog = false;
    this.confirmationDialogMessage = '';
    this.confirmationDialogButtonMessage = '';
  }

  confirmAction() {
    this.confirmationDialogFunc();
  }
}
