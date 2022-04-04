-- insert all items
-- peter saunders
-- mar 23 2022

-- account type
USE [rrr-db]
GO

INSERT INTO [dbo].[AccountType]
           ([Type])
     VALUES
           ('default'),
           ('admin'),
           ('user')
GO

-- person
INSERT INTO [dbo].[Person]
           ([AccountTypeId]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[EmailUpdates]
           ,[EmailNewsletter]
           ,[Username]
           ,[Password]
           ,[FailedLoginCount])
     VALUES
           (2, 'albert', 'albatross', 'albert@albatross.net', 1, 1, 'aalbatross', 'aaa', 0),
           (2, 'brittany', 'bert', 'brittany@bert.org', 1, 0, 'bbert', 'bbb', 0),
           (2, 'christina', 'chen', 'christina@chen.ca', 0, 1, 'cchen', 'ccc', 0),
           (1, 'daphne', 'dunder', 'daphne@dunder.nl', 1, 1, 'ddunder', 'ddd', 0),
           (1, 'emory', 'excel', 'emory@excel.com', 1, 1, 'excel2020', 'eee', 0)
GO

-- account manager
INSERT INTO [dbo].[AccountManager]
           ([PersonId]
           ,[IsLoggedIn]
           ,[LastLogin]
           ,[LastLogout]
           ,[LastActivity])
     VALUES
           (1,1, CONVERT(date,'2022-02-26'), CONVERT(date,'2022-02-22'), CONVERT(time,'19:00:51.740')),
           (2,1, CONVERT(date,'2022-02-26'), CONVERT(date,'2022-02-22'), CONVERT(time,'19:00:51.740')),
           (3,0, CONVERT(date,'2022-02-25'), CONVERT(date,'2022-02-25'), CONVERT(time,'6:00:11.224')),
           (5,0, CONVERT(date,'2022-02-26'), CONVERT(date,'2022-02-26'), CONVERT(time,'14:11:10.110'))
GO

-- season
INSERT INTO [dbo].[Season]
           ([SeasonName])
     VALUES
		   ('no season'),
           ('fall'),
		   ('winter'),
		   ('spring'),
		   ('summer')
GO

-- cuisine
INSERT INTO [dbo].[Cuisine]
           ([Region]
           ,[Country])
     VALUES
           ('', ''),
           ('american', 'united states'),
           ('carribean', ''),
           ('french', 'france'),
           ('italian', 'italy'),
           ('korean', 'korea'),
           ('middle eastern', ''),
           ('thai', 'thailand'),
           ('canadian', 'canada'),
           ('chinese', 'china'),
           ('indian', 'india'),
           ('japanese', 'japan'),
           ('mexican', 'mexico'),
           ('north american', '')
GO

-- course
INSERT INTO [dbo].[Course]
           ([Name])
     VALUES
           (''),
           ('main'),
           ('soup'),
           ('salad'),
           ('lunch'),
           ('dinner'),
           ('dessert'),
           ('entree'),
           ('appetizer'),
           ('cheese'),
           ('fish'),
		   ('breakfast'),
		   ('drink')
GO

-- diet
INSERT INTO [dbo].[Diet]
           ([Name])
     VALUES
           ('d'),
           ('hindu'),
           ('kosher'),
           ('atkins'),
           ('juice'),
           ('keto'),
           ('gluten-free'),
           ('carnivore'),
           ('vegan'),
           ('vegetarian'),
           ('pescatarian'),
           ('plant-based'),
           ('low-fat'),
           ('low-sodium'),
           ('mediterranean'),
           ('okinawa'),
           ('organic'),
           ('raw foodism'),
           ('paleo'),
           ('high protein'),
           ('dairy-free'),
           ('low-carb'),
		   ('no diet')
GO

-- unit
INSERT INTO [dbo].[Unit]
           ([Name]
           ,[Liquid]
           ,[Weight]
           ,[PhysicalMeasure])
     VALUES
           ('', 0, 0, 0),
           ('cup', 1, 0, 0),
           ('cup', 0, 0, 1),
           ('fluid oz', 1, 0, 0),
           ('oz', 0, 1, 0),
           ('tablespoon', 0, 0, 1),
           ('teaspoon', 0, 0, 1),
           ('ml', 1, 0, 0),
           ('litre', 1, 0, 0),
           ('gram', 0, 1, 0)
GO

-- review
INSERT INTO [dbo].[Review]
           ([RatingValue]
           ,[Comment]
           ,[PublishDate]
           ,[Votes]
           ,[IMadeThis]
           ,[IHaveAQuestion])
     VALUES
           (0,'','',0,0,0),
           (0,'is there a vegan option?','2022-02-26 20:52:34.943',1,0,1),
           (5,'best banana pacakes EVER','2022-02-24 8:52:00',2,1,0),
           (2,'a little too spicy for me','2022-02-24 19:40:32',3,1,0),
           (4,'like the fair, but kinda lame','2022-01-22 14:52:20.01',10,1,0),
           (3,'Would tofu dogs work?','2022-01-20 16:12:12.101',4,0,1)
GO

