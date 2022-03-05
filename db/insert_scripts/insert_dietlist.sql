-- peter saunders
-- february 26 2022
-- insert diet list items

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[DietList]
           ([Id]
           ,[RecipeId]
           ,[DietId])
     VALUES
           (1, 1, 22),
           (2, 1, 16),
           (3, 1, 12),
           (4, 2, 6),
           (5, 2, 12),
           (6, 2, 21),
           (7, 2, 22),
           (8, 2, 20),
           (9, 2, 19),
           (10, 3, 7),
           (11, 3, 12),
           (12, 3, 19),
           (13, 3, 21),
           (14, 3, 22),
           (15, 4, 5),
           (16, 4, 6),
           (17, 4, 20),
           (18, 4, 22)
GO


