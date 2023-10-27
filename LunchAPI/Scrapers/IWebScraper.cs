using LunchAPI.DTO;

namespace LunchAPI.Scrapers;

public interface IWebScraper
{
	Task<Menu> GetLunchMenuAsync();
}