-- recipe
INSERT INTO [dbo].[Recipe]
           ([PersonId]
           ,[CuisineId]
           ,[Name]
           ,[CreationDate]
           ,[ServingCount]
           ,[Story]
           ,[Difficulty])
     VALUES
           (1,1,'banana pancakes', convert(date,'2022-2-22'), 4,'with great power comes great responsibility', 2),
           (2,13,'chili dogs', convert(date,'2022-1-22'), 4,'come now, come many, leave now for forever more', 1),
           (2,5,'buldak', convert(date,'2021-2-11'), 6,'hot hot hot, chicken hot chicken', 3),
           (3,1,'bulletproof coffee', convert(date,'2022-2-26'), 1,'bring it, bring me hot, bring me now', 1)
GO

-- timing
INSERT INTO [dbo].[Timing]
           ([RecipeId]
           ,[Preparation]
           ,[Cooking])
     VALUES
           (1, convert(time,'0:20:00'), convert(time,'0:20:00')),
           (2, convert(time,'0:20:00'), convert(time,'0:10:00')),
           (3, convert(time,'0:30:00'), convert(time,'0:50:00')),
           (4, convert(time,'0:05:00'), convert(time,'0:05:00'))
GO

-- season list
INSERT INTO [dbo].[SeasonList]
           ([RecipeId]
           ,[SeasonId])
     VALUES
           (1,1),
           (1,2),
           (1,3),
           (1,4),
           (2,1),
           (2,4),
           (3,1),
           (3,2),
           (3,4),
           (4,1),
           (4,2),
           (4,3),
           (4,4)
GO

-- instruction
INSERT INTO [dbo].[Instruction]
           ([RecipeId]
		   ,[StepNumber]
           ,[StepWithDoneness])
     VALUES
           (1, 1, 'mix the drys together'),
           (1, 2, 'mix the wets'),
           (1, 3, 'sift the drys'),
           (1, 4, 'slice the bananas into small disks'),
           (1, 5, 'preheat fry pan with oil to medium heat'),
           (1, 6, 'fold the wets and bananas into the drys'),
           (1, 7, 'scoop a reasonable amount per size onto the pan'),
           (1, 8, 'wait until the top becomes pocked with some reasonable bubbles'),
           (1, 9, 'flip the cake'),
           (1, 10, 'wait for a bit longer until the other side is cooked'),
		   (4, 1, 'make 400ml of coffee'),
		   (4, 2, 'melt 100ml of butter'),
		   (4, 3, 'combine coffee and butter'),
		   (4, 4, 'serve')
GO

-- course list
INSERT INTO [dbo].[CourseList]
           ([CourseId]
           ,[RecipeId])
     VALUES
           (1, 1),
           (7, 1),
           (11, 1),
           (1, 2),
           (4, 2),
           (5, 2),
           (7, 2),
           (1, 3),
           (5, 3),
           (11, 4),
           (12, 4)
GO

-- diet list
INSERT INTO [dbo].[DietList]
           ([RecipeId]
           ,[DietId])
     VALUES
           (1, 22),
           (1, 16),
           (1, 12),
           (2, 6),
           (2, 12),
           (2, 21),
           (2, 22),
           (2, 20),
           (2, 19),
           (3, 7),
           (3, 12),
           (3, 19),
           (3, 21),
           (3, 22),
           (4, 5),
           (4, 6),
           (4, 20),
           (4, 22)
GO

-- ingredient list
INSERT INTO [dbo].[IngredientList]
           ([RecipeId]
           ,[UnitId]
           ,[Name]
           ,[Quantity])
     VALUES
           (1, 1, 'egg', 2),
           (1, 3, 'flour', 1),
           (1, 7, 'vanilla extract', 1),
           (1, 2, 'water', 1),
           (2, 1, 'hotdog', 8),
           (2, 3, 'cheddar cheese', 1),
           (2, 2, 'chili', 4),
           (2, 1, 'hotdog bun', 8),
           (3, 9, 'chicken breast', 500),
           (3, 3, 'korean chili flakes', 0.5),
           (3, 3, 'mozarella cheese', 1),
           (4, 2, 'coffee', 0.5),
           (4, 3, 'butter', 0.25)
GO

-- publish state
INSERT INTO [dbo].[PublishState]
           ([RecipeId]
           ,[ReviewId]
           ,[State])
     VALUES
           (1,1,1),
           (2,1,1),
           (3,1,1),
           (4,1,1),
           (2,2,1),
           (2,3,1),
           (1,4,1),
           (1,5,1),
           (3,6,1)
GO

-- person review
INSERT INTO [dbo].[PersonReview]
           ([RecipeId]
           ,[PersonId]
           ,[ReviewId])
     VALUES
           (4,1,2),
           (3,2,3),
           (2,1,4),
           (2,5,5),
           (1,4,1)
GO

-- image recipe
INSERT INTO [dbo].[ImageRecipe]
           ([RecipeId]
           ,[Location])
     VALUES
           (1,'C:\recipeimage\1.jpg'),
           (3,'C:\recipeimage\3.jpg'),
           (2,'C:\recipeimage\2.jpg')
GO

-- image person
INSERT INTO [dbo].[ImagePerson]
           ([PersonId]
           ,[Location])
     VALUES
           (1,'C:\personimage\1.jpg'),
           (2,'C:\personimage\2.jpg'),
           (3,'C:\personimage\3.jpg')
GO