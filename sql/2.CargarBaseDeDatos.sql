-- Insertar datos en dbo.Users
INSERT INTO dbo.Users (UserId, Name, Surname, Email, Password, Role)
VALUES
(NEWID(), N'Marcelo', N'Rivas', N'marcelo.rivas@example.com', N'hola123', 1),
(NEWID(), N'Carla', N'Méndez', N'carla.mendez@example.com', N'password123', 0),
(NEWID(), N'Ignacio', N'Ferrer', N'ignacio.ferrer@example.com', N'123456', 1);

-- Insertar datos en dbo.Locales
INSERT INTO dbo.Locales (LocalId, Nombre, Capacidad, Direccion, Zona, DuenioUserId)
VALUES 
(NEWID(), N'Club Nocturno Eclipse', 500, N'Costanera 2020', N'Pilar', (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 1)),
(NEWID(), N'Groove Night Club', 300, N'San Martín 1200', N'Zona Norte', (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 0)),
(NEWID(), N'Neon Arena', 800, N'Independencia 654', N'Zona Sur', (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 1 ORDER BY Name));

-- Insertar datos en dbo.Eventos
INSERT INTO dbo.Eventos (EventoId, Nombre, Descripcion, Fecha, ImgSource, LocalId, DeletedAt)
VALUES 
(NEWID(), N'Fiesta Electrónica Neon', N'Hernán Cattáneo, Mariano Mellino', GETDATE(), N'https://source.unsplash.com/600x400/?dj-set', (SELECT TOP 1 LocalId FROM dbo.Locales), NULL),
(NEWID(), N'Noche Retro', N'Guido Schneider, Gustavo Lamas', DATEADD(DAY, 10, GETDATE()), N'https://source.unsplash.com/600x400/?concert', (SELECT TOP 1 LocalId FROM dbo.Locales ORDER BY Capacidad), NULL),
(NEWID(), N'Sunset Party', N'Jorge Savoretti, Leandro Fresco', DATEADD(DAY, 20, GETDATE()), N'https://source.unsplash.com/600x400/?crowd', (SELECT TOP 1 LocalId FROM dbo.Locales WHERE Zona = N'Zona Norte'), NULL);

-- Insertar datos en dbo.Servicios
INSERT INTO dbo.Servicios (ServicioId, Nombre, Descripcion, Precio, EventoId)
VALUES 
(NEWID(), N'Entrada VIP', N'Acceso a zona exclusiva con vista al DJ y barra privada.', 700.00, (SELECT TOP 1 EventoId FROM dbo.Eventos)),
(NEWID(), N'Barra Libre', N'Acceso ilimitado a todas las bebidas disponibles.', 1000.00, (SELECT TOP 1 EventoId FROM dbo.Eventos ORDER BY Fecha)),
(NEWID(), N'Alquiler de Mesa', N'Mesa reservada en la pista principal con atención personalizada.', 500.00, (SELECT TOP 1 EventoId FROM dbo.Eventos WHERE Nombre = N'Noche Retro'));

-- Insertar datos en dbo.Reservas
INSERT INTO dbo.Reservas (ReservaId, EventoId, CantidadPersonas, Precio, UsuarioId)
VALUES 
(NEWID(), 
 (SELECT TOP 1 EventoId FROM dbo.Eventos), 
 5, 
 3500.00, 
 (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 0)),
(NEWID(), 
 (SELECT TOP 1 EventoId FROM dbo.Eventos ORDER BY Fecha), 
 8, 
 8000.00, 
 (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 0 ORDER BY Name)),
(NEWID(), 
 (SELECT TOP 1 EventoId FROM dbo.Eventos WHERE Nombre = N'Sunset Party'), 
 10, 
 10000.00, 
 (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 0));

-- Insertar datos en dbo.ReservaServicio
INSERT INTO dbo.ReservaServicio (ReservaId, ServicioId)
VALUES 
((SELECT TOP 1 ReservaId FROM dbo.Reservas), (SELECT TOP 1 ServicioId FROM dbo.Servicios)),
((SELECT TOP 1 ReservaId FROM dbo.Reservas ORDER BY CantidadPersonas), (SELECT TOP 1 ServicioId FROM dbo.Servicios ORDER BY Precio)),
((SELECT TOP 1 ReservaId FROM dbo.Reservas WHERE Precio > 7000), (SELECT TOP 1 ServicioId FROM dbo.Servicios WHERE Nombre = N'Alquiler de Mesa'));