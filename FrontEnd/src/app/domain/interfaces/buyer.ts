import { Producto, Productos } from './category-products';

export interface RespuestaLogIn {
  comprador: Comprador;
}

export interface Comprador {
  id: number;
  nombre: string;
  nickName: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: string;
  limiteGastoCentsMes: number;
  carritoCompra: Producto[];
  pedidos: Pedido[];
}

export interface Pedido {
  id: number;
  estado: string;
  productosPedido: Producto[];
}
