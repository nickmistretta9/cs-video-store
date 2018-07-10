USE [VideoStore]
GO
/****** Object:  StoredProcedure [dbo].[spAddNewCustomer]    Script Date: 7/9/2018 7:27:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAddNewCustomer]
(
	@CustomerName varchar(100),
	@CustomerEmail nvarchar(50),
	@CustomerPhone nvarchar(11)
)
AS
	INSERT INTO Customers (CustomerName, CustomerEmail, CustomerPhone)
	VALUES (@CustomerName, @CustomerEmail, @CustomerPhone)

