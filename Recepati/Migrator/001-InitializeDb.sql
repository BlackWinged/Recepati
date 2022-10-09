Create table Recipe(
	Id nvarchar(100),
	[Name] nvarchar(500),
	[Description] nvarchar(max),
	[Procedure] nvarchar(max),
	primary key (Id)
);


Create table RecipeVsIngredient(
	Id nvarchar(100),
	[Name] nvarchar(500),
	[Description] nvarchar(max),
	[Procedure] nvarchar(max),
	primary key (Id)
);

Create table Ingredient(
	Id nvarchar(100),
	[Name] nvarchar(500),
	Price decimal(18,4),
	primary key (Id)
);
