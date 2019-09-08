USE [DBAurangabad]
GO
/****** Object:  StoredProcedure [dbo].[Usp_UserRegistration]    Script Date: 9/8/2019 9:33:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Usp_UserRegistration]
-- =============================================
-- Author:		Shankar Lugde
-- Create date: 08-09-2019
-- Description:	SP for State Master CRUD Operation
-- =============================================

@Activity					VARCHAR(500) = NULL,
@Error						VARCHAR(500) OUTPUT,
 
@UserId					BIGINT,
@EmailId					NVARCHAR(300),
@FirstName				NVARCHAR(50)  ,
@LastName				NVARCHAR(50)  ,
@MobileNo				NVARCHAR(10)  ,
@IPAddress				NVARCHAR(15)  ,
@Password				NVARCHAR(50)  ,
@ConfirmPassword		NVARCHAR(50)  ,

@IsActive					BIT    = NULL,
@CreatedBy				BIGINT = NULL,
@CreatedOn				DATETIME = NULL,
@ModifiedBy				BIGINT = NULL,
@ModifiedOn				DATETIME = NULL

AS
BEGIN
			DECLARE @CurrentDate DATETIME;
			SET @CurrentDate =getdate ();-- (SELECT [dbo].[GetIndianDateTime] () )

			IF(@Activity='InsUserRegistration')
			BEGIN
				INSERT INTO [dbo].[UserRegistration]
				([UserId],[EmailId]
				,[FirstName],[LastName],[MobileNo]
				,[IPAddress],[Password]
				,[ConfirmPassword],[IsActive]
				,[CreatedBy],[CreatedOn]
				,[ModifiedBy],[ModifiedOn])
				VALUES
				(@UserId,@EmailId,@FirstName
				,@LastName,@MobileNo 
				,@IPAddress,@Password 
				,@ConfirmPassword,@IsActive
				,@CreatedBy,@CreatedOn 
				,@ModifiedBy,@ModifiedOn)
			END

			IF(@Activity='UpdUserRegistration')
			BEGIN
				UPDATE [dbo].[UserRegistration]
				SET	
					EmailId=@EmailId,FirstName=@FirstName
					,LastName=@LastName,MobileNo=@MobileNo 
					,IPAddress=@IPAddress,Password=@Password 
					,ConfirmPassword=@ConfirmPassword,
					IsActive=@IsActive
					,CreatedBy=@CreatedBy,CreatedOn=@CreatedOn 
					,ModifiedBy=@ModifiedBy,ModifiedOn=@ModifiedOn
				WHERE UserId=@UserId;
			END

			IF(@Activity='DelUserRegistration')
			BEGIN
				DELETE FROM [dbo].[UserRegistration]
				WHERE UserId=@UserId;
			END

END
