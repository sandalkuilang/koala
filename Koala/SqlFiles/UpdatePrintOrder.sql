UPDATE [Order]
SET Status = @Status, UpdateDate = GETDATE()
WHERE OrderId = @OrderId

UPDATE [OrderDetail]
SET Queue = @Queue
WHERE OrderId = @OrderId
