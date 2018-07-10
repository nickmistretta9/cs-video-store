USE [VideoStore]
GO
/****** Object:  StoredProcedure [dbo].[spRetrieveCustomerByName]    Script Date: 7/9/2018 7:46:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spRetrieveCustomerId
(
	@CustomerName varchar(100)
)
AS
	SELECT CustomerId FROM Customers WHERE CustomerName = @CustomerName
