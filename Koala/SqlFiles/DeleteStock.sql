DELETE FROM TransactionStock
WHERE MaterialId = @MaterialId
	AND QualityId = @QualityId
	AND SupplierId = @SupplierId