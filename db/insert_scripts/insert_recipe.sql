-- peter saunders
-- february 26 2022
-- insert for recipe

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[Recipe]
           ([Id]
           ,[PersonId]
           ,[CuisineId]
           ,[Name]
           ,[CreationDate]
           ,[ServingCount]
           ,[Story]
           ,[Difficulty])
     VALUES
           (0,0,0,'', '', 0,'', 0),
           (1,1,1,'banana pancakes', '2022-2-22', 4,'with great power comes great responsibility', 2),
           (2,2,13,'chili dogs', '2022-1-22', 4,'come now, come many, leave now for forever more', 1),
           (3,2,5,'buldak', '2021-2-11', 6,'hot hot hot, chicken hot chicken', 3),
           (4,3,1,'bulletproof coffee', '2022-2-26', 1,'bring it, bring me hot, bring me now', 1)
GO