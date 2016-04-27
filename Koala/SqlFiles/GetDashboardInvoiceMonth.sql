SELECT CAST(SUM(TOTAL) AS money) Number, 
	DATEPART(Month, CreatedDate) AS Name, 
	COUNT(OrderId) [Order],
	(SELECT SUM(Remaining) FROM [Order] WHERE Status <> 'F' AND Remaining <> 0 AND DATEPART(YEAR, CreatedDate) = @Year) TotalInstallment,
	(SELECT COUNT(OrderId) FROM [Order] WHERE Status <> 'F' AND Remaining <> 0 AND DATEPART(YEAR, CreatedDate) = @Year) OrderInstallment
FROM dbo.[Order]
WHERE Status = 'F'
	AND Remaining = 0  
	AND DATEPART(YEAR, CreatedDate) = @Year 
GROUP BY  DATEPART(Month, CreatedDate)  
ORDER BY DATEPART(Month, CreatedDate) 