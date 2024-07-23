# Descripcion del Proyecto
Este sistema ha sido desarrollado con C# y con arquitectura en Capas, en una capa todo lo que tiene que ver con los datos de la BD, su contexto y modelos estan en esta capa, otra las interfaces y servicios que esta capa de Negocio, es la capa que interactua directamente con la base de datos y sus propiedades, en otra la webAPI la cual en esta se usan los servicios desarrollados en la capa de negocios y se aplican directamente en los controladores de la misma, y en otra El frontend con WindowsForms; por lo que tenemos un total de 4 capas en el desarrollo de este sistema.


## BASE DE  DATOS DEL PROYECTO, se ejecutan las tablas y se insertan datos de pureba:

CREATE DATABASE SISTEMA_POS;
USE SISTEMA_POS;

CREATE TABLE Producto (
    Id INT PRIMARY KEY identity(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripción TEXT,
    Precio DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Dirección (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    Calle VARCHAR(200),
    Ciudad VARCHAR(100),
    Estado VARCHAR(100),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);

CREATE TABLE Pedido (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    DirecciónId INT NOT NULL,
    Fecha DATETIME NOT NULL,
    Producto VARCHAR(100),
    Total DECIMAL(10, 2) NOT NULL,           
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (DirecciónId) REFERENCES Dirección(Id)
);

CREATE TABLE Rol (
    Id INT PRIMARY KEY identity(1,1),
    Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Usuario (
    Id INT PRIMARY KEY identity(1,1),
    Nombres VARCHAR(50) NOT NULL,
	Apellidos VARCHAR(50) NOT NULL,
	Sexo varchar(50) NOT NULL,
	email VARCHAR(50) NOT NULL UNIQUE ,
    Contrasena VARCHAR(100) NOT NULL,
    RolId INT NOT NULL DEFAULT 2,
    FOREIGN KEY (RolId) REFERENCES Rol(Id)
);

-- Insertar roles iniciales
INSERT INTO Rol (Nombre) VALUES ('Administrador'), ('Cliente');
insert into Usuario(Nombres,Apellidos,Sexo,email,Contrasena,RolId) values('Admin1','prueba','Masculino','admin@gmail.com','passwordExample',1);
insert into Producto(Nombre,Descripción,Precio) values('Piza','Descripcion 1',200),('Piza queso 1','Descripcion 2',300),('Papas fritas','Descripcion 2',250)
select * from Usuario;
----------------------------------------------------------------------------------------------------------------
## Modelo relacional de la base de datos
* Modelo Relacional de la base de datos:
![](https://res.cloudinary.com/disw7bgxd/image/upload/v1721387716/ModeloRelacionalBD_kgwvjz.jpg)
--------------------------------------------------------------------------------------------------------------
## Una vez creada la base de datos deben hacer lo siguiente
1. Abre SQL Server Management Studio (SSMS) y conéctate a tu instancia de SQL Server.
2. La instancia a la que te conectes deberas agregarla al archivo appsettings.json del proyecto
--------------------------------------------------------------------------------------------------------------
## Utilizacion de Scaffold para el contexto de la base de datos con sus modelos
#### Acontinuacion su comando:
* Scaffold-DbContext "Server='Tu_ServidorBD';DataBase=SISTEMA_POS;Integrated Security=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir DataContext
---------------------------------------------------------------------------------------------------------------
## Paquetes utilizados en el desarrollo del sistema
* Microsoft.EntityFrameworkCore  
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCor.Tools
* BCrypt
* Newtonsof.Json
------------------------------------------------------------------------------------------------------------------
## Funcionamiento del sistema
* Endpoints del sistema
![](https://res.cloudinary.com/disw7bgxd/image/upload/v1721388038/Endpoints_lqmef0.jpg)

---------------------------------------------------------------------------------------------------------------------
## Frontend Funcional con WindowsForm
* CRUD de productos
![](https://res.cloudinary.com/disw7bgxd/image/upload/v1721388427/Editar_adterf.jpg)
![](https://res.cloudinary.com/disw7bgxd/image/upload/v1721388427/Eliminar_tfibdx.jpg)
![](hhttps://res.cloudinary.com/disw7bgxd/image/upload/v1721388435/Productos_wq652d.jpg)
![](hhttps://res.cloudinary.com/disw7bgxd/image/upload/v1721388435/Productos_wq652d.jpg)

* Se Ingresa por rol y autenticacion de JWT a cada uno de los modulos
 ![](https://res.cloudinary.com/disw7bgxd/image/upload/v1721388431/Login_mmrsmm.jpg)
 ![](https://res.cloudinary.com/disw7bgxd/image/upload/v1721388432/MainAdmin_qnyy8a.jpg)

-------------------------------------------------------------------------------------------------------------------------
## Clonar el repositorio
* Para clonar el repositorio copia la url en GitHubDesktop y se clonara rapidamente:
https://github.com/Carlos-Alcerro/Sistema_Ventas.git
