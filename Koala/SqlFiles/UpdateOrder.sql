UPDATE  [Order]
SET     [Status] = @Status ,
        [Total] = @Total ,
        [Installment] = @Installment ,
        [Remaining] = @Remaining ,
		[UpdateDate] = GETDATE(),
        [Disc] = @Disc
WHERE   OrderId = @OrderId
