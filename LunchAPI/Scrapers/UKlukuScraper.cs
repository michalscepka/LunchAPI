using LunchAPI.DTO;
using LunchAPI.Helpers;

namespace LunchAPI.Scrapers;

public class UKlukuScraper : IWebScraper
{
	private readonly string _restaurantName;
	private readonly string _url;

    public UKlukuScraper(string restaurantName, string url)
	{
		_restaurantName = restaurantName;
		_url = url;
	}

	public async Task<Menu> GetLunchMenuAsync()
	{
		var document = await HtmlHelper.GetHtmlDocumentAsync(_url);

		var menuNodes = new List<string>();
		foreach (var item in document.DocumentNode.SelectNodes("//div[@class='wp-block-columns alignwide is-layout-flex wp-container-32 wp-block-columns-is-layout-flex']"))
			menuNodes.Add(item.InnerText);

		return null;
	}
}
