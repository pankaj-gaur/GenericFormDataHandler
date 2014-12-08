GO

/****** Object:  StoredProcedure [dbo].[sp_Insert_FormData]    Script Date: 11/21/2014 3:46:56 PM ******/
DROP PROCEDURE [dbo].[sp_Insert_FormData]
GO

/****** Object:  StoredProcedure [dbo].[sp_Insert_FormData]    Script Date: 11/21/2014 3:46:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pankaj Gaur
-- Create date: 18 Nov 2014
-- Description:	Insert Form Data in the Database
-- =============================================
CREATE PROCEDURE [dbo].[sp_Insert_FormData] 
	@FormID NVARCHAR(24),
	@ProjectID NVARCHAR(24),
	@FormData VARBINARY(MAX),
	@UserIP NVARCHAR(16) = NULL,
	@UserLocation NVARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @submitterID UNIQUEIDENTIFIER
	SET @submitterID = NEWID();

	INSERT INTO [dbo].[FormData] 
	(FormDataID,FormID,ProjectID,FormData,SubmitDate,SubmitterID)
	VALUES
	(NEWID(), @FormID, @ProjectID, @FormData, GETDATE(),@submitterID)

	INSERT INTO [dbo].[SubmitterInfomation]
	(SubmitterID,UserIP,UserLocation)
	VALUES
	(@submitterID, @UserIP, @UserLocation)
	
END

GO


