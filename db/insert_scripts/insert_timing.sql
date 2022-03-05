-- peter saunders
-- february 26 2022
-- insert for recipe timings

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[Timing]
           ([Id]
           ,[RecipeId]
           ,[Preparation]
           ,[Cooking])
     VALUES
           (1, 1, '0:20:00', '0:20:00'),
           (2, 2, '0:20:00', '0:10:00'),
           (3, 3, '0:30:00', '0:50:00'),
           (4, 4, '0:05:00', '0:05:00')
GO


