USE [VideoStore]
GO
/****** Object:  StoredProcedure [dbo].[spAddNewCustomer]    Script Date: 7/9/2018 7:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.spAddNewOrder
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

