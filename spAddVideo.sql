CREATE PROCEDURE [dbo].[spAddVideo]
(
	@VideoTitle varchar(255),
	@VideoReleaseDate datetime,
	@VideoGenre varchar(50),
	@VideoPrice varchar(10)
)
AS

	DECLARE @convertedMoney money 
	SET @convertedMoney = CAST(@VideoPrice AS money)
	INSERT INTO Videos (VideoTitle, VideoReleaseDate, VideoGenre, VideoPrice)
	VALUES (@VideoTitle, @VideoReleaseDate, @VideoGenre, @convertedMoney)

GO