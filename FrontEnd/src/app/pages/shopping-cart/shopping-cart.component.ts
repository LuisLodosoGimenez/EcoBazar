import { Component } from '@angular/core';
import { AppComponent } from '../../app.component';
import { ShoppingCartApiService } from '../../services/shopping-cart-api.service';
import { Producto } from '../../domain/interfaces/category-products';
import { ComponentNavigationService } from '../../services/component-navigation-services/component-navigation.service';
import { Router, RouterLink } from '@angular/router';
import { Comprador } from '../../domain/interfaces/buyer';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css',
})
export class ShoppingCartComponent {
  usuario?: Comprador;

  constructor(
    private shoppingCartApiService: ShoppingCartApiService,
    private notificationsService: ComponentNavigationService,
    private router: Router,
  ) {
    this.usuario = AppComponent.usuario;
    if (AppComponent.usuario == undefined) {
      router.navigate(['../']);
    }
  }

  ReturnTotalPrice() {
    const precioCentString = this.usuario?.carritoCompra.reduce((a, b) => a + b.precio_cents, 0) + '';

    if (precioCentString == '0') return '00.00€';
    return (
      precioCentString.substring(0, precioCentString.length - 2) +
      ',' +
      precioCentString.substring(precioCentString.length - 2) +
      '€'
    );
  }
  ReturnShoppingCartSize() {
    return AppComponent.usuario!.carritoCompra.length;
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
      id_comprador: AppComponent.usuario!.id,
      id_producto: producto.id,
    };
    this.shoppingCartApiService.DeleteProductFromShoppingCart(shoppingCart).subscribe({
      next: (data) => {
        AppComponent.usuario!.carritoCompra = data.carritoCompra;
        console.log(data);
        this.notificationsService.showNotification(producto.articulo.nombre + ' HA SIDO ELIMINADO DEL CARRITO');
      },
      error: (error) => {
        this.notificationsService.showNotification('PROBLEMA INTERNO DEL SERVIDOR');
      },
    });

    this.shoppingCartApiService.DeleteProductFromShoppingCart;
  }
}
