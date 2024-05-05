import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Producto } from '../../../../domain/interfaces/category-products';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css',
})
export class ProductCardComponent {
  @Input() product!: Producto;
  @Output() openProductInfo = new EventEmitter<string>();

  ClickProduct() {
    this.openProductInfo.emit();
  }
}
