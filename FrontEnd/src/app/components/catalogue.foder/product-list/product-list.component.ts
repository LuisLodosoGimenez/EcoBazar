import { Component, ElementRef, Input, Renderer2, ViewChild, ViewContainerRef } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { Producto, Productos } from '../../../domain/interfaces/category-products';
import { ShoppingCartApiService } from '../../../services/shopping-cart-api.service';
import { AppComponent } from '../../../app.component';
import { Router } from '@angular/router';

enum State {
  astive = 1,
  inactive = 0,
}

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent {
  @Input() categoryName!: string;
  @Input() productsByCategory!: Productos;
  productos = ['producto1', 'producto2', 'producto3', 'producto4', 'producto5'];
  productInfoState: State = 0;
  productDetails?: Producto;
  failText?: string;

  constructor(
    private renderer2: Renderer2,
    private shoppingCartApiService: ShoppingCartApiService,
    private router: Router,
  ) {}

  @ViewChild('fullPage') fullPage!: ElementRef;

  openProductInfo(product: Producto) {
    this.productDetails = product;
    this.productInfoState = 1;
  }

  closeProductInfo() {
    console.log('close');
    this.productInfoState = 0;
  }

  ReturnPrice() {
    const precioCentString = this.productDetails!.precio_cents + '';
    return (
      precioCentString.substring(0, precioCentString.length - 2) +
      ',' +
      precioCentString.substring(precioCentString.length - 2) +
      '€'
    );
  }

  AddProductToShoppingCart() {
    if (AppComponent.usuario == undefined) this.router.navigate(['/log-in']);
    console.log('ID_PRODUCTO:' + this.productDetails!.id);
    let shoppingCart = {
      id_comprador: AppComponent.usuario!.comprador.id,
      id_producto: this.productDetails!.id,
    };
    this.shoppingCartApiService.AddProductToShoppingCart(shoppingCart).subscribe({
      next: (data) => {
        AppComponent.usuario!.comprador.carritoCompra = data.carritoCompra;
        console.log(data);
        this.productDetails = undefined;
        this.productInfoState = 0;

        this.failText = 'REGISTRO CORRECTO. INICIE SESION';
      },
      error: (error) => {
        console.log(error);
        this.failText = error['error']['response'];
      },
    });
  }
}
