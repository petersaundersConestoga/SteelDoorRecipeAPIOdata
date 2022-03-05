-- peter saunders
-- february 26 2022
-- insert of acccount types
USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[AccountType]
           ([Id]
           ,[Type])
     VALUES
           (1, 'admin'),
           (2, 'user')
GO


