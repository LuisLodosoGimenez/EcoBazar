import { CommonModule } from '@angular/common';
import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Usuario } from './domain/classes/usuario';
import { ApiService } from './services/api.service';
import { HeaderComponent } from './components/header.folder/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppComponent {
  constructor(private apiService: ApiService) {}
  title = 'userProfileSpringCarrot';
  static usuario: Usuario = {
    id: 0,
    nombre: '',
    nick_name: '',
    email: '',
    edad: 0,
    pedidos: [],
    carrito_compra: [],
  };

  haIniciado() {
    if (
      AppComponent.usuario.id == 0 &&
      AppComponent.usuario.nombre == '' &&
      AppComponent.usuario.nick_name == '' &&
      AppComponent.usuario.email == '' &&
      AppComponent.usuario.edad == 0 &&
      AppComponent.usuario.pedidos.length == 0 &&
      AppComponent.usuario.carrito_compra.length == 0
    )
      return false;
    else return true;
  }
}
