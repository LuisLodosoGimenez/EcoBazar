export type Productos = Producto[];

export interface Producto {
  id: number;
  precioCents: number;
  unidades: number;
  diasEntrega: number;
  vendedor: Vendedor;
  articulo: Articulo;
  observadoresProducto: any[];
  descuentoAplicado: DescuentoAplicado;
}

export interface Vendedor {
  id: number;
  nombre: string;
  nickName: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: any;
}

export interface Articulo {
  id: number;
  nombre: string;
  categoria: string;
  edadMin: number;
  consejosUtilizacion: string;
  consejosRetirada: string;
  origen: string;
  procesoProduccion: string;
  impactoAmbientalSocial: string;
  contribucionOds: string;
  productor: Productor;
  productos: any[];
  imagenesUrl: string[];
}

export interface Productor {
  id: number;
  nombre: string;
  nickName: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: any;
}

export interface DescuentoAplicado {
  textoDescuento: string;
}
