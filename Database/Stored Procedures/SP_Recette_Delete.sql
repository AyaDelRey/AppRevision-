CREATE PROCEDURE [dbo].[SP_Recette_Delete]
	@recette_id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE 
		FROM [Recettes]
		WHERE [Recette_Id] = @recette_id
END