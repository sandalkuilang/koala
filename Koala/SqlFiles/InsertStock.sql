﻿INSERT INTO [TransactionStock] ([MaterialId] ,[QualityId] ,[SupplierId] ,[Qty] ,[Status] ,[CreatedDate] ,[CreatedBy])
VALUES (@MaterialId, @QualityId, @SupplierId, @Qty, 'IN', GETDATE(), @CreatedBy)        