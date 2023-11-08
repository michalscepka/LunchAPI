namespace LunchAPI.DTO;

public class Menu
{
	public Guid Id { get; set; }
	public string RestaurantName { get; set; }
	public string Url { get; set; }
	public IEnumerable<Meal> Items { get; set; }
}
