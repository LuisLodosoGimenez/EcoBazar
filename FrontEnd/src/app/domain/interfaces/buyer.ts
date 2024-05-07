import { CarritoCompra } from './shopping-cart';

export interface CompradorLogin {
  comprador: Comprador;
}

export interface Comprador {
  limite_gasto_cents_mes: number;
  carritoCompra: CarritoCompra[];
  id: number;
  nombre: string;
  nick_name: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: any;
}
