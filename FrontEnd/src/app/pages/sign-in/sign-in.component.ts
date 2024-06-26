import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { SignInApiService } from '../../services/sign-in-api.service';
import { ComponentNavigationService } from '../../services/component-navigation-services/component-navigation.service';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ReactiveFormsModule, RouterLink],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent {
  failText?: String;

  constructor(
    private signInApiService: SignInApiService,
    private notificationsService: ComponentNavigationService,
  ) {}

  result = '';
  result1 = '';

  formularioRegistro = new FormGroup({
    nombre: new FormControl('', [Validators.required]),
    apellidos: new FormControl('', [Validators.required]),
    mail: new FormControl('', [Validators.required]),
    nickname: new FormControl('', [Validators.required]),
    contraseña: new FormControl('', [Validators.required]),
    edad: new FormControl('', [Validators.required]),
  });

  registrase() {
    let body = {
      tipoRegistro: 'comprador',
      nombre: String(this.formularioRegistro.value.nombre) + ' ' + String(this.formularioRegistro.value.apellidos),
      nickName: String(this.formularioRegistro.value.nickname),
      contraseña: String(this.formularioRegistro.value.contraseña),
      email: String(this.formularioRegistro.value.mail),
      edad: Number(this.formularioRegistro.value.edad),
      limiteGasto: 100,
    };

    this.signInApiService.signIn(body).subscribe({
      next: (data) => {
        console.log(data);

        this.failText = 'REGISTRO CORRECTO. INICIE SESION';
      },
      error: (error) => {
        var failure: String = error['error']['response'];
        if (failure.includes('NickName') || failure.includes('Email')) {
          this.failText = failure;
        } else this.notificationsService.showNotification('PROBLEMA INTERNO DEL SERVIDOR');
      },
    });
  }

  mailIsOk(): void {
    const mailProvided = this.formularioRegistro.get('mail')?.value;
    const optionsMail = ['@gmail.com', '@hotmail.com'];

    if (mailProvided && mailProvided.trim() != '') {
      if (optionsMail.some((optionMail) => mailProvided.includes(optionMail))) this.result = 'Correo aceptado';
      else this.result = 'Correo no admisible';
    } else this.result = 'Es obligatorio añadir un correo';
  }

  passwordIsOk(): void {
    const passwordProvided = this.formularioRegistro.get('contraseña')?.value;

    if (passwordProvided != null) {
      if (this.validatePassword(passwordProvided)) this.result1 = 'y Contraseña válida';
      else this.result1 = 'y Contraseña no válida';
    } else this.result1 = 'y Es obligatorio añadir una contraseña';
  }

  validatePassword(passwordP: string): boolean {
    const capitalLetter = /[A-Z]/.test(passwordP);
    const hasNumber = /[0-9]/.test(passwordP);
    const specialCharacter = /[^A-Za-z0-9]/.test(passwordP);

    return capitalLetter && hasNumber && specialCharacter;
  }
}
