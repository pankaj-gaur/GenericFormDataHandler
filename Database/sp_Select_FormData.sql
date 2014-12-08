GO

/****** Object:  StoredProcedure [dbo].[sp_Select_FormData]    Script Date: 11/21/2014 3:50:10 PM ******/
DROP PROCEDURE [dbo].[sp_Select_FormData]
GO

/****** Object:  StoredProcedure [dbo].[sp_Select_FormData]    Script Date: 11/21/2014 3:50:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pankaj Gaur
-- Create date: 18 Nov 2014
-- Description:	Select Form Data from the Database
-- =============================================
CREATE PROCEDURE [dbo].[sp_Select_FormData] 
	@FormID NVARCHAR(24) = NULL,
	@ProjectID NVARCHAR(24) = NULL,
	@SubmitDate DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		a.FormDataID, a.FormID, a.ProjectID, a.FormData, a.SubmitDate, a.SubmitterID,
		b.UserIP, b.UserLocation
	FROM [dbo].[FormData] a, [dbo].[SubmitterInfomation] b
	WHERE	(a.FormID = @FormID OR @FormID IS NULL) AND 
			(a.ProjectID = @ProjectID OR @ProjectID IS NULL) AND 
			(a.SubmitDate = @SubmitDate OR @SubmitDate IS NULL) AND 
			(a.SubmitterID = b.SubmitterID)
	
END

GO


