Create database pruebaTecnica;
use pruebaTecnica;


CREATE TABLE VENTA(
    IdVenta int PRIMARY KEY IDENTITY(1,1),
    NumeroDocumento varchar(20),
    RazonSocial varchar(50),
    Total decimal(10,2),
    IdEstado smallint 
);

select * from VENTA


CREATE TABLE DETALLE_VENTA(
    IdDetalleVenta int PRIMARY KEY IDENTITY(1,1),
    IdVenta int REFERENCES VENTA(IdVenta),
    IdProducto int REFERENCES PRODUCTO(IdProducto), -- Referencia al producto
    Precio decimal(10,2),
    Cantidad int,
    Total decimal(10,2),
    IdEstado smallint -- Nuevo campo para el estado del detalle de venta
); go

-- Agregar campo de fecha a la tabla VENTA
ALTER TABLE VENTA
ADD FechaRegistro DATETIME DEFAULT GETDATE(); -- Agrega la fecha actual como valor predeterminado

-- Agregar campo de fecha a la tabla DETALLE_VENTA
ALTER TABLE DETALLE_VENTA
ADD FechaRegistro DATETIME DEFAULT GETDATE(); -- Agrega la fecha actual como valor predeterminado

CREATE PROCEDURE [dbo].[DetalleVenta_Insertar_sp]
    @p_idventa int, -- Recibe el ID de la venta
    @p_idproducto int, -- Nuevo parámetro para el IdProducto
    @p_precio decimal(10,2),
    @p_cantidad int,
    @p_total decimal(10,2),
    @p_idestado smallint -- Nuevo parámetro para IdEstado
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar en la tabla DETALLE_VENTA
    INSERT INTO DETALLE_VENTA (
        IdVenta,
        IdProducto, -- Solo se inserta el IdProducto
        Precio,
        Cantidad,
        Total,
        IdEstado -- Insertar el valor de IdEstado
    )
    VALUES (
        @p_idventa, -- Este es el ID de la venta asociada
        @p_idproducto, -- ID del producto
        @p_precio,
        @p_cantidad,
        @p_total,
        @p_idestado
    );
END;

CREATE PROCEDURE [dbo].[Producto_Actualizar_sp]
    @p_idproducto int,
    @p_nombre varchar(100),
    @p_precio decimal(10,2),
    @p_stock int
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE PRODUCTO
    SET 
        Nombre = @p_nombre,
        Precio = @p_precio,
        Stock = @p_stock
    WHERE IdProducto = @p_idproducto;
END;

CREATE PROCEDURE [dbo].[Producto_Insertar_sp]
    @p_nombre varchar(100),
    @p_precio decimal(10,2),
    @p_stock int
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO PRODUCTO (
        Nombre,
        Precio,
        Stock,
        IdEstado
    )
    VALUES (
        @p_nombre,
        @p_precio,
        @p_stock,
        1
    );
END;



CREATE PROCEDURE [dbo].[Producto_Eliminar_sp]
    @p_idproducto int
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el estado del producto a 0
    UPDATE PRODUCTO
    SET IdEstado = 0
    WHERE IdProducto = @p_idproducto;
END;

select * from PRODUCTO

CREATE PROCEDURE [dbo].[Producto_Listar_sp]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdProducto,
        Nombre,
        Precio,
        Stock,
        IdEstado,
		FechaRegistro
    FROM PRODUCTO
	Where IdEstado = 1
END;

CREATE PROCEDURE [dbo].[Producto_SeleccionarPorId_sp]
    @IdProducto INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdProducto,
        Nombre,
        Precio,
        Stock,
        IdEstado
    FROM PRODUCTO
    WHERE IdProducto = @IdProducto AND IdEstado = 1;
END;

CREATE PROCEDURE [dbo].[Venta_Insertar_sp]
    @p_idventa int OUTPUT, -- Para devolver el ID generado
    @p_numeroDocumento varchar(20),
    @p_razonSocial varchar(50),
    @p_total decimal(10,2),
    @p_idestado smallint -- Nuevo parámetro para IdEstado
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar en la tabla VENTA
    INSERT INTO VENTA(
        NumeroDocumento,
        RazonSocial,
        Total,
        IdEstado -- Insertar el valor de IdEstado
    )
    VALUES (
        @p_numeroDocumento,
        @p_razonSocial,
        @p_total,
        @p_idestado
    );

    -- Obtener el ID de la venta recién insertada utilizando @@IDENTITY
    SELECT @p_idventa = @@IDENTITY;
END;

CREATE PROCEDURE [dbo].[Venta_Listar_sp]
AS
BEGIN
    SELECT 
        IdVenta, 
        NumeroDocumento, 
        RazonSocial, 
        Total,
		IdEstado,
		FechaRegistro
    FROM VENTA
	WHERE IdEstado = 1
END

INSERT INTO PRODUCTO (Nombre, Precio, Stock, IdEstado) VALUES
('Producto 1', 29.99, 100, 1) -- Producto disponible