--create database Sales_DB;
use Sales_DB;

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1), -- Identidad para autoincrementar el ID
    CategoryName NVARCHAR(100) NOT NULL,      -- Nombre de la categor�a (obligatorio)
    Description NVARCHAR(255) NULL           -- Descripci�n de la categor�a (opcional)
);

-- Creaci�n de la tabla Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1), -- Identidad para autoincrementar el ID
    ProductName NVARCHAR(100) NOT NULL,      -- Nombre del producto (obligatorio)
    CategoryID INT NOT NULL,                 -- Relaci�n con la tabla Categories
    UnitPrice DECIMAL(10, 2) NOT NULL,       -- Precio unitario (obligatorio)
    UnitsInStock INT NOT NULL,               -- Unidades en inventario (obligatorio)
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryID)
        REFERENCES Categories (CategoryID)   -- Llave for�nea a la tabla Categories
        ON DELETE CASCADE                    -- Elimina productos si se elimina la categor�a
        ON UPDATE CASCADE                    -- Actualiza el CategoryID en Products si cambia en Categories
);