DECLARE @@SupplierId VARCHAR(5);

SELECT @@SupplierId = SupplierId
FROM TransactionStock
WHERE MaterialId = @MaterialId
	AND QualityId = @QualityId
	  
INSERT INTO  [TransactionStock]
    ([MaterialId]
    ,[SupplierId]
    ,[QualityId]
    ,[Qty]
    ,[Status]
    ,[CreatedDate]
    ,[CreatedBy])
VALUES (@MaterialId, @@SupplierId, @QualityId, @Qty * -1, 'OUT', GETDATE(), @CreatedBy)