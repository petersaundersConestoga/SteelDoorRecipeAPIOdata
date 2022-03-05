-- peter saunders
-- february 26 2022
-- insert diet types

USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[Diet]
           ([Id]
           ,[Name])
     VALUES
           (1,	'hindu'),
           (2,	'kosher'),
           (3,	'atkins'),
           (4,	'juice'),
           (5,	'keto'),
           (6,	'gluten-free'),
           (7,	'carnivore'),
           (8,	'vegan'),
           (9,	'vegetarian'),
           (10, 'pescatarian'),
           (11, 'plant-based'),
           (12, 'low-fat'),
           (13, 'low-sodium'),
           (14, 'mediterranean'),
           (15, 'okinawa'),
           (16, 'organic'),
           (17, 'raw foodism'),
           (18, 'paleo'),
           (19, 'high protein'),
           (20, 'dairy-free'),
           (21, 'low-carb'),
		   (22, 'no diet')
GO


