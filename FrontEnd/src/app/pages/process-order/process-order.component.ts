import { Component } from '@angular/core';
import { Comprador } from '../../domain/interfaces/buyer';
import { ShoppingCartApiService } from '../../services/shopping-cart-api.service';
import { ComponentNavigationService } from '../../services/component-navigation-services/component-navigation.service';
import { AppComponent } from '../../app.component';
import { CreateOrderApiService } from '../../services/create-order-api.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-process-order',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './process-order.component.html',
  styleUrl: './process-order.component.css',
})
export class ProcessOrderComponent {
  usuario?: Comprador;

  constructor(
    private shoppingCartApiService: ShoppingCartApiService,
    private notificationsService: ComponentNavigationService,
    private createOrderApiService: CreateOrderApiService,
    private router: Router,
  ) {
    this.usuario = AppComponent.usuario;
    if (AppComponent.usuario == undefined) {
      router.navigate(['../']);
    }
  }

  CreateOrder() {
    this.createOrderApiService.createOrder(this.usuario!).subscribe({
      next: (data) => {
        console.log('aque');
        console.log(data);
        AppComponent.usuario = data.comprador;
        this.usuario = data.comprador;
        this.notificationsService.showNotification('PEDIDO REALIZADO CON EXITO');
        this.router.navigate(['../']);
      },
      error: (error) => {
        this.notificationsService.showNotification('PROBLEMA INTERNO DEL SERVIDOR');
        console.log(error);
      },
    });
  }

  GetDate(days: number) {
    const today = new Date();
    return new Date(today.setDate(today.getDate() + days)).toLocaleDateString();
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
    return this.usuario?.carritoCompra.length;
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
