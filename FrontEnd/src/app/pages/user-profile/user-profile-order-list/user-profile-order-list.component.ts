import { Component } from '@angular/core';
import { Comprador, Pedido } from '../../../domain/interfaces/buyer';
import { AppComponent } from '../../../app.component';

@Component({
  selector: 'app-user-profile-order-list',
  standalone: true,
  imports: [],
  templateUrl: './user-profile-order-list.component.html',
  styleUrl: './user-profile-order-list.component.css',
})
export class UserProfileOrderListComponent {
  usuario?: Comprador = AppComponent.usuario;

  ReturnTotalPrice(pedido: Pedido) {
    const precioCentString = pedido.productosPedido.reduce((a, b) => a + b.precio_cents, 0) + '';

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
}
