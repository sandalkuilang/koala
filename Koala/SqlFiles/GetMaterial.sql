SELECT  a.Id ,
        a.Description ,
        b.Id AS QualityId,
        b.Description AS QualityName,
		Price
FROM    MaterialType a INNER JOIN Quality b ON a.QualityId = b.Id
ORDER BY Description