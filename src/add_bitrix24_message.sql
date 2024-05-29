USE [Notifier]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Artem Matveev
-- Create date: 13.03.2024
-- Description:	Add bitrix message to the store
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[add_bitrix_messages]	
	 @notificationId bigint		
	,@body nvarchar(MAX)
	,@priority int
AS
BEGIN	

	declare @Result table(
		 id bigint
		,notification_id bigint
		,resource_id bigint
		,[user_id] bigint
		,body nvarchar(MAX)
		,priority_id int
		,creation_time datetime
		,sent_time datetime
		,receive_time datetime	
	);


	INSERT INTO [dbo].[bitrix_message]([notification_id]
           ,[resource_id]
           ,[user_id]           
           ,[body]
           ,[priority_id])	
	OUTPUT INSERTED.* INTO @Result
	SELECT  @notificationId as notification_id
		   ,c.[resource_id]
		   ,r.[bitrix_user_id]		   		   
		   ,@body as body
		   ,@priority as priority
	FROM [dbo].[convention] as c
	join [dbo].[resource] as r on r.id = c.resource_id
	where c.notification_id = @notificationId and c.enabled = 1	

	
	select id
	      ,notification_id 
		  ,resource_id
		  ,[user_id]
		  ,body
		  ,priority_id
		  ,creation_time
		  ,sent_time
  		  ,receive_time
	from @Result

END
