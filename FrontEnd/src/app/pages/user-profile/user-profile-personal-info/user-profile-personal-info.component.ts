import { Component } from '@angular/core';
import { AppComponent } from '../../../app.component';
import { Comprador } from '../../../domain/interfaces/buyer';

@Component({
  selector: 'app-user-profile-personal-info',
  standalone: true,
  imports: [],
  templateUrl: './user-profile-personal-info.component.html',
  styleUrl: './user-profile-personal-info.component.css',
})
export class UserProfilePersonalInfoComponent {
  usuario?: Comprador;

  constructor() {
    this.usuario = AppComponent.usuario;
  }
}
