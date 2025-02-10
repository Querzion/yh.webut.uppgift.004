-- SQL Script
-- Created on 10/02/2025 by querzion

-- Write your SQL queries below:

INSERT INTO Products (ProductName, Price)
VALUES ('Konsult', '1000'),
       ('Designer', '760');

INSERT INTO Users (FirstName, LastName, Email)
VALUES ('Slisk', 'Lindqvist', 'slisk.lindqvist@querzion.com');

INSERT INTO Customers (CustomerName)
VALUES ('Querzion Inc.');

INSERT INTO StatusTypes (StatusName)
VALUES ('Ej påbörjat'), ('Pågående'), ('Avslutat');

INSERT INTO Projects (Title, Description, StartDate, EndDate, CustomerId, StatusId, UserId, ProductId)
VALUES ('Hemsida', 'Skapa en hemsida för verksamhet.', '2025-02-28','2025-06-01', 1, 1, 1, 2);