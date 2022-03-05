-- peter saunders
-- february 26 2022
-- insert ingredient list

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[IngredientList]
           ([Id]
           ,[RecipeId]
           ,[UnitId]
           ,[Name]
           ,[Quantity])
     VALUES
           (1, 1, 1, 'egg', 2),
           (2, 1, 3, 'flour', 1),
           (3, 1, 7, 'vanilla extract', 1),
           (4, 1, 2, 'water', 1),
           (5, 2, 1, 'hotdog', 8),
           (6, 2, 3, 'cheddar cheese', 1),
           (7, 2, 2, 'chili', 4),
           (8, 2, 1, 'hotdog bun', 8),
           (9, 3, 10, 'chicken breast', 500),
           (10, 3, 3, 'korean chili flakes', 0.5),
           (11, 3, 3, 'mozarella cheese', 1),
           (12, 4, 2, 'coffee', 0.5),
           (13, 4, 3, 'butter', 0.25)
GO


