-- peter saunders
-- february 26 2022
-- insert peron
USE [CapstoneRecipeDatabase]
GO

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
           (0, 2, '', '', '', 0, 0, '', '', 0),
           (1, 2, 'albert', 'albatross', 'albert@albatross.net', 1, 1, 'aalbatross', 'aaa', 0),
           (2, 2, 'brittany', 'bert', 'brittany@bert.org', 1, 0, 'bbert', 'bbb', 0),
           (3, 2, 'christina', 'chen', 'christina@chen.ca', 0, 1, 'cchen', 'ccc', 0),
           (4, 1, 'daphne', 'dunder', 'daphne@dunder.nl', 1, 1, 'ddunder', 'ddd', 0),
           (5, 1, 'emory', 'excel', 'emory@excel.com', 1, 1, 'excel2020', 'eee', 0)
GO


