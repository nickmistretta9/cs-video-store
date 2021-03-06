USE [VideoStore]
GO
/****** Object:  StoredProcedure [dbo].[spAddNewOrder]    Script Date: 7/10/2018 7:30:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAddNewOrder]
(
	@OrderPrice decimal,
	@OrderDate datetime,
	@OrderCustomer int
)
AS
	DECLARE @convertedMoney money 
	SET @convertedMoney = CAST(@OrderPrice AS money)
	INSERT INTO Orders (OrderCustomer, OrderPrice, OrderDate)
	VALUES (@OrderCustomer, @OrderPrice, @OrderDate)

	SELECT SCOPE_IDENTITY()
