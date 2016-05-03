INSERT INTO [Supplier]
           ([SupplierId]
           ,[Name]
           ,[Telp]
           ,[Address]
		   ,CreatedDate)
     VALUES
           (@SupplierId
           ,@Name
           ,@Telp
           ,@Address
		   ,GETDATE())