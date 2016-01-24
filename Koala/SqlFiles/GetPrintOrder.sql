SELECT  OrderId as PoNumber,
        CustomerName ,
        CustomerPhone ,
        Status , 
        CreatedDate , 
		UpdateDate,
        Total AS TotalPayment,
        Installment ,
        Remaining ,
        Disc AS Discount
FROM    dbo.[Order]
WHERE Status = 'I'
	AND DATEDIFF(MONTH, GETDATE(), CreatedDate) BETWEEN @Between AND 0
ORDER BY CreatedDate DESC