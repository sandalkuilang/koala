DELETE FROM [Order]
WHERE OrderId = @OrderId
	AND Status = @Status

DELETE FROM [OrderDetail]
WHERE OrderId = @OrderId  