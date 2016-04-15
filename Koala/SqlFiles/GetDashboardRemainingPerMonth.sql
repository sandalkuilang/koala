SELECT TOP 15
        c.CustomerName Name,
        SUM(c.Remaining) * -1 AS Number
FROM    dbo.[OrderDetail] a 
        INNER JOIN dbo.[Order] c ON a.OrderId = c.OrderId
WHERE   DATEPART(YEAR, CreatedDate) = @Year
	AND DATEPART(MONTH, CreatedDate) = @Month
	AND Remaining < 0
GROUP BY c.CustomerName
ORDER BY SUM(a.Total) DESC  

