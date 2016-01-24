SELECT  a.OrderId ,
        SeqNbr ,
        MaterialTypeId AS MaterialId ,
        QualityId ,
        FinishingId ,
        SizeId ,
        Title ,
        Width ,
        Height ,
        Qty ,
        b.Status
        Filename ,
        Image AS Stream ,
        Queue ,
        Deadline ,
        Description ,
        a.Total
FROM    [OrderDetail] a
        INNER JOIN [Order] b ON a.OrderId = b.OrderId
WHERE a.OrderId = @OrderId 
