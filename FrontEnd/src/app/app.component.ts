import { CommonModule } from '@angular/common';
import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header.folder/header/header.component';
import { CompradorLogin } from './domain/interfaces/buyer';
import { ComponentNavigationService } from './services/component-navigation-services/component-navigation.service';

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
  static usuario?: CompradorLogin;
  notification: boolean = false;
  notificationMessage: string = '';
  @ViewChild('fullPage') fullPage!: ElementRef;

  constructor(
    private componentNavigationService: ComponentNavigationService,
    private renderer2: Renderer2,
  ) {}

  ngOnInit(): void {
    this.componentNavigationService.alert.subscribe((res: any) => {
      this.componentNavigationService.modifyOpacity(true);
      this.notification = true;
      this.notificationMessage = res.message;
      if (res.time != undefined) {
        setTimeout(() => this.closeNotification, res.time);
      }
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
}
