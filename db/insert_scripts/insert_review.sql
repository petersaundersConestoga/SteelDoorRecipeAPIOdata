-- peter saunders
-- february 26 2022
-- insert to review

USE [CapstoneRecipeDatabase]
GO

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


