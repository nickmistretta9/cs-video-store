USE [VideoStore]
GO
/****** Object:  StoredProcedure [dbo].[spRetrieveVideoIds]    Script Date: 7/10/2018 8:07:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spRetrieveVideoIds]
(
	@OrderId int
)
AS
	SELECT VideoId FROM VideoOrders WHERE OrderId = @OrderId
