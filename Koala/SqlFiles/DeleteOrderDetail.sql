DELETE FROM OrderDetail
WHERE OrderId = @OrderId
	AND SeqNbr = @SeqNbr