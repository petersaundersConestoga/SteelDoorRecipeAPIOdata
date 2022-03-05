-- peter saunders
-- february 26 2022
-- insert to recipe, person, review

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[PersonReview]
           ([RecipeId]
           ,[PersonId]
           ,[ReviewId])
     VALUES
           (1,4,1),
           (4,1,2),
           (3,2,3),
           (2,1,4),
           (2,5,5)
GO


