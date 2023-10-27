using LunchAPI.DTO;
using LunchAPI.Helpers;

namespace LunchAPI.Scrapers;

public class MBScraper : IWebScraper
{
	private readonly int _offset = 2;
	private readonly string _restaurantName;
	private readonly string _url;

	public MBScraper(string restaurantName, string url)
	{
		_restaurantName = restaurantName;
		_url = url;
	}

	public async Task<Menu> GetLunchMenuAsync()
	{
		var document = await HtmlHelper.GetHtmlDocumentAsync(_url);

		var menuNodes = new List<string>();
		foreach (var item in document.DocumentNode.SelectNodes("//div[@class='elementor-widget-wrap elementor-element-populated']/div[4]/div[@class='elementor-widget-container']/table/tbody/tr/td"))
			menuNodes.Add(item.InnerText);

		var menuItems = GetMenuItems(menuNodes);

		return new Menu() 
		{
			Id = Guid.NewGuid(),
			RestaurantName = _restaurantName,
			Url = _url,
			Items = menuItems
		};
	}

	private IEnumerable<IMenuItem> GetMenuItems(List<string> menuNodes)
	{
		var menuItems = new List<IMenuItem>();
		for (int i = 0; i < menuNodes.Count; i += _offset)
		{
			if (string.IsNullOrWhiteSpace(menuNodes[i]))
				continue;

			menuItems.Add(new Meal()
			{
				Name = menuNodes[i],
				Price = menuNodes[i + 1]
			});
		}
		return menuItems;
	}
}
