USE [VideoStore]
GO
/****** Object:  StoredProcedure [dbo].[spAddNewOrder]    Script Date: 7/9/2018 9:14:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.spAddNewVideoOrder
(
	@VideoId int,
	@OrderId int
)
AS
	INSERT INTO VideoOrders (VideoId, OrderId)
	VALUES (@VideoId, @OrderId)