UPDATE  [MaterialType]
SET     [Description] = @Description , 
        [Price] = @Price
WHERE   Id = @Id
	AND [QualityId] = @QualityId
 