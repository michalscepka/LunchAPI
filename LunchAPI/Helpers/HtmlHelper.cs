using HtmlAgilityPack;
using System.Text;

namespace LunchAPI.Helpers;

public class HtmlHelper
{
	public static async Task<HtmlDocument> GetHtmlDocumentAsync(string url)
	{
		var httpClient = new HttpClient();
		var responseBytes = await httpClient.GetByteArrayAsync(url);
		var response = Encoding.UTF8.GetString(responseBytes);
		var document = new HtmlDocument();
		document.LoadHtml(response);
		return document;
	}
}
