-- SQL Script
-- Created on 10/02/2025 by querzion

-- Write your SQL queries below:

INSERT INTO Products (Id, ProductName, Price)
VALUES ('1','Konsult', '1000'),
       ('2','Designer', '760'),
       ('3','Random','9999');

INSERT INTO Users (Id, FirstName, LastName, Email)
VALUES ('1','Slisk', 'Lindqvist', 'slisk.lindqvist@querzion.com'),
       ('2','Random', 'Randomsson','random@random.nu'),
       ('3','Lova', 'rInge','t@precis.nu');

INSERT INTO Customers (Id, CustomerName)
VALUES ('1','Querzion Inc.'), 
       ('2','SubstansInformation HB'), 
       ('3','Hug(e)Network AB'), 
       ('4','Q-Hub'), 
       ('5','QnD_GameSquad'),
       ('6','Random Inc.');

INSERT INTO StatusTypes (Id, StatusName)
VALUES ('1','Ej påbörjat'), 
       ('2','Pågående'), 
       ('3','Avslutat');

INSERT INTO Projects (Title, Description, StartDate, EndDate, CustomerId, StatusId, UserId, ProductId)
VALUES ('Hemsida', 'Skapa en hemsida för verksamhet.', '2025-02-28','2025-06-01', 1, 1, 1, 2),
       ('OpenAI', 'Träna AI modell','2025-06-01','2026-06-01',3,1,3,1),
       ('Random', 'Random, just Random.', '2025-04-02','2025-06-01', 6, 2, 3, 3);