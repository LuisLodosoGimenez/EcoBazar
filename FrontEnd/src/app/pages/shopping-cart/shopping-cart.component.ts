import { Component } from '@angular/core';
import { AppComponent } from '../../app.component';
import { CompradorLogin } from '../../domain/interfaces/buyer';
import { CarritoCompra } from '../../domain/interfaces/shopping-cart';
import { ShoppingCartApiService } from '../../services/shopping-cart-api.service';
import { Producto } from '../../domain/interfaces/category-products';
import { ComponentNavigationService } from '../../services/component-navigation-services/component-navigation.service';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css',
})
export class ShoppingCartComponent {
  usuario?: CompradorLogin;

  constructor(
    private shoppingCartApiService: ShoppingCartApiService,
    private notificationsService: ComponentNavigationService,
  ) {
    this.usuario = AppComponent.usuario;
  }

  ReturnTotalPrice() {
    const precioCentString = this.usuario?.comprador.carritoCompra.reduce((a, b) => a + b.precio_cents, 0) + '';

    if (precioCentString == '0') return '00.00€';
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

  RemoveProductFromShoppintCart(producto: Producto) {
    let shoppingCart = {
      id_comprador: AppComponent.usuario!.comprador.id,
      id_producto: producto.id,
    };
    this.shoppingCartApiService.DeleteProductFromShoppingCart(shoppingCart).subscribe({
      next: (data) => {
        AppComponent.usuario!.comprador.carritoCompra = data.carritoCompra;
        console.log(data);
        this.notificationsService.showNotification(producto.articulo.nombre + ' HA SIDO ELIMINADO DEL CARRITO');
      },
      error: (error) => {
        console.log(error);
      },
    });

    this.shoppingCartApiService.DeleteProductFromShoppingCart;
  }
}
