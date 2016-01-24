UPDATE  [OrderDetail]
SET     [Queue] = @Queue
WHERE   OrderId = @OrderId
        AND SeqNbr = @SeqNbr