using LunchAPI.DTO;

namespace LunchAPI.Services
{
	public interface ILunchMenuService
	{
		Task<IEnumerable<Menu>> GetAllMenusAsync();
	}
}