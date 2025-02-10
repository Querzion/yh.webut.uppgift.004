-- SQL Script
-- Created on 10/02/2025 by querzion

-- Write your SQL queries below:
SELECT
    Projects.Id AS Id,
    Title AS Titel,
    Description AS Beskrivning,
    FORMAT (Projects.StartDate, 'yyyy-MM-dd') AS StartDatum,
    FORMAT (Projects.EndDate, 'yyyy-MM-dd') AS SlutDatum,
    C.CustomerName AS Kund,
    ST.StatusName AS Status,
    CONCAT (U.FirstName, ' ', U.LastName) AS Förmedlare,
    P.ProductName AS Service,
    P.Price AS Pris
FROM Projects
JOIN Customers C on C.Id = Projects.CustomerId
JOIN Products P on P.Id = Projects.ProductId
JOIN StatusTypes ST on ST.Id = Projects.StatusId
JOIN Users U on U.Id = Projects.UserId