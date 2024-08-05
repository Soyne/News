using NewsApiService.Services.News.Queries;
using NewsApiService.Services.News.Dtos;

namespace NewsApiService.Interfaces
{
	public interface IApiClient
	{
		Task<GetEverithingNewsResponse?> GetEverythingNewsAsync(GetEverithingNewsQuery requestDto, CancellationToken cancellationToken = default);
	}
}
