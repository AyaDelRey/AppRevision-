CREATE PROCEDURE [dbo].[SP_Recette_GetById]
	@recette_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[Recette_Id],
			[Titre], 
			[Description],
			[Instructions],
			[CreatedBy],
			[CreatedAt]
		FROM [Recettes]
	WHERE [Recette_Id] = @recette_id
END
