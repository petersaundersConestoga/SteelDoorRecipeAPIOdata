-- peter saunders
-- february 26 2022
-- insert to recipe, person, review

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[PersonReview]
           ([Id]
		   ,[RecipeId]
           ,[PersonId]
           ,[ReviewId])
     VALUES
           (0,1,4,1),
           (1,4,1,2),
           (2,3,2,3),
           (3,2,1,4),
           (4,2,5,5)
GO


