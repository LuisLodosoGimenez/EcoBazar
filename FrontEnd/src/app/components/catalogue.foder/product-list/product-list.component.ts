import { Component, ElementRef, Input, Renderer2, ViewChild, ViewContainerRef } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { Producto, Productos } from '../../../domain/interfaces/category-products';

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

  constructor(private renderer2: Renderer2) {}

  @ViewChild('fullPage') fullPage!: ElementRef;

  openProductInfo() {
    console.log('open');
    this.productInfoState = 1;
  }

  closeProductInfo() {
    console.log('close');
    this.productInfoState = 0;
  }
}
