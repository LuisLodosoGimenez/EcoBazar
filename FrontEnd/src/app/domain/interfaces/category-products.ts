export type Productos = Producto[];

export interface Producto {
  id: number;
  precio_cents: number;
  unidades: number;
  dias_entrega: number;
  vendedor: Vendedor;
  articulo: Articulo;
}

export interface Vendedor {
  id: number;
  nombre: string;
  nick_name: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: any;
}

export interface Articulo {
  id: number;
  nombre: string;
  categoria: string;
  edad_min: number;
  consejos_utilizacion: string;
  consejos_retirada: string;
  origen: string;
  proceso_produccion: string;
  impacto_ambiental_social: string;
  contribucion_ods: string;
  productor: Productor;
  productos: any[];
  imagenesUrl: string[];
}

export interface Productor {
  id: number;
  nombre: string;
  nick_name: string;
  contraseña: string;
  email: string;
  edad: number;
  imagenesUrl: any;
}
