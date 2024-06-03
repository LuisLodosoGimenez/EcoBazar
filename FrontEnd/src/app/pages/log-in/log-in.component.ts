import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterModule, ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { LogInApiService } from '../../services/log-in-api.service';
import { ComponentNavigationService } from '../../services/component-navigation-services/component-navigation.service';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ReactiveFormsModule, RouterLink, RouterModule],
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss'],
})
export class LogInComponent implements OnInit {
  failText: String = '';
  url?: String;

  constructor(
    private router: Router,
    private logInApiService: LogInApiService,
    private route: ActivatedRoute,
    private notificationsService: ComponentNavigationService,
  ) {}

  ngOnInit() {
    console.log('hola');
    this.route.queryParams.subscribe((params) => {
      this.url = params['page'];
      console.log(this.url); // { order: "popular" }
      // popular
    });
    console.log(this.url!);
  }

  formularioInicioSesion = new FormGroup({
    nickname: new FormControl('', [Validators.required]),
    contraseña: new FormControl('', [Validators.required]),
  });

  camposCompletos() {
    return this.formularioInicioSesion.value.nickname && this.formularioInicioSesion.value.contraseña;
  }

  iniciarSesion() {
    let body = {
      tipoSesion: 'comprador',
      nickName: String(this.formularioInicioSesion.value.nickname),
      contraseña: String(this.formularioInicioSesion.value.contraseña),
    };

    this.logInApiService.logIn(body).subscribe({
      next: (data) => {
        console.log(data);
        AppComponent.usuario = data.comprador;

        console.log(AppComponent.usuario);
        if (this.url == null) this.router.navigate(['../']);
        this.router.navigate([this.url]);
      },
      error: (error) => {
        var failure: String = error['error']['response'];
        if (failure.includes('El NickName') || failure.includes('Contraseña')) {
          this.failText = failure;
        } else this.notificationsService.showNotification('PROBLEMA INTERNO DEL SERVIDOR');
      },
    });
  }
}
