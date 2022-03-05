-- peter saunders
-- february 26 2022
-- insert for course list

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[CourseList]
           ([Id]
           ,[CourseId]
           ,[RecipeId])
     VALUES
           (1, 1, 1),
           (2, 7, 1),
           (3, 11, 1),
           (4, 1, 2),
           (5, 4, 2),
           (6, 5, 2),
           (7, 7, 2),
           (8, 1, 3),
           (9, 5, 3),
           (10, 11, 4),
           (11, 12, 4)
GO


