using LunchAPI.DTO;

namespace LunchAPI.Scrapers;

public class UKlukuScraper : IWebScraper
{
	public required string RestaurantName { get; set; }
	public required string Url { get; set; }
	public required string XPathExpression { get; set; }

	public IEnumerable<Meal> GetMenuItems(List<string> menuNodes)
	{
		var dayNow = DateTime.Now.DayOfWeek;
		var menuItems = new List<Meal>();

		for (int i = 0; i < menuNodes.Count; i++)
		{
			menuItems.Add(new Meal()
			{
				Name = menuNodes[i],
				Price = menuNodes[i + 1]
			});
		}
		return menuItems;
	}
}
