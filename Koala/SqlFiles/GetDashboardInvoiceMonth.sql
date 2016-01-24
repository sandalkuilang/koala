SELECT CONVERT(DECIMAl(8,3), SUM(TOTAL) / 1000000) AS Number, DATEPART(Month, UpdateDate) AS Month
FROM dbo.[Order]
WHERE Status = 'F'
	AND Remaining = 0 
	AND DATEPART(YEAR, UpdateDate) = @Year
GROUP BY  DATEPART(Month, UpdateDate)  
ORDER BY DATEPART(Month, UpdateDate) 