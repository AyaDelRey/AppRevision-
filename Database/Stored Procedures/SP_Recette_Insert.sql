CREATE PROCEDURE [dbo].[SP_Recette_Insert]
	@titre NVARCHAR(64),
	@description NVARCHAR(512) NULL,-- NULL manquant pour la description
	@instructions NVARCHAR(MAX),	-- NULL n'est pas valable pour les instructions
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO [Recettes] ([Titre],[Description],[Instructions],[CreatedBy])
		OUTPUT [inserted].[Recette_Id]
		VALUES
			(@titre, @description, @instructions, @user_id);
END