USE [Notifier]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Artem Matveev
-- Create date: 16.11.2023
-- Description:	Add email message to the store
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[add_email_messages]	
	 @notificationId bigint
	,@fromEmail varchar(50)
	,@fromName varchar(50)
	,@subject nvarchar(255)
	,@body nvarchar(MAX)
	,@priority int
AS
BEGIN	

	declare @Result table(
		 id bigint
		,notification_id bigint
		,resource_id bigint
		,from_name nvarchar(255)
		,from_email nvarchar(255)
		,to_name nvarchar(255)
		,to_email nvarchar(255)
		,subject nvarchar(255)
		,body nvarchar(MAX)
		,priority_id int
		,creation_time datetime
		,sent_time datetime
		,receive_time datetime	
	);


	INSERT INTO [dbo].[email_message]([notification_id]
           ,[resource_id]
           ,[from_name]
           ,[from_email]
           ,[to_name]
           ,[to_email]
           ,[subject]
           ,[body]
           ,[priority_id])	
	OUTPUT INSERTED.* INTO @Result
	SELECT  @notificationId as notification_id
		   ,c.[resource_id]
		   ,@fromName as from_name			
		   ,@fromEmail as from_email
		   ,r.[name] as to_name
		   ,r.[email] as to_email		  		  
		   ,@subject as subject
		   ,@body as body
		   ,@priority as priority
	FROM [dbo].[convention] as c
	join [dbo].[resource] as r on r.id = c.resource_id
	where c.notification_id = @notificationId and c.is_enable = 1	

	
	select id
	      ,notification_id 
		  ,resource_id
		  ,from_name
		  ,from_email
		  ,to_name
		  ,to_email
		  ,subject
		  ,body
		  ,priority_id
		  ,creation_time
		  ,sent_time
  		  ,receive_time
	from @Result

END
