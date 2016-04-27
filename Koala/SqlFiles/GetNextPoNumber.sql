select COUNT(*)
from [order]
where Convert(VARCHAR(12), CreatedDate, 112) = Convert(VARCHAR(12), @CreatedDate, 112)
