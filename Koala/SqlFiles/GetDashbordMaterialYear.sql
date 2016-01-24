SELECT TOP 5
        b.Description ,
        COUNT(a.MaterialTypeId) AS Count
FROM    dbo.[OrderDetail] a
        INNER JOIN dbo.MaterialType b ON a.MaterialTypeId = b.Id
        INNER JOIN dbo.[Order] c ON a.OrderId = c.OrderId
WHERE   DATEPART(YEAR, UpdateDate) = @Year
GROUP BY b.Description
ORDER BY COUNT(a.MaterialTypeId) DESC        