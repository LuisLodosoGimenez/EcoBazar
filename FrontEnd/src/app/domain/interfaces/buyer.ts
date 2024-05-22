import { Producto, Productos } from './category-products';

export interface RespuestaLogIn {
  comprador: Comprador;
}

export interface Comprador {
  limite_gasto_cents_mes: number;
  carritoCompra: Productos;
  pedidos: Pedido[];
  id: number;
  nombre: string;
  nick_name: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: any;
}

export interface Pedido {
  id: number;
  estado: string;
  productosPedido: Producto[];
}
