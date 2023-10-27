using LunchAPI.DTO;
using LunchAPI.Helpers;

namespace LunchAPI.Scrapers;

public class PastaFidliScraper : IWebScraper
{
	private readonly int _offset = 2;
	private readonly string _restaurantName;
	private readonly string _url;

    public PastaFidliScraper(string restaurantName, string url)
	{
		_restaurantName = restaurantName;
		_url = url;
	}

	public async Task<Menu> GetLunchMenuAsync()
	{
		var document = await HtmlHelper.GetHtmlDocumentAsync(_url);

		var menuNodes = new List<string>();
		foreach (var item in document.DocumentNode.SelectNodes("//tbody[@class='today']/tr[@class='highlight']/td"))
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
			menuItems.Add(new Meal()
			{
				Name = menuNodes[i],
				Price = menuNodes[i + 1]
			});
		}
		return menuItems;
	}
}
