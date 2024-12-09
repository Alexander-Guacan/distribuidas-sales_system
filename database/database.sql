--create database sales;
use sales;

-- Tabla de Roles
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de Permisos
CREATE TABLE Permissions (
    PermissionId INT PRIMARY KEY IDENTITY,
    PermissionName NVARCHAR(100) NOT NULL UNIQUE
);

-- Tabla de Usuarios
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,  -- Contraseña hasheada con bcrypt
    IsActive BIT NOT NULL DEFAULT 1,      -- Indica si el usuario está activo
    LastLogin DATETIME,                  -- Fecha del último inicio de sesión
    FailedLoginAttempts INT DEFAULT 0,    -- Intentos fallidos de inicio de sesión
    LockoutTime DATETIME,                -- Hora en que la cuenta fue bloqueada, si aplica
    RoleId INT FOREIGN KEY REFERENCES Roles(RoleId) -- Rol del usuario
);

-- Tabla de Roles-Permisos (Asignación de permisos a roles)
CREATE TABLE RolePermissions (
    RoleId INT FOREIGN KEY REFERENCES Roles(RoleId),
    PermissionId INT FOREIGN KEY REFERENCES Permissions(PermissionId),
    PRIMARY KEY (RoleId, PermissionId)
);

-- Tabla de Productos
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Categories(CategoryId),
    UnitPrice DECIMAL(18, 2) NOT NULL,
    UnitsInStock INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Tabla de Categorías
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

-- Tabla de Logs de Auditoría
CREATE TABLE AuditLogs (
    LogId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    Action NVARCHAR(255) NOT NULL,           -- Descripción de la acción realizada
    ActionDate DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha y hora de la acción
    IpAddress NVARCHAR(50),                  -- IP del usuario
    UserAgent NVARCHAR(255)                  -- Información del agente de usuario
);

-- Tabla de Intentos Fallidos de Inicio de Sesión
CREATE TABLE FailedLoginAttempts (
    AttemptId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    AttemptTime DATETIME NOT NULL DEFAULT GETDATE(),
    IpAddress NVARCHAR(50)
);

-- Insertar Roles
INSERT INTO Roles (RoleName) VALUES ('Admin');
INSERT INTO Roles (RoleName) VALUES ('Employee');
INSERT INTO Roles (RoleName) VALUES ('Client');

-- Insertar Permisos para Productos
INSERT INTO Permissions (PermissionName) VALUES ('Create Product');
INSERT INTO Permissions (PermissionName) VALUES ('Retrieve Product');
INSERT INTO Permissions (PermissionName) VALUES ('Update Product');
INSERT INTO Permissions (PermissionName) VALUES ('Delete Product');

-- Insertar Permisos para Categorías
INSERT INTO Permissions (PermissionName) VALUES ('Create Category');
INSERT INTO Permissions (PermissionName) VALUES ('Retrieve Category');
INSERT INTO Permissions (PermissionName) VALUES ('Update Category');
INSERT INTO Permissions (PermissionName) VALUES ('Delete Category');

-- Insertar Permisos para Usuarios
INSERT INTO Permissions (PermissionName) VALUES ('Create User');
INSERT INTO Permissions (PermissionName) VALUES ('Retrieve User');
INSERT INTO Permissions (PermissionName) VALUES ('Update User');
INSERT INTO Permissions (PermissionName) VALUES ('Delete User');

-- Asignar permisos al rol Admin
DECLARE @AdminRoleId INT = (SELECT RoleId FROM Roles WHERE RoleName = 'Admin');
DECLARE @CreateProductId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Create Product');
DECLARE @RetrieveProductId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Retrieve Product');
DECLARE @UpdateProductId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Update Product');
DECLARE @DeleteProductId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Delete Product');
DECLARE @CreateCategoryId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Create Category');
DECLARE @RetrieveCategoryId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Retrieve Category');
DECLARE @UpdateCategoryId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Update Category');
DECLARE @DeleteCategoryId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Delete Category');
DECLARE @CreateUserId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Create User');
DECLARE @RetrieveUserId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Retrieve User');
DECLARE @UpdateUserId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Update User');
DECLARE @DeleteUserId INT = (SELECT PermissionId FROM Permissions WHERE PermissionName = 'Delete User');

-- Asignar los permisos a Admin
INSERT INTO RolePermissions (RoleId, PermissionId)
VALUES
    (@AdminRoleId, @CreateProductId),
    (@AdminRoleId, @RetrieveProductId),
    (@AdminRoleId, @UpdateProductId),
    (@AdminRoleId, @DeleteProductId),
    (@AdminRoleId, @CreateCategoryId),
    (@AdminRoleId, @RetrieveCategoryId),
    (@AdminRoleId, @UpdateCategoryId),
    (@AdminRoleId, @DeleteCategoryId),
    (@AdminRoleId, @CreateUserId),
    (@AdminRoleId, @RetrieveUserId),
    (@AdminRoleId, @UpdateUserId),
    (@AdminRoleId, @DeleteUserId);

   -- Asignar permisos al rol Employee
DECLARE @EmployeeRoleId INT = (SELECT RoleId FROM Roles WHERE RoleName = 'Employee');

-- Asignar permisos a Employee
INSERT INTO RolePermissions (RoleId, PermissionId)
VALUES
    (@EmployeeRoleId, @CreateProductId),
    (@EmployeeRoleId, @RetrieveProductId),
    (@EmployeeRoleId, @UpdateProductId),
    (@EmployeeRoleId, @DeleteProductId),
    (@EmployeeRoleId, @RetrieveCategoryId); -- Solo puede consultar categorías

    -- Asignar permisos al rol Client
DECLARE @ClientRoleId INT = (SELECT RoleId FROM Roles WHERE RoleName = 'Client');

-- Asignar permisos a Client
INSERT INTO RolePermissions (RoleId, PermissionId)
VALUES
    (@ClientRoleId, @RetrieveProductId);  -- Solo puede consultar productos

    -- Insertar usuario admin
INSERT INTO Users (UserName, Email, Password, RoleId)
VALUES 
('admin', 'adguacan@espe.edu.ec', '$2a$12$M/q7VytVT0YqyKv3NqxnjOOMOv/CdeLTdxGoFIEsc/ktQOjOJ15aO', (SELECT RoleId FROM Roles WHERE RoleName = 'Admin')); -- Admin&780 

-- Insertar usuario employee
INSERT INTO Users (UserName, Email, Password, RoleId)
VALUES 
('employee', 'employee@example.com', '$2a$12$eJQRDrGnRGL/QdQlxFpl9u9HIr5gJtTeUi0f1kyLtclVJDTjNCAGC', (SELECT RoleId FROM Roles WHERE RoleName = 'Employee')); -- Employee&780

-- Insertar usuario client
INSERT INTO Users (UserName, Email, Password, RoleId)
VALUES 
('client', 'client@example.com', '$2a$12$aAoyG4cZo4L8cj1VONrSgO/O5mCBYnJMTgE1lq7piZNxYbP9WKCoq', (SELECT RoleId FROM Roles WHERE RoleName = 'Client')); -- Client&780
