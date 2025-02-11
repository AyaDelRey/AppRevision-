CREATE PROCEDURE [dbo].[SP_Recette_GetAll]
AS
BEGIN
	SELECT	[Recette_Id],
			[Titre], 
			[Description],
			[Instructions],
			[CreatedBy],
			[CreatedAt]
		FROM [Recettes]
END
