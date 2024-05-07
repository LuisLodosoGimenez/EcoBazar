import { Component } from '@angular/core';
import { AppComponent } from '../../app.component';
import { CompradorLogin } from '../../domain/interfaces/buyer';
import { CarritoCompra } from '../../domain/interfaces/shopping-cart';
import { ShoppingCartApiService } from '../../services/shopping-cart-api.service';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css',
})
export class ShoppingCartComponent {
  usuario?: CompradorLogin;

  constructor(private shoppingCartApiService: ShoppingCartApiService) {
    this.usuario = AppComponent.usuario;
  }

  ReturnTotalPrice() {
    const precioCentString = this.usuario?.comprador.carritoCompra.reduce((a, b) => a + b.precio_cents, 0) + '';
    return (
      precioCentString.substring(0, precioCentString.length - 2) +
      ',' +
      precioCentString.substring(precioCentString.length - 2) +
      '€'
    );
  }
  ReturnShoppingCartSize() {
    return this.usuario?.comprador.carritoCompra.length;
  }
  ReturnPrice(price: number) {
    const precioCentString = price + '';
    return (
      precioCentString.substring(0, precioCentString.length - 2) +
      ',' +
      precioCentString.substring(precioCentString.length - 2) +
      '€'
    );
  }

  RemoveProductFromShoppintCart(productoId: number) {
    let shoppingCart = {
      id_comprador: AppComponent.usuario!.comprador.id,
      id_producto: productoId,
    };
    this.shoppingCartApiService.DeleteProductFromShoppingCart(shoppingCart).subscribe({
      next: (data) => {
        AppComponent.usuario!.comprador.carritoCompra = data.carritoCompra;
        console.log(data);
      },
      error: (error) => {
        console.log(error);
      },
    });

    this.shoppingCartApiService.DeleteProductFromShoppingCart;
  }
}
