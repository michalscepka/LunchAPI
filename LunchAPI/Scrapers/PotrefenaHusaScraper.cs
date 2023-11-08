using LunchAPI.DTO;

namespace LunchAPI.Scrapers;

public class PotrefenaHusaScraper : IWebScraper
{
	private readonly int _offset = 2;

	public required string RestaurantName { get; set; }
	public required string Url { get; set; }
	public required string XPathExpression { get; set; }

	public IEnumerable<Meal> GetMenuItems(List<string> menuNodes)
	{
		var menuItems = new List<Meal>();
		for (int i = 0; i < menuNodes.Count; i += _offset)
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
