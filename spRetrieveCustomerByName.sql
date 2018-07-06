SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spRetrieveCustomerByName]
(
	@CustomerName varchar(100)
)
AS
	SELECT * FROM Customers WHERE CustomerName = @CustomerName
GO