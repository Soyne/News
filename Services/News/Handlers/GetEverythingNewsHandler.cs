using MediatR;
using NewsApiService.Interfaces;
using NewsApiService.Services.News.Queries;
using NewsApiService.Services.News.Dtos;

namespace NewsApiService.Services.News.Handlers
{
	public class GetEverythingNewsHandler : IRequestHandler<GetEverithingNewsQuery, GetEverithingNewsResponse?>
	{
		private readonly IApiClient _apiClient;

		public GetEverythingNewsHandler(IApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public async Task<GetEverithingNewsResponse?> Handle(GetEverithingNewsQuery request, CancellationToken cancellationToken)
		{
			var response = await _apiClient.GetEverythingNewsAsync(request, cancellationToken);

			return response;
		}
	}
}
