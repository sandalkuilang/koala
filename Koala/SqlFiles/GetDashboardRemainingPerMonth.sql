SELECT TOP 15
		c.OrderId Id,
        c.CustomerName Name,
        c.Remaining * -1 AS Number
FROM    dbo.[OrderDetail] a 
        INNER JOIN dbo.[Order] c ON a.OrderId = c.OrderId
WHERE   DATEPART(YEAR, CreatedDate) = @Year
	AND DATEPART(MONTH, CreatedDate) = @Month
	AND Remaining < 0
GROUP BY c.OrderId, c.CustomerName, c.Remaining
ORDER BY c.Remaining ASC