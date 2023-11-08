using LunchAPI.DTO;
using LunchAPI.Helpers;
using LunchAPI.Scrapers;

namespace LunchAPI.Services;

public class LunchMenuService : ILunchMenuService
{
	public async Task<IEnumerable<Menu>> GetAllMenusAsync()
	{
		var scrapers = new List<IWebScraper>
		{
			new MBScraper()
			{
				RestaurantName = "MB",
				Url = "https://mbrestaurace.cz/",
				XPathExpression = "//div[@class='elementor-widget-wrap elementor-element-populated']/div[4]/div[@class='elementor-widget-container']/table/tbody/tr/td"
			},
			new PastaFidliScraper()
			{
				RestaurantName = "PastaFidli",
				Url = "http://www.pastaafidli.cz/cz/denni-menu/",
				XPathExpression = "//tbody[@class='today']/tr[@class='highlight']/td"
			},
			new PotrefenaHusaScraper()
			{
				RestaurantName = "Potrefená Husa",
				Url = "https://www.menicka.cz/8373-potrefena-husa-ostrava.html",
				XPathExpression = "//div[@class='menicka'][1]//li[@class='jidlo']/div[@class='polozka'] | //div[@class='menicka'][1]//li[@class='jidlo']/div[@class='cena']"
			},
			new UKlukuScraper()
			{
				RestaurantName = "U kluků",
				Url = "https://www.ukluku.cz/",
				XPathExpression = "//div[@class='wp-block-group__inner-container']//strong"
			}
		};

		var tasks = new List<Task>();
		var menus = new List<Menu>();

		foreach (var scraper in scrapers)
			tasks.Add(Task.Run(async () => menus.Add(await GetLunchMenuAsync(scraper))));
		await Task.WhenAll(tasks);

		return menus;
	}

	private async Task<Menu> GetLunchMenuAsync(IWebScraper scraper)
	{
		var menuNodes = await GetMenuNodesAsync(scraper.Url, scraper.XPathExpression);
		var resultMenu = new Menu()
		{
			Id = Guid.NewGuid(),
			RestaurantName = scraper.RestaurantName,
			Url = scraper.Url
		};

		if (menuNodes is null)
		{
			resultMenu.Items = new List<Meal>();
			return resultMenu;
		}

		var menuItems = scraper.GetMenuItems(menuNodes.ToList());
		resultMenu.Items = menuItems;
		return resultMenu;
	}

	private async Task<IEnumerable<string>?> GetMenuNodesAsync(string url, string xPathExpression)
	{
		var document = await HtmlHelper.GetHtmlDocumentAsync(url);

		var menuNodes = new List<string>();
		var selectedNodes = document.DocumentNode.SelectNodes(xPathExpression);
		if (selectedNodes is null)
			return null;

		foreach (var item in selectedNodes)
			menuNodes.Add(item.InnerText);

		return menuNodes;
	}
}
