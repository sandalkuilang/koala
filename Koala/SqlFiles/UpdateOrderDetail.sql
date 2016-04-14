﻿UPDATE  [OrderDetail]
SET     [MaterialTypeId] = @MaterialTypeId ,
        [QualityId] = @QualityId ,
        [FinishingId] = @FinishingId ,
        [Title] = @Title ,
        [Width] = @Width ,
        [Height] = @Height ,
        [Qty] = @Qty , 
        [Queue] = @Queue ,
        [Deadline] = @Deadline ,
        [Description] = @Description ,
        [Total] = @Total
WHERE   OrderId = @OrderId
        AND SeqNbr = @SeqNbr