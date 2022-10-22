Create table Fridge(
	Id nvarchar(100),
	UserId nvarchar(100)
	primary key (Id)
);

Create table FridgeVsIngredient(
	Id nvarchar(100),
	FridgeId nvarchar(500),
	IngredientId nvarchar(500),
	[Name] nvarchar (2000),
	primary key (Id)
);