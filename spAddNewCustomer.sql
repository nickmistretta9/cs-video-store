CREATE PROCEDURE [dbo].[spAddNewCustomer]
(
	@CustomerName varchar(100),
	@CustomerEmail nvarchar(50),
	@CustomerPhone nvarchar(11),
	@CustomerOrder int
)
AS
	INSERT INTO Customers (CustomerName, CustomerEmail, CustomerPhone, CustomerOrder)
	VALUES (@CustomerName, @CustomerEmail, @CustomerPhone, @CustomerOrder)

GO