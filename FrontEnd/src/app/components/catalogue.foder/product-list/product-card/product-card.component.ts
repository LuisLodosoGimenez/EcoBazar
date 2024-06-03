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
  @Output() openProductInfo = new EventEmitter<Producto>();

  ClickProduct() {
    this.openProductInfo.emit(this.product);
  }

  ReturnPrice() {
    const precioCentString = this.product.precioCents + '';
    return (
      precioCentString.substring(0, precioCentString.length - 2) +
      ',' +
      precioCentString.substring(precioCentString.length - 2) +
      '€'
    );
  }
}
