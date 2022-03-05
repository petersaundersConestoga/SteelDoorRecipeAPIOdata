-- peter saunders
-- february 27 2022
-- insert script for instructio1
USE [CapstoneRecipeDatabase]
GO

INSERT INTO [dbo].[Instruction]
           ([Id]
           ,[RecipeId]
           ,[StepWithDoneness])
     VALUES
           (0, 0, ''),
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


