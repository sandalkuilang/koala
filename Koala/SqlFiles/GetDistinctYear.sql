SELECT DISTINCT(DATEPART(Year, CreatedDate))
FROM [Order]
WHERE Status = 'F'
ORDER BY DATEPART(Year, CreatedDate) DESC