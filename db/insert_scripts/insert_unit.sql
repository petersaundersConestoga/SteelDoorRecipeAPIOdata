-- peter saunders
-- february 26 2022
-- insert measurement types

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[Unit]
           ([Id]
           ,[Name]
           ,[Liquid]
           ,[Weight]
           ,[PhysicalMeasure])
     VALUES
           (1,	'none', 0, 0, 0),
           (2,	'cup', 1, 0, 0),
           (3,	'cup', 0, 0, 1),
           (4,	'fluid oz', 1, 0, 0),
           (5,	'oz', 0, 1, 0),
           (6,	'tablespoon', 0, 0, 1),
           (7,	'teaspoon', 0, 0, 1),
           (8,	'ml', 1, 0, 0),
           (9,	'litre', 1, 0, 0),
           (10,	'gram', 0, 1, 0)
GO


