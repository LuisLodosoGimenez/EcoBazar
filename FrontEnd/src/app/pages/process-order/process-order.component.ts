import { Component } from '@angular/core';
import { CompradorLogin } from '../../domain/interfaces/buyer';
import { ShoppingCartApiService } from '../../services/shopping-cart-api.service';
import { ComponentNavigationService } from '../../services/component-navigation-services/component-navigation.service';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-process-order',
  standalone: true,
  imports: [],
  templateUrl: './process-order.component.html',
  styleUrl: './process-order.component.css',
})
export class ProcessOrderComponent {
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
}
