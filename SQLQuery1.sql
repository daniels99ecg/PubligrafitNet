create table Publigrafit

use Publigrafit;

CREATE TABLE usuario (
  id_usuario int  IDENTITY(1,1) PRIMARY KEY  NOT NULL,
  fk_rol2 int NOT NULL,
  nombres varchar(50) NOT NULL,
  apellidos varchar(50) NOT NULL,
  email varchar(40) NOT NULL,
  contrasena varchar(20) NOT NULL,
	estado bit not null

) 


CREATE TABLE rol (
  id_rol int  IDENTITY(1,1) PRIMARY KEY  NOT NULL,
  nombre_rol varchar(20) NOT NULL,
  fecha date NOT NULL,
  estado tinyint NOT NULL,

)

ALTER TABLE usuario
ADD CONSTRAINT fk_rol2 FOREIGN KEY (fk_rol2) REFERENCES rol (id_rol);

INSERT INTO rol (nombre_rol, fecha, estado) VALUES
('Administrador', '2023-05-16', 1),
('Empleado', '2023-05-16', 1),
('Empleado', '2023-05-16', 1);

select * from rol


select * from Usuario

insert into Usuario (fk_rol2, nombres, apellidos, email, contrasena) values(1,'daniel','garcia','daniel@gmail.com','as12')

create table permiso(
id_permiso varchar (10) PRIMARY KEY not null,
    nombre_permiso varchar (40) not null
);

create table rol_x_permiso(
id_rol_x_permiso int PRIMARY KEY not null,
    fk_rol int not null,
    fk_permiso varchar (10) not null
);

ALTER TABLE rol_x_permiso
ADD CONSTRAINT fk_permiso FOREIGN KEY (fk_permiso) REFERENCES permiso (id_permiso);
--ADD CONSTRAINT fk_rol FOREIGN KEY (fk_rol) REFERENCES rol (id_rol);


create table compras(
id_compra int identity (1,1) PRIMARY KEY not null,
    proveedor varchar (50) not null,
    cantidad int not null,
    fecha date not null,
    total float (10),
	estado bit not null

);

create table detalle_compra(
id_detalle_compra int identity (1,1) PRIMARY KEY not null,
    fk_compra int not null,
    fk_insumo2 int not null,
    cantidad int not null,
    precio float (10) not null,
    iva float (5) not null,
    subtotal float (10) not null,

);

create table insumos(
id_insumo int identity (1,1) PRIMARY KEY not null,
    nombre varchar (50) not null,
    precio float (10) not null,
    cantidad int not null,
		estado bit not null

);


ALTER TABLE detalle_compra
ADD CONSTRAINT fk_insumo2 FOREIGN KEY (fk_insumo2) REFERENCES insumos (id_insumo);
--ADD CONSTRAINT fk_compra FOREIGN KEY (fk_compra) REFERENCES compras (id_compra);


create table ficha_tecnica(
id_ft int identity (1,1) PRIMARY KEY not null,
    fk_insumo int not null,
    cantidad_insumo int not null,
    costo_insumo float (10) not null,
    imagen_producto_final image not null,
    costo_final_producto float (10) not null,
    detalle varchar (255) not null,
		estado bit not null

);

ALTER TABLE ficha_tecnica
ADD CONSTRAINT fk_insumo FOREIGN KEY (fk_insumo) REFERENCES insumos (id_insumo);

create table producto(
id_producto int identity (1,1) PRIMARY KEY not null,
    fk_categoria int not null,
    nombre_producto varchar (50) not null,
    precio float (10) not null,
    imagen image not null,
    cantidad_producto int not null,
    stock int not null,
);

create table categoria(
id_categoria int identity (1,1) PRIMARY KEY not null,
    nombre varchar (50) not null
);

ALTER TABLE producto
ADD CONSTRAINT fk_categoria FOREIGN KEY (fk_categoria) REFERENCES categoria (id_categoria);

create table venta(
id_venta int identity (1,1) PRIMARY KEY not null,
    fk_dni_cliente int not null,
    tipo_comprobante varchar (20) not null,
    fecha date not null,
    total float (10) not null
);

create table detalle_venta(
id_detalle_venta int identity (1,1) PRIMARY KEY not null,
    fk_venta  int not null,
    fk_producto int not null,
    cantidad int not null,
    precio float (10) not null,
    iva float (5) not null,
    subtotal float (10) not null
);

ALTER TABLE detalle_venta
ADD CONSTRAINT fk_producto FOREIGN KEY (fk_producto) REFERENCES producto (id_producto);
--ADD CONSTRAINT fk_venta FOREIGN KEY (fk_venta) REFERENCES venta (id_venta);


create table cliente(
dni_cliente int PRIMARY KEY not null,
    nombre varchar (50) not null,
    apellido varchar (50) not null,
    telefono varchar (15) null,
    direccion varchar (50) null,
    email varchar (40) null
);

ALTER TABLE venta
ADD CONSTRAINT fk_dni_cliente FOREIGN KEY (fk_dni_cliente) REFERENCES cliente (dni_cliente);