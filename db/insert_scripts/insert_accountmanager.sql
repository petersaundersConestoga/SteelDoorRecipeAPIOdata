-- peter saunders
-- february 26 2022
-- insert some account information to account manager
USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[AccountManager]
           ([Id]
           ,[PersonId]
           ,[IsLoggedIn]
           ,[LastLogin]
           ,[LastLogout]
           ,[LastActivity])
     VALUES
           (1,1,1,'2022-02-26','2022-02-22','19:00:51.740'),
           (2,2,1,'2022-02-26','2022-02-22','19:00:51.740'),
           (3,3,0,'2022-02-25','2022-02-25','6:00:11.224'),
           (4,5,0,'2022-02-26','2022-02-26','14:11:10.110')
GO


