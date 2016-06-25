UPDATE [Supplier]
SET Name = @Name
    ,Telp = @Telp
    ,Address = @Address
	,Active = @Active
 WHERE  [Id] = @SupplierId