using LunchAPI.DTO;

namespace LunchAPI.Scrapers;

public interface IWebScraper
{
	public string RestaurantName { get; set; }
	public string Url { get; set; }
	public string XPathExpression { get; set; }

	IEnumerable<Meal> GetMenuItems(List<string> menuNodes);
}
