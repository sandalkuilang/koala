SELECT  DISTINCT(a.Description), a.Id
FROM    MaterialType a INNER JOIN Quality b ON a.QualityId = b.Id
ORDER BY Description