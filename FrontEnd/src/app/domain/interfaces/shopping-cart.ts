import { Producto } from './category-products';

export interface ShoppingCartProduct {
  idComprador: number;
  idProducto: number;
}

export interface NuevoCarritoCompra {
  carritoCompra: Producto[];
}
