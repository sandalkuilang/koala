SELECT TOP 15
        b.Description Name,
        SUM(a.Qty) AS Number
FROM    dbo.[OrderDetail] a
        INNER JOIN dbo.MaterialType b ON a.MaterialTypeId = b.Id
        INNER JOIN dbo.[Order] c ON a.OrderId = c.OrderId
WHERE   DATEPART(YEAR, CreatedDate) = @Year
GROUP BY b.Description
ORDER BY SUM(a.Qty) DESC  