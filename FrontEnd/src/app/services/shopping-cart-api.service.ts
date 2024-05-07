import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NuevoCarritoCompra, ShoppingCartProduct } from '../domain/interfaces/shopping-cart';

@Injectable({
  providedIn: 'root',
})
export class ShoppingCartApiService {
  constructor(private http: HttpClient) {}

  AddProductToShoppingCart(body: ShoppingCartProduct) {
    const headers = new HttpHeaders({
      accept: '*/*',
      'Content-Type': 'application/json-patch+json',
      'Access-Control-Allow-Origin': '*',
    });

    return this.http.post<NuevoCarritoCompra>(`http://localhost:5237/api/Api/AñadirProductoACarritoCompra`, body);
  }

  DeleteProductFromShoppingCart(body: ShoppingCartProduct) {
    const headers = new HttpHeaders({
      accept: '*/*',
      'Content-Type': 'application/json-patch+json',
      'Access-Control-Allow-Origin': '*',
    });

    return this.http.post<NuevoCarritoCompra>(`http://localhost:5237/api/Api/EliminarProductoEnCarritoCompra`, body);
  }
}
