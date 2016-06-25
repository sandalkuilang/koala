SELECT mat.Description MaterialName
	  ,sup.Name  SupplierName
	  ,qua.Description QualityName
      ,SUM([Qty]) Qty
FROM [TransactionStock] stock 
	INNER JOIN MaterialType mat ON stock.MaterialId = mat.Id AND stock.QualityId = mat.QualityId
	INNER JOIN Supplier sup ON stock.SupplierId = sup.Id
	INNER JOIN Quality qua ON stock.QualityId = qua.Id  
WHERE sup.Active = 1 
	AND mat.Id = @MaterialId
	AND mat.QualityId = @QualityId
GROUP BY mat.Description 
	  ,sup.Name 
	  ,qua.Description