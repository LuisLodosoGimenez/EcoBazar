import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Comprador, RespuestaLogIn } from '../domain/interfaces/buyer';

@Injectable({
  providedIn: 'root',
})
export class CreateOrderApiService {
  constructor(private http: HttpClient) {}

  createOrder(body: Comprador) {
    const headers = new HttpHeaders({
      accept: '*/*',
      'Content-Type': 'application/json-patch+json',
      'Access-Control-Allow-Origin': '*',
    });

    return this.http.post<RespuestaLogIn>(`http://localhost:5237/api/Api/CrearPedido`, body);
  }
}
