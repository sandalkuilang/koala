SELECT TOP 15
        c.CustomerName Name,
        SUM(a.Total) AS Number
FROM    dbo.[OrderDetail] a 
        INNER JOIN dbo.[Order] c ON a.OrderId = c.OrderId
WHERE   DATEPART(YEAR, CreatedDate) = @Year
GROUP BY c.CustomerName
ORDER BY SUM(a.Total) DESC  