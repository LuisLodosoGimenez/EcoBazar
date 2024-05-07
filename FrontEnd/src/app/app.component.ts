import { CommonModule } from '@angular/common';
import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header.folder/header/header.component';
import { CompradorLogin } from './domain/interfaces/buyer';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppComponent {
  constructor() {}
  title = 'userProfileSpringCarrot';
  static usuario?: CompradorLogin;

  haIniciado() {
    if (AppComponent.usuario == null) return false;
    else return true;
  }
}
