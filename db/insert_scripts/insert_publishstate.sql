-- peter saunders
-- february 26 2022
-- insert to publish state

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[PublishState]
           ([Id]
           ,[RecipeId]
           ,[ReviewId]
           ,[State])
     VALUES
           (1,1,0,1),
           (2,2,0,1),
           (3,3,0,1),
           (4,4,0,1),
           (5,0,1,1),
           (6,0,2,1),
           (8,0,3,1),
           (9,0,4,1),
           (10,0,5,1)
GO


