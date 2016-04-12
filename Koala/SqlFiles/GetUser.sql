SELECT Username, Type
FROM [user]
WHERE Username = @Username
	AND Password = @Password