CREATE PROCEDURE [dbo].[SP_Recette_GetByUserId]
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[Recette_Id],
			[Titre], 
			[Description],
			[Instructions],
			[CreatedBy],
			[CreatedAt]
		FROM [Recettes]
	WHERE [CreatedBy] = @user_id
END
