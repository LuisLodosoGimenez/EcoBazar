import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css',
})
export class UserProfileComponent {
  constructor(private router: Router) {
    if (AppComponent.usuario == undefined) {
      router.navigate(['../']);
    }
  }
  cerrarSesion() {
    AppComponent.usuario = undefined;
  }
}
