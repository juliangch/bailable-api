-- Insertar datos en dbo.Users (para cumplir FK de Locales)
INSERT INTO dbo.Users (UserId, Name, Surname, Email, Password, Role)
VALUES
(NEWID(), N'Juan', N'Pérez', N'juan.perez@example.com', N'password123', 1),
(NEWID(), N'María', N'Gómez', N'maria.gomez@example.com', N'password123', 0),
(NEWID(), N'Carlos', N'López', N'carlos.lopez@example.com', N'password123', 1);

-- Insertar datos en dbo.Locales
INSERT INTO dbo.Locales (LocalId, Nombre, Capacidad, Direccion, Zona, DuenioUserId)
VALUES 
(NEWID(), N'Local A', 100, N'Calle 123, Ciudad A', N'Zona Centro', (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 1)),
(NEWID(), N'Local B', 50, N'Calle 456, Ciudad B', N'Zona Norte', (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 0)),
(NEWID(), N'Local C', 200, N'Calle 789, Ciudad C', N'Zona Sur', (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 1 ORDER BY Name));

-- Insertar datos en dbo.Eventos
INSERT INTO dbo.Eventos (EventoId, Nombre, Descripcion, Fecha, ImgSource, LocalId, DeletedAt)
VALUES 
(NEWID(), N'Evento 1', N'Descripción del evento 1', GETDATE(), N'/imagenes/evento1.jpg', (SELECT TOP 1 LocalId FROM dbo.Locales), NULL),
(NEWID(), N'Evento 2', N'Descripción del evento 2', DATEADD(DAY, 10, GETDATE()), N'/imagenes/evento2.jpg', (SELECT TOP 1 LocalId FROM dbo.Locales ORDER BY Capacidad), NULL),
(NEWID(), N'Evento 3', N'Descripción del evento 3', DATEADD(DAY, 20, GETDATE()), N'/imagenes/evento3.jpg', (SELECT TOP 1 LocalId FROM dbo.Locales WHERE Zona = N'Zona Norte'), NULL);

-- Insertar datos en dbo.Servicios
INSERT INTO dbo.Servicios (ServicioId, Nombre, Descripcion, Precio, EventoId)
VALUES 
(NEWID(), N'Servicio A', N'Descripción del servicio A', 100.00, (SELECT TOP 1 EventoId FROM dbo.Eventos)),
(NEWID(), N'Servicio B', N'Descripción del servicio B', 200.00, (SELECT TOP 1 EventoId FROM dbo.Eventos ORDER BY Fecha)),
(NEWID(), N'Servicio C', N'Descripción del servicio C', 150.00, (SELECT TOP 1 EventoId FROM dbo.Eventos WHERE Nombre = N'Evento 2'));
-- Insertar datos en dbo.Reservas
INSERT INTO dbo.Reservas (ReservaId, EventoId, CantidadPersonas, Precio, UsuarioId)
VALUES 
(NEWID(), (SELECT TOP 1 EventoId FROM dbo.Eventos), 5, 500.00, (SELECT TOP 1 UserId FROM dbo.Users)),
(NEWID(), (SELECT TOP 1 EventoId FROM dbo.Eventos ORDER BY Fecha), 3, 300.00, (SELECT TOP 1 UserId FROM dbo.Users ORDER BY Name)),
(NEWID(), (SELECT TOP 1 EventoId FROM dbo.Eventos WHERE Nombre = N'Evento 3'), 10, 1000.00, (SELECT TOP 1 UserId FROM dbo.Users WHERE Role = 0));

-- Insertar datos en dbo.ReservaServicio
INSERT INTO dbo.ReservaServicio (ReservaId, ServicioId)
VALUES 
((SELECT TOP 1 ReservaId FROM dbo.Reservas), (SELECT TOP 1 ServicioId FROM dbo.Servicios)),
((SELECT TOP 1 ReservaId FROM dbo.Reservas ORDER BY CantidadPersonas), (SELECT TOP 1 ServicioId FROM dbo.Servicios ORDER BY Precio)),
((SELECT TOP 1 ReservaId FROM dbo.Reservas WHERE Precio > 500), (SELECT TOP 1 ServicioId FROM dbo.Servicios WHERE Nombre = N'Servicio C'));