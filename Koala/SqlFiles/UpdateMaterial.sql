UPDATE  [MaterialType]
SET     [Description] = @Description ,
        [QualityId] = @QualityId ,
        [Price] = @Price
WHERE   Id = @Id
 