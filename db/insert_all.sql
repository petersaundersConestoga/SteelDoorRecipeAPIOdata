-- insert all items
-- peter saunders
-- mar 23 2022

-- account type
USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[AccountType]
           ([Id]
           ,[Type])
     VALUES
           (0, 'default'),
           (1, 'admin'),
           (2, 'user')
GO

-- person
INSERT INTO [dbo].[Person]
           ([Id]
           ,[AccountTypeId]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[EmailUpdates]
           ,[EmailNewsletter]
           ,[Username]
           ,[Password]
           ,[FailedLoginCount])
     VALUES
           (1, 2, 'albert', 'albatross', 'albert@albatross.net', 1, 1, 'aalbatross', 'aaa', 0),
           (2, 2, 'brittany', 'bert', 'brittany@bert.org', 1, 0, 'bbert', 'bbb', 0),
           (3, 2, 'christina', 'chen', 'christina@chen.ca', 0, 1, 'cchen', 'ccc', 0),
           (4, 1, 'daphne', 'dunder', 'daphne@dunder.nl', 1, 1, 'ddunder', 'ddd', 0),
           (5, 1, 'emory', 'excel', 'emory@excel.com', 1, 1, 'excel2020', 'eee', 0)
GO

-- account manager
INSERT INTO [dbo].[AccountManager]
           ([Id]
           ,[PersonId]
           ,[IsLoggedIn]
           ,[LastLogin]
           ,[LastLogout]
           ,[LastActivity])
     VALUES
           (1,1,1, CONVERT(date,'2022-02-26'), CONVERT(date,'2022-02-22'), CONVERT(time,'19:00:51.740')),
           (2,2,1, CONVERT(date,'2022-02-26'), CONVERT(date,'2022-02-22'), CONVERT(time,'19:00:51.740')),
           (3,3,0, CONVERT(date,'2022-02-25'), CONVERT(date,'2022-02-25'), CONVERT(time,'6:00:11.224')),
           (4,5,0, CONVERT(date,'2022-02-26'), CONVERT(date,'2022-02-26'), CONVERT(time,'14:11:10.110'))
GO

-- season
INSERT INTO [dbo].[Season]
           ([Id]
           ,[SeasonName])
     VALUES
		   (0,  'no season'),
           (1,	'fall'),
		   (2,	'winter'),
		   (3,	'spring'),
		   (4,	'summer')
GO

-- cuisine
INSERT INTO [dbo].[Cuisine]
           ([Id]
           ,[Region]
           ,[Country])
     VALUES
           (0,	'', ''),
           (1,	'american', 'united states'),
           (2,	'carribean', ''),
           (3,	'french', 'france'),
           (4,	'italian', 'italy'),
           (5,	'korean', 'korea'),
           (6,	'middle eastern', ''),
           (7,	'thai', 'thailand'),
           (8,	'canadian', 'canada'),
           (9,	'chinese', 'china'),
           (10,	'indian', 'india'),
           (11,	'japanese', 'japan'),
           (12,	'mexican', 'mexico'),
           (13,	'north american', '')
GO

-- course
INSERT INTO [dbo].[Course]
           ([Id]
           ,[Name])
     VALUES
           (0, ''),
           (1, 'main'),
           (2, 'soup'),
           (3, 'salad'),
           (4, 'lunch'),
           (5, 'dinner'),
           (6, 'dessert'),
           (7, 'entree'),
           (8, 'appetizer'),
           (9, 'cheese'),
           (10, 'fish'),
		   (11, 'breakfast'),
		   (12, 'drink')
GO

-- diet
INSERT INTO [dbo].[Diet]
           ([Id]
           ,[Name])
     VALUES
           (0,	'd'),
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

-- unit
INSERT INTO [dbo].[Unit]
           ([Id]
           ,[Name]
           ,[Liquid]
           ,[Weight]
           ,[PhysicalMeasure])
     VALUES
           (0,	'', 0, 0, 0),
           (1,	'cup', 1, 0, 0),
           (2,	'cup', 0, 0, 1),
           (3,	'fluid oz', 1, 0, 0),
           (4,	'oz', 0, 1, 0),
           (5,	'tablespoon', 0, 0, 1),
           (6,	'teaspoon', 0, 0, 1),
           (7,	'ml', 1, 0, 0),
           (8,	'litre', 1, 0, 0),
           (9,	'gram', 0, 1, 0)
GO

-- review
INSERT INTO [dbo].[Review]
           ([Id]
           ,[RatingValue]
           ,[Comment]
           ,[PublishDate]
           ,[Votes]
           ,[IMadeThis]
           ,[IHaveAQuestion])
     VALUES
           (0,0,'','',0,0,0),
           (1,0,'is there a vegan option?','2022-02-26 20:52:34.943',1,0,1),
           (2,5,'best banana pacakes EVER','2022-02-24 8:52:00',2,1,0),
           (3,2,'a little too spicy for me','2022-02-24 19:40:32',3,1,0),
           (4,4,'like the fair, but kinda lame','2022-01-22 14:52:20.01',10,1,0),
           (5,3,'Would tofu dogs work?','2022-01-20 16:12:12.101',4,0,1)
GO

-- recipe
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
           (1,1,1,'banana pancakes', convert(date,'2022-2-22'), 4,'with great power comes great responsibility', 2),
           (2,2,13,'chili dogs', convert(date,'2022-1-22'), 4,'come now, come many, leave now for forever more', 1),
           (3,2,5,'buldak', convert(date,'2021-2-11'), 6,'hot hot hot, chicken hot chicken', 3),
           (4,3,1,'bulletproof coffee', convert(date,'2022-2-26'), 1,'bring it, bring me hot, bring me now', 1)
GO

-- timing
INSERT INTO [dbo].[Timing]
           ([Id]
           ,[RecipeId]
           ,[Preparation]
           ,[Cooking])
     VALUES
           (1, 1, convert(time,'0:20:00'), convert(time,'0:20:00')),
           (2, 2, convert(time,'0:20:00'), convert(time,'0:10:00')),
           (3, 3, convert(time,'0:30:00'), convert(time,'0:50:00')),
           (4, 4, convert(time,'0:05:00'), convert(time,'0:05:00'))
GO

-- season list
INSERT INTO [dbo].[SeasonList]
           ([Id]
           ,[RecipeId]
           ,[SeasonId])
     VALUES
           (1,1,1),
           (2,1,2),
           (3,1,3),
           (4,1,4),
           (5,2,1),
           (6,2,4),
           (7,3,1),
           (8,3,2),
           (9,3,4),
           (10,4,1),
           (11,4,2),
           (12,4,3),
           (13,4,4)
GO

-- instruction
INSERT INTO [dbo].[Instruction]
           ([Id]
           ,[RecipeId]
           ,[StepWithDoneness])
     VALUES
           (1, 1, 'mix the drys together'),
           (2, 1, 'mix the wets'),
           (3, 1, 'sift the drys'),
           (4, 1, 'slice the bananas into small disks'),
           (5, 1, 'preheat fry pan with oil to medium heat'),
           (6, 1, 'fold the wets and bananas into the drys'),
           (7, 1, 'scoop a reasonable amount per size onto the pan'),
           (8, 1, 'wait until the top becomes pocked with some reasonable bubbles'),
           (9, 1, 'flip the cake'),
           (10, 1, 'wait for a bit longer until the other side is cooked'),
		   (11, 4, 'make 400ml of coffee'),
		   (12, 4, 'melt 100ml of butter'),
		   (13, 4, 'combine coffee and butter'),
		   (14, 4, 'serve')
GO

-- course list
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

-- diet list
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

-- ingredient list
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
           (9, 3, 9, 'chicken breast', 500),
           (10, 3, 3, 'korean chili flakes', 0.5),
           (11, 3, 3, 'mozarella cheese', 1),
           (12, 4, 2, 'coffee', 0.5),
           (13, 4, 3, 'butter', 0.25)
GO

-- publish state
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
           (5,2,1,1),
           (6,2,2,1),
           (8,1,3,1),
           (9,1,4,1),
           (10,3,5,1)
GO

-- person review
INSERT INTO [dbo].[PersonReview]
           ([Id]
           ,[RecipeId]
           ,[PersonId]
           ,[ReviewId])
     VALUES
           (1,4,1,2),
           (2,3,2,3),
           (3,2,1,4),
           (4,2,5,5),
           (5,1,4,1)
GO

-- image recipe
INSERT INTO [dbo].[ImageRecipe]
           ([Id]
           ,[RecipeId]
           ,[Location])
     VALUES
           (1,1,'C:/recipeimage/1.png'),
           (2,2,'C:/recipeimage/2.png'),
           (3,3,'C:/recipeimage/3.png')
GO

-- image person
INSERT INTO [dbo].[ImagePerson]
           ([Id]
           ,[PersonId]
           ,[Location])
     VALUES
           (1,1,'C:/personimage/1.png'),
           (2,2,'C:/personimage/2.png'),
           (3,3,'C:/personimage/3.png')
GO