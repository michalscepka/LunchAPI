using LunchAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ILunchMenuService, LunchMenuService>();
var app = builder.Build();

app.MapGet("/", async (ILunchMenuService lunchMenuService) =>
{
	var message = await lunchMenuService.GetAllMenusAsync();
	return Results.Ok(message);
});

app.Run();
