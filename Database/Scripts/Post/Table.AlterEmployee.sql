/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Check if the column [FavouriteDrinkId] already exists in [dbo].[Employee]
IF NOT EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = 'Employee'
        AND COLUMN_NAME = 'FavouriteDrinkId'
)
BEGIN
    -- If the column doesn't exist, add it
    ALTER TABLE [dbo].[Employee]
    ADD [FavouriteDrinkId] INT NULL

    -- Add the foreign key constraint
    ALTER TABLE [dbo].[Employee]
    ADD CONSTRAINT [FK_Employee_Drink] FOREIGN KEY ([FavouriteDrinkId]) REFERENCES [dbo].Drink
END