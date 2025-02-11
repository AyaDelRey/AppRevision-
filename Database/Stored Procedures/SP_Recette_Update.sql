CREATE PROCEDURE [dbo].[SP_Recette_Update]
	@recette_id UNIQUEIDENTIFIER,
	@titre NVARCHAR(64),
	@description NVARCHAR(512) NULL,
	@instructions NVARCHAR(MAX)
AS
BEGIN
	UPDATE [Recettes]
		SET [Titre] = @titre,
			[Description] = @description,
			[Instructions] = @instructions
		WHERE [Recette_Id] = @recette_id
END

