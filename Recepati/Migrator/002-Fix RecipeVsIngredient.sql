Drop table RecipeVsIngredient

Create table RecipeVsIngredient(
	Id nvarchar(100),
	RecipeId nvarchar(500),
	IngredientId nvarchar(500),
	primary key (Id)
);
