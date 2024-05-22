import { Producto } from './category-products';

export interface ShoppingCartProduct {
  id_comprador: number;
  id_producto: number;
}

export interface NuevoCarritoCompra {
  carritoCompra: Producto[];
}
