using LunchAPI.DTO;
using LunchAPI.Scrapers;

namespace LunchAPI.Services;

public class LunchMenuService : ILunchMenuService
{
	public async Task<IEnumerable<Menu>> GetAllMenusAsync()
	{
		var scrapers = new List<IWebScraper>
		{
			new MBScraper("MB", "https://mbrestaurace.cz/"),
			new PastaFidliScraper("PastaFidli", "http://www.pastaafidli.cz/cz/denni-menu/"),
			new PotrefenaHusaScraper("Potrefena Husa", "https://www.menicka.cz/8373-potrefena-husa-ostrava.html")
		};

		var tasks = new List<Task>();
		var menus = new List<Menu>();
		foreach (var scraper in scrapers)
		{
			tasks.Add(Task.Run(async () => { menus.Add(await scraper.GetLunchMenuAsync()); }));
		}
		await Task.WhenAll(tasks);

		return menus;
	}
}
