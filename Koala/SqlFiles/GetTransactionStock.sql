SELECT [TransId]
      ,[MaterialId]
	  ,mat.Description MaterialName
      ,sup.[Id]
	  ,sup.Name SupplierName
      ,stock.[QualityId]
	  ,qua.Description QualityName
      ,[Qty]
	  ,[STATUS]
      ,CASE [Status]
		WHEN 'IN' THEN 'Stock In Hand'
		ELSE 'Sold' 
		END AS StatusName
      ,stock.[CreatedDate] 
FROM [TransactionStock] stock 
	INNER JOIN MaterialType mat ON stock.MaterialId = mat.Id AND stock.QualityId = mat.QualityId 
	INNER JOIN Supplier sup ON stock.SupplierId = sup.Id
	INNER JOIN Quality qua ON stock.QualityId = qua.Id 
WHERE (DATEDIFF(MONTH, GETDATE(), stock.CreatedDate) BETWEEN @Between AND 0) AND sup.Active = 1
ORDER BY CreatedDate DESC